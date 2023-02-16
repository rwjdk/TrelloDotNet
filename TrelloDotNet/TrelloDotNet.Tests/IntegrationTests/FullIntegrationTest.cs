using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

/// <summary>
/// This is a full test of all client features. It is done on a auto-generated board that is deleted at the end so will not touch any existing boards for the token
/// </summary>
public class FullIntegrationTest : IntegrationTestBase
{
    [Fact]
    public async Task CanUpdateBoard()
    {
        Board.Name = BoardName + "X";
        Board.Description = BoardDescription + "X";
        var updatedBoard = await TrelloClient.UpdateBoardAsync(Board);
        var getBoard = await TrelloClient.GetBoardAsync(BoardId);
        Assert.EndsWith("X", updatedBoard.Name);
        Assert.EndsWith("X", updatedBoard.Description);
        Assert.EndsWith("X", getBoard.Name);
        Assert.EndsWith("X", getBoard.Description);
        Assert.Equal(updatedBoard.Description, getBoard.Description);
        Assert.Equal(updatedBoard.Description, getBoard.Description);
    }

    [Fact]
    public async Task DefaultSingleMemberOnBoard()
    {
        var members = await TrelloClient.GetMembersOfBoardAsync(BoardId);
        Assert.Single(members);
        var member = await TrelloClient.GetMember(members.First().Id);
        Assert.Equal(member.FullName, members.First().FullName);
        Assert.Equal(member.Username, members.First().Username);
    }

    [Fact]
    public async Task DefaultsOnBoard()
    {
        var lists = await TrelloClient.GetListsOnBoardAsync(BoardId);
        var todoList = lists.FirstOrDefault(x => x.Name == "To Do");
        var doingList = lists.FirstOrDefault(x => x.Name == "Doing");
        var doneList = lists.FirstOrDefault(x => x.Name == "Done");
        Assert.NotNull(todoList);
        Assert.NotNull(doingList);
        Assert.NotNull(doneList);

        //No closed lists
        var listsFiltered = await TrelloClient.GetListsOnBoardFilteredAsync(BoardId, ListFilter.Closed);
        Assert.Empty(listsFiltered);

        //No Cards
        Assert.Empty(await TrelloClient.GetCardsInListAsync(todoList.Id));
        Assert.Empty(await TrelloClient.GetCardsInListAsync(doingList.Id));
        Assert.Empty(await TrelloClient.GetCardsInListAsync(doneList.Id));

        //Default Labels
        var labels = await TrelloClient.GetLabelsOfBoardAsync(BoardId);
        Assert.Equal(6, labels.Count);
    }

    [Fact]
    public async Task ListCanBeAddedAndUpdated()
    {
        var listsBefore = await TrelloClient.GetListsOnBoardAsync(BoardId);
        var newListName = Guid.NewGuid().ToString();
        var addedList = await TrelloClient.AddListAsync(new List(newListName, BoardId));
        Assert.Equal(newListName, addedList.Name);
        var listsAfter = await TrelloClient.GetListsOnBoardAsync(BoardId);
        Assert.Equal(listsBefore.Count + 1, listsAfter.Count);
        var newList = listsAfter.FirstOrDefault(x => x.Name == newListName);
        Assert.NotNull(newList);

        var updatedName = Guid.NewGuid().ToString();
        ;
        newList.Name = updatedName;
        var newListUpdated = await TrelloClient.UpdateListAsync(newList);
        var getnewListViaId = await TrelloClient.GetListAsync(newList.Id);
        Assert.Equal(updatedName, getnewListViaId.Name);
        Assert.Equal(newListUpdated.Name, getnewListViaId.Name);
    }

    [Fact]
    public async Task CardTests()
    {
        var member = (await TrelloClient.GetMembersOfBoardAsync(BoardId)).First();
        var labels = await TrelloClient.GetLabelsOfBoardAsync(BoardId);

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
            labels[1].Id,
            labels[2].Id
        };

        var memberIds = new List<string> { member.Id };
        var addedCard = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Card", "Description", start, due, false, labelIds, memberIds));
        Assert.Equal("Card", addedCard.Name);
        Assert.Equal("Description", addedCard.Description);
        Assert.Equal(start, addedCard.Start);
        Assert.Equal(due, addedCard.Due);
        Assert.False(addedCard.DueComplete);
        Assert.Equal(2, addedCard.Labels.Count);
        Assert.Equal(labels[1].Color, addedCard.Labels[0].Color);
        Assert.Equal(labels[2].Color, addedCard.Labels[1].Color);
        Assert.Single(addedCard.MemberIds);
        Assert.Single(await TrelloClient.GetCardsInListAsync(cardList.Id));

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
        var addedChecklist = await TrelloClient.AddChecklistAsync(getCard.Id, newChecklist);
        var addedChecklistNotTwice = await TrelloClient.AddChecklistAsync(getCard.Id, newChecklist, true);

        Assert.Equal("Sample Checklist", addedChecklist.Name);
        Assert.Equal(3, addedChecklist.Items.Count);
        var itemA = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemA");
        var itemB = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemB");
        var itemC = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemC");

        Assert.NotNull(itemA);
        Assert.NotNull(itemB);
        Assert.NotNull(itemC);

        //Assert.Equal(due, itemA.Due); //This will fail on a free version of Trello so commented out
        //Assert.Equal(member.Id, itemA.MemberId); //This will fail on a free version of Trello so commented out

        Assert.Null(itemB.Due);
        Assert.Null(itemB.MemberId);

        Assert.Null(itemC.Due);
        //Assert.Equal(member.Id, itemC.MemberId); //This will fail on a free version of Trello so commented out

        var doneCard = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Card 2"));
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id);
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
    }

    [Fact]
    public async Task GetBoardRawAsJson()
    {
        var rawGet = await TrelloClient.GetAsync($"boards/{BoardId}");
        Assert.NotNull(rawGet);
    }

    [Fact]
    public async Task GetBoardRaw()
    {
        var rawGetBoard = await TrelloClient.GetAsync<Board>($"boards/{BoardId}");
        Assert.Equal(BoardId, rawGetBoard.Id);
    }
}