using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

/// <summary>
/// This is a full test of all client features. It is done on a auto-generated board that is deleted at the end so will not touch any existing boards for the token
/// </summary>
public class CardTests : TestBaseWithNewBoard
{
    [Fact]
    public async Task CardCrud()
    {
        var member = (await TrelloClient.GetMembersOfBoardAsync(BoardId)).First();
        var allLabelsOnBoard = await TrelloClient.GetLabelsOfBoardAsync(BoardId);

        //Test No cards exist
        Assert.Empty(await TrelloClient.GetCardsOnBoardAsync(BoardId));
        Assert.Empty(await TrelloClient.GetCardsOnBoardFilteredAsync(BoardId, CardsFilter.All));

        //Create list to test cards on
        var cardList = await TrelloClient.AddListAsync(new List("List for Card Tests", BoardId));

        //Add Card
        DateTimeOffset? start = new DateTimeOffset(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        DateTimeOffset? due = new DateTimeOffset(new DateTime(2099, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        var labelIds = new List<string>
        {
            allLabelsOnBoard[1].Id,
            allLabelsOnBoard[2].Id
        };

        var memberIds = new List<string> { member.Id };
        var addedCard = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Card", "Description", start, due, false, labelIds, memberIds));
        AssertTimeIsNow(addedCard.Created);
        AssertTimeIsNow(addedCard.LastActivity);
        Assert.Equal("Card", addedCard.Name);
        Assert.Equal("Description", addedCard.Description);
        Assert.Equal(start, addedCard.Start);
        Assert.Equal(due, addedCard.Due);
        Assert.False(addedCard.DueComplete);
        Assert.Equal(2, addedCard.Labels.Count);
        Assert.Equal(allLabelsOnBoard[1].Color, addedCard.Labels[0].Color);
        Assert.Equal(allLabelsOnBoard[2].Color, addedCard.Labels[1].Color);
        Assert.Single(addedCard.MemberIds);
        Assert.Single(await TrelloClient.GetCardsInListAsync(cardList.Id));
        Assert.NotEmpty(addedCard.Url);
        Assert.NotEmpty(addedCard.ShortUrl);
        Assert.NotEmpty(addedCard.IdShort.ToString());
        Assert.NotNull(addedCard.Cover);

        var membersOfCardAsync = await TrelloClient.GetMembersOfCardAsync(addedCard.Id);
        Assert.Single(membersOfCardAsync);

        //Update Card
        addedCard.DueComplete = true;
        addedCard.Description = "New Description";
        var updateCard = await TrelloClient.UpdateCardAsync(addedCard);
        Assert.True(updateCard.DueComplete);
        Assert.Equal("New Description", updateCard.Description);
        var getCard = await TrelloClient.GetCardAsync(addedCard.Id);
        Assert.Equal(updateCard.Description, getCard.Description);
        Assert.Equal(updateCard.DueComplete, getCard.DueComplete);

        //Test Checklists
        var checklists = await TrelloClient.GetChecklistsOnBoardAsync(BoardId);
        Assert.Empty(checklists);

        var checklistItems = new List<ChecklistItem>
        {
            new("ItemA", due, member.Id),
            new("ItemB"),
            new("ItemC", null, member.Id)
        };
        var newChecklist = new Checklist("Sample Checklist", checklistItems);
        var addedChecklist = await TrelloClient.AddChecklistAsync(getCard.Id, newChecklist, true);
        await TrelloClient.AddChecklistAsync(getCard.Id, newChecklist, true);

        Assert.Equal("Sample Checklist", addedChecklist.Name);
        Assert.Equal(3, addedChecklist.Items.Count);
        var itemA = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemA");
        var itemB = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemB");
        var itemC = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemC");

        Assert.NotNull(itemA);
        Assert.NotEmpty(itemA.Id);
        Assert.Equal(ChecklistItemState.Incomplete, itemA.State);
        Assert.Equal(addedChecklist.Id, itemA.ChecklistId);
        Assert.NotNull(itemB);
        Assert.NotEmpty(itemB.Id);
        Assert.Equal(addedChecklist.Id, itemB.ChecklistId);
        Assert.Equal(ChecklistItemState.Incomplete, itemB.State);
        Assert.NotNull(itemC);
        Assert.NotEmpty(itemC.Id);
        Assert.Equal(addedChecklist.Id, itemC.ChecklistId);
        Assert.Equal(ChecklistItemState.Incomplete, itemC.State);

        //Assert.Equal(due, itemA.Due); //This will fail on a free version of Trello so commented out
        //Assert.Equal(member.Id, itemA.MemberId); //This will fail on a free version of Trello so commented out

        Assert.Null(itemB.Due);
        Assert.Null(itemB.MemberId);

        Assert.Null(itemC.Due);
        //Assert.Equal(member.Id, itemC.MemberId); //This will fail on a free version of Trello so commented out

        var doneCard = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Card 2"));
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true);
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true);

        var checklistsNow = await TrelloClient.GetChecklistsOnBoardAsync(BoardId);
        Assert.Equal(2, checklistsNow.Count);

        var cardsNow = await TrelloClient.GetCardsOnBoardAsync(BoardId);
        Assert.Equal(2, cardsNow.Count);

        await TrelloClient.DeleteCard(doneCard.Id);
        var cardsNowAfterDelete = await TrelloClient.GetCardsOnBoardAsync(BoardId);
        Assert.Single(cardsNowAfterDelete);

        var rawPost = await TrelloClient.PostAsync("cards", new QueryParameter("name", "Card"), new QueryParameter("idList", cardList.Id));
        Assert.NotNull(rawPost);

        var rawPostCard = await TrelloClient.PostAsync<Card>("cards", new QueryParameter("name", "Card"), new QueryParameter("idList", cardList.Id));
        Assert.NotNull(rawPostCard.Id);

        var rawUpdate = await TrelloClient.PutAsync($"cards/{rawPostCard.Id}", new QueryParameter("desc", "New Description"));
        Assert.NotNull(rawUpdate);

        var rawUpdateCard = await TrelloClient.PutAsync<Card>($"cards/{rawPostCard.Id}", new QueryParameter("desc", "New Description2"));
        Assert.Equal("New Description2", rawUpdateCard.Description);

        //Archive and Reopen
        var cardForArchiveAndReopen = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Card for archive and reopen"));
        Assert.False(cardForArchiveAndReopen.Closed);
        var archivedCard = await TrelloClient.ArchiveCardAsync(cardForArchiveAndReopen.Id);
        Assert.True(archivedCard.Closed);
        var reopendCard = await TrelloClient.ReOpenCardAsync(archivedCard.Id);
        Assert.False(reopendCard.Closed);

        //Test: Set dates
        var cardWithDates = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Date Card"));
        DateTimeOffset testStartDate = new DateTimeOffset(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        var testDueDate = testStartDate.AddDays(2);
        
        var cardWithStart = await TrelloClient.SetStartDateOnCardAsync(cardWithDates.Id, testStartDate);
        Assert.Equal(testStartDate, cardWithStart.Start);
        
        var cardWithDue = await TrelloClient.SetDueDateOnCardAsync(cardWithDates.Id, testDueDate);
        Assert.Equal(testStartDate, cardWithDue.Start);
        Assert.Equal(testDueDate, cardWithDue.Due);
        
        var cardWithStartAndDue = await TrelloClient.SetStartDateAndDueDateOnCardAsync(cardWithDates.Id, testStartDate.AddDays(1), testDueDate.AddDays(1), true);
        Assert.Equal(testStartDate.AddDays(1), cardWithStartAndDue.Start);
        Assert.Equal(testDueDate.AddDays(1), cardWithStartAndDue.Due);

        //Test: Add/Remove Labels
        var cardWithLabels = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Label Card"));
        Assert.Empty(cardWithLabels.LabelIds);
        var cardWithLabelsAdded = await TrelloClient.AddLabelsToCardAsync(cardWithLabels.Id, allLabelsOnBoard.Select(x=> x.Id).ToArray());
        Assert.Equal(allLabelsOnBoard.Count, cardWithLabelsAdded.LabelIds.Count);
        await TrelloClient.AddLabelsToCardAsync(cardWithLabels.Id, allLabelsOnBoard.Select(x=> x.Id).ToArray()); //Call once more to test it can handle added something already there

        var cardWithSingleLabelsRemoved = await TrelloClient.RemoveLabelsFromCardAsync(cardWithLabels.Id, allLabelsOnBoard.First().Id);
        Assert.Equal(allLabelsOnBoard.Count-1, cardWithSingleLabelsRemoved.LabelIds.Count);
        await TrelloClient.RemoveLabelsFromCardAsync(cardWithLabels.Id, allLabelsOnBoard.First().Id); //Call once more to test it can handle removing something already not there

        var cardWithAllLabelsRemoved = await TrelloClient.RemoveAllLabelsFromCardAsync(cardWithLabels.Id);
        Assert.Empty(cardWithAllLabelsRemoved.LabelIds);
        await TrelloClient.RemoveAllLabelsFromCardAsync(cardWithLabels.Id); //Call once more to test it can handle removing from already empty

        //Test: Add/Remove Members
        var cardWithMembers = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Member Card"));
        Assert.Empty(cardWithMembers.MemberIds);
        var cardWithMemberAdded = await TrelloClient.AddMembersToCardAsync(cardWithLabels.Id, memberIds.ToArray());
        Assert.Single(cardWithMemberAdded.MemberIds);
        await TrelloClient.AddMembersToCardAsync(cardWithLabels.Id, memberIds.ToArray()); //Call once more to test it can handle added something already there

        var cardWithSingleMemberRemoved = await TrelloClient.RemoveMembersFromCardAsync(cardWithLabels.Id, memberIds.First());
        Assert.Empty(cardWithSingleMemberRemoved.MemberIds);
        await TrelloClient.RemoveMembersFromCardAsync(cardWithLabels.Id, memberIds.First()); //Call once more to test if remove something not there works

        await TrelloClient.AddMembersToCardAsync(cardWithMembers.Id, memberIds.ToArray());//Re-add as test-board only have a single member
        var cardWithAllMemberRemoved = await TrelloClient.RemoveAllMembersFromCardAsync(cardWithMembers.Id);
        Assert.Empty(cardWithAllMemberRemoved.MemberIds);
        await TrelloClient.RemoveAllMembersFromCardAsync(cardWithMembers.Id); //call once more to test it can handle already empty member list

        //Custom Delete
        var customDeleteCard = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Custom Delete Card"));
        await TrelloClient.DeleteAsync($"cards/{customDeleteCard.Id}");
    }
}