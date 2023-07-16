using System.Diagnostics.CodeAnalysis;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Actions;
using Xunit.Abstractions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class CardTests : TestBaseWithNewBoard
{
    private readonly ITestOutputHelper _output;

    public CardTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task CardCrud()
    {
        try
        {
            int step = 1;
            const int totalSteps = 17;
            WaitToAvoidRateLimits();
            await CreateNewBoard();
            var member = (await TrelloClient.GetMembersOfBoardAsync(BoardId)).First();
            var allLabelsOnBoard = await TrelloClient.GetLabelsOfBoardAsync(BoardId);
            var memberIds = new List<string> { member.Id };
            DateTimeOffset? start = new DateTimeOffset(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc));
            DateTimeOffset? due = new DateTimeOffset(new DateTime(2099, 1, 1, 12, 0, 0, DateTimeKind.Utc));

            AddOutput("TestNoCardsExist", ref step, totalSteps);
            await TestNoCardsExist();

            //Create list to test cards on
            WaitToAvoidRateLimits();
            var cardList = await TrelloClient.AddListAsync(new List("List for Card Tests", BoardId));

            AddOutput("TestAttachments", ref step, totalSteps);
            await TestAttachments(cardList);
            return;

            AddOutput("TestAddCard", ref step, totalSteps);
            var addedCard = await TestAddCard(cardList, start, due, memberIds, allLabelsOnBoard);

            AddOutput("TestUpdateCard", ref step, totalSteps);
            var updateCard = await TestUpdateCard(addedCard);

            AddOutput("TestGetCard", ref step, totalSteps);
            var getCard = await TestGetCard(addedCard, updateCard);

            AddOutput("TestChecklists", ref step, totalSteps);
            var doneCard = await TestChecklists(due, member, getCard, cardList);

            AddOutput("TestCardOnBoardAtThisPoint", ref step, totalSteps);
            await TestCardOnBoardAtThisPoint();

            AddOutput("TestDelete", ref step, totalSteps);
            await TestDelete(doneCard);

            AddOutput("TestRawPost", ref step, totalSteps);
            var rawPostCard = await TestRawPost(cardList);

            AddOutput("TestRawPut", ref step, totalSteps);
            await TestRawPut(rawPostCard);

            AddOutput("TestArchiveAndReopen", ref step, totalSteps);
            await TestArchiveAndReopen(cardList);

            AddOutput("TestDates", ref step, totalSteps);
            await TestDates(cardList);

            AddOutput("TestAddRemoveLabels", ref step, totalSteps);
            await TestAddRemoveLabels(cardList, allLabelsOnBoard);

            AddOutput("TestAddRemoveMembers", ref step, totalSteps);
            await TestAddRemoveMembers(cardList, memberIds);

            AddOutput("TestStickers", ref step, totalSteps);
            await TestStickers(cardList);

            AddOutput("TestCustomDelete", ref step, totalSteps);
            await TestCustomDelete(cardList);

            AddOutput("TestCover", ref step, totalSteps);
            await TestCovers(cardList);

            AddOutput("TestComments", ref step, totalSteps);
            await TestComments(cardList);

            AddOutput("TestAttachments", ref step, totalSteps);
            await TestAttachments(cardList);
        }
        finally
        {
            await DeleteBoard();
        }
    }

    private async Task TestAttachments(List cardList)
    {
        Card card = await TrelloClient.AddCardAsync(new Card(cardList.Id, "AttachmentTests"));
        Attachment att1 = await TrelloClient.AddAttachmentToCardAsync(card.Id, new AttachmentUrlLink("https://www.rwj.dk", "Some webpage URL"));

        await using Stream stream = File.Open("TestData" + Path.DirectorySeparatorChar + "TestImage.png", FileMode.Open);
        var attachmentFileUpload = new AttachmentFileUpload(stream, "MyFileName.png", "SomeName");
        Attachment att2 = await TrelloClient.AddAttachmentToCardAsync(card.Id, attachmentFileUpload, true);

        try
        {
            TrelloClient.Options.IncludeAttachmentsInCardGetMethods = true;
            Card cardAfterAttachments = await TrelloClient.GetCardAsync(card.Id);
            Assert.Equal(2, cardAfterAttachments.Attachments.Count);
            Assert.Equal(att2.Id, cardAfterAttachments.AttachmentCover);
        }
        finally
        {
            TrelloClient.Options.IncludeAttachmentsInCardGetMethods = false;
        }
        

        

        var attachments = await TrelloClient.GetAttachmentsOnCardAsync(card.Id);
        Assert.Equal(2, attachments.Count);
        Attachment attachment1 = attachments.Single(x=> x.Id == att1.Id);
        Attachment attachment2 = attachments.Single(x=> x.Id == att2.Id);

        Assert.Equal(att1.Url, attachment1.Url);
        Assert.Equal(att1.Name, attachment1.Name);
        Assert.Equal(att2.FileName, attachment2.FileName);
        Assert.Equal(att2.Name, attachment2.Name);
    }

    private void AddOutput(string description, ref int step, int totalSteps)
    {
        _output.WriteLine($"CardTest - Step {step}/{totalSteps} - {description}");
        step++;
    }

    private async Task<Card> TestGetCard(Card addedCard, Card updateCard)
    {
        WaitToAvoidRateLimits();
        var getCard = await TrelloClient.GetCardAsync(addedCard.Id);
        Assert.Equal(updateCard.Description, getCard.Description);
        Assert.Equal(updateCard.DueComplete, getCard.DueComplete);
        return getCard;
    }

    private async Task TestNoCardsExist()
    {
        //Test No cards exist
        WaitToAvoidRateLimits();
        Assert.Empty(await TrelloClient.GetCardsOnBoardAsync(BoardId));
        WaitToAvoidRateLimits();
        Assert.Empty(await TrelloClient.GetCardsOnBoardFilteredAsync(BoardId, CardsFilter.All));
    }

    private async Task<Card> TestAddCard(List cardList, [DisallowNull] DateTimeOffset? start, [DisallowNull] DateTimeOffset? due, List<string> memberIds, List<Label> allLabelsOnBoard)
    {
        //Add Card
        WaitToAvoidRateLimits();
        var labelIds = new List<string>
        {
            allLabelsOnBoard[1].Id,
            allLabelsOnBoard[2].Id
        };
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
        Assert.Equal(CardCoverSize.Normal, addedCard.Cover.Size);
        Assert.Equal(CardCoverBrightness.Dark, addedCard.Cover.Brightness);
        Assert.Null(addedCard.Cover.Color);
        Assert.Null(addedCard.Cover.BackgroundImageId);
        Assert.Null(addedCard.Cover.BackgroundImageId);
        Assert.Empty(addedCard.ChecklistIds);
        Assert.Null(addedCard.CustomFieldItems);

        WaitToAvoidRateLimits();
        var membersOfCardAsync = await TrelloClient.GetMembersOfCardAsync(addedCard.Id);
        Assert.Single(membersOfCardAsync);
        return addedCard;
    }

    private async Task<Card> TestUpdateCard(Card addedCard)
    {
        //Update Card
        addedCard.DueComplete = true;
        addedCard.Description = "New Description";
        WaitToAvoidRateLimits();
        var updateCard = await TrelloClient.UpdateCardAsync(addedCard);
        Assert.True(updateCard.DueComplete);
        Assert.Equal("New Description", updateCard.Description);
        return updateCard;
    }

    private async Task<Card> TestChecklists([DisallowNull] DateTimeOffset? due, Member member, Card getCard, List cardList)
    {
        //Test Checklists
        WaitToAvoidRateLimits();
        var checklists = await TrelloClient.GetChecklistsOnBoardAsync(BoardId);
        Assert.Empty(checklists);

        var checklistItems = new List<ChecklistItem>
        {
            new("ItemA", due, member.Id),
            new("ItemB"),
            new("ItemC", null, member.Id)
        };
        var newChecklist = new Checklist("Sample Checklist", checklistItems);
        WaitToAvoidRateLimits();
        var addedChecklist = await TrelloClient.AddChecklistAsync(getCard.Id, newChecklist, true);
        WaitToAvoidRateLimits();
        await TrelloClient.AddChecklistAsync(getCard.Id, newChecklist, true);

        Assert.Equal("Sample Checklist", addedChecklist.Name);
        Assert.Equal(getCard.Id, addedChecklist.CardId);
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

        WaitToAvoidRateLimits();
        var doneCard = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Card 2"));
        WaitToAvoidRateLimits();
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true);
        WaitToAvoidRateLimits();
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true);

        //Test adding a checklist with no items
        WaitToAvoidRateLimits();
        await TrelloClient.AddChecklistAsync(doneCard.Id, new Checklist("Empty Checklist"));

        var checklistsNow = await TrelloClient.GetChecklistsOnBoardAsync(BoardId);
        Assert.Equal(3, checklistsNow.Count);

        await TrelloClient.DeleteChecklistAsync(addedChecklist.Id);
        var checklistsNow2 = await TrelloClient.GetChecklistsOnBoardAsync(BoardId);
        Assert.Equal(2, checklistsNow2.Count);

        return doneCard;
    }

    private async Task TestCardOnBoardAtThisPoint()
    {
        WaitToAvoidRateLimits();
        var cardsNow = await TrelloClient.GetCardsOnBoardAsync(BoardId);
        Assert.Equal(2, cardsNow.Count);
    }

    private async Task TestDelete(Card doneCard)
    {
        WaitToAvoidRateLimits();
        await TrelloClient.DeleteCardAsync(doneCard.Id);
        var cardsNowAfterDelete = await TrelloClient.GetCardsOnBoardAsync(BoardId);
        Assert.Single(cardsNowAfterDelete);
    }

    private async Task<Card> TestRawPost(List cardList)
    {
        WaitToAvoidRateLimits();
        var rawPost = await TrelloClient.PostAsync("cards", new QueryParameter("name", "Card"), new QueryParameter("idList", cardList.Id));
        Assert.NotNull(rawPost);

        WaitToAvoidRateLimits();
        var rawPostCard = await TrelloClient.PostAsync<Card>("cards", new QueryParameter("name", "Card"), new QueryParameter("idList", cardList.Id));
        Assert.NotNull(rawPostCard.Id);
        return rawPostCard;
    }

    private async Task TestRawPut(Card rawPostCard)
    {
        WaitToAvoidRateLimits();
        var rawUpdate = await TrelloClient.PutAsync($"cards/{rawPostCard.Id}", new QueryParameter("desc", "New Description"));
        Assert.NotNull(rawUpdate);

        WaitToAvoidRateLimits();
        var rawUpdateCard = await TrelloClient.PutAsync<Card>($"cards/{rawPostCard.Id}", new QueryParameter("desc", "New Description2"));
        Assert.Equal("New Description2", rawUpdateCard.Description);
    }

    private async Task TestArchiveAndReopen(List cardList)
    {
        //Archive and Reopen
        WaitToAvoidRateLimits();
        var cardForArchiveAndReopen = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Card for archive and reopen"));
        Assert.False(cardForArchiveAndReopen.Closed);

        WaitToAvoidRateLimits();
        var archivedCard = await TrelloClient.ArchiveCardAsync(cardForArchiveAndReopen.Id);
        Assert.True(archivedCard.Closed);

        WaitToAvoidRateLimits();
        var reopendCard = await TrelloClient.ReOpenCardAsync(archivedCard.Id);
        Assert.False(reopendCard.Closed);
    }

    private async Task TestDates(List cardList)
    {
        //Test: Set dates
        WaitToAvoidRateLimits();
        var cardWithDates = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Date Card"));
        DateTimeOffset testStartDate = new(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        var testDueDate = testStartDate.AddDays(2);

        WaitToAvoidRateLimits();
        var cardWithStart = await TrelloClient.SetStartDateOnCardAsync(cardWithDates.Id, testStartDate);
        Assert.Equal(testStartDate, cardWithStart.Start);

        WaitToAvoidRateLimits();
        var cardWithDue = await TrelloClient.SetDueDateOnCardAsync(cardWithDates.Id, testDueDate);
        Assert.Equal(testStartDate, cardWithDue.Start);
        Assert.Equal(testDueDate, cardWithDue.Due);

        WaitToAvoidRateLimits();
        var cardWithStartAndDue = await TrelloClient.SetStartDateAndDueDateOnCardAsync(cardWithDates.Id, testStartDate.AddDays(1), testDueDate.AddDays(1), true);
        Assert.Equal(testStartDate.AddDays(1), cardWithStartAndDue.Start);
        Assert.Equal(testDueDate.AddDays(1), cardWithStartAndDue.Due);
    }

    private async Task TestAddRemoveLabels(List cardList, List<Label> allLabelsOnBoard)
    {
        //Test: Add/Remove Labels
        WaitToAvoidRateLimits();
        var cardWithLabels = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Label Card"));
        Assert.Empty(cardWithLabels.LabelIds);

        WaitToAvoidRateLimits();
        var cardWithLabelsAdded = await TrelloClient.AddLabelsToCardAsync(cardWithLabels.Id, allLabelsOnBoard.Select(x => x.Id).ToArray());
        Assert.Equal(allLabelsOnBoard.Count, cardWithLabelsAdded.LabelIds.Count);

        WaitToAvoidRateLimits();
        await TrelloClient.AddLabelsToCardAsync(cardWithLabels.Id, allLabelsOnBoard.Select(x => x.Id).ToArray()); //Call once more to test it can handle added something already there

        WaitToAvoidRateLimits();
        var cardWithSingleLabelsRemoved = await TrelloClient.RemoveLabelsFromCardAsync(cardWithLabels.Id, allLabelsOnBoard.First().Id);
        Assert.Equal(allLabelsOnBoard.Count - 1, cardWithSingleLabelsRemoved.LabelIds.Count);

        WaitToAvoidRateLimits();
        await TrelloClient.RemoveLabelsFromCardAsync(cardWithLabels.Id, allLabelsOnBoard.First().Id); //Call once more to test it can handle removing something already not there

        WaitToAvoidRateLimits();
        var cardWithAllLabelsRemoved = await TrelloClient.RemoveAllLabelsFromCardAsync(cardWithLabels.Id);
        Assert.Empty(cardWithAllLabelsRemoved.LabelIds);

        WaitToAvoidRateLimits();
        await TrelloClient.RemoveAllLabelsFromCardAsync(cardWithLabels.Id); //Call once more to test it can handle removing from already empty
    }

    private async Task TestAddRemoveMembers(List cardList, List<string> memberIds)
    {
        //Test: Add/Remove Members
        WaitToAvoidRateLimits();
        var cardWithMembers = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Member Card"));
        Assert.Empty(cardWithMembers.MemberIds);

        WaitToAvoidRateLimits();
        var cardWithMemberAdded = await TrelloClient.AddMembersToCardAsync(cardWithMembers.Id, memberIds.ToArray());
        Assert.Single(cardWithMemberAdded.MemberIds);

        WaitToAvoidRateLimits();
        await TrelloClient.AddMembersToCardAsync(cardWithMembers.Id, memberIds.ToArray()); //Call once more to test it can handle added something already there

        WaitToAvoidRateLimits();
        var cardWithSingleMemberRemoved = await TrelloClient.RemoveMembersFromCardAsync(cardWithMembers.Id, memberIds.First());
        Assert.Empty(cardWithSingleMemberRemoved.MemberIds);

        WaitToAvoidRateLimits();
        await TrelloClient.RemoveMembersFromCardAsync(cardWithMembers.Id, memberIds.First()); //Call once more to test if remove something not there works

        WaitToAvoidRateLimits();
        await TrelloClient.AddMembersToCardAsync(cardWithMembers.Id, memberIds.ToArray()); //Re-add as test-board only have a single member
        WaitToAvoidRateLimits();
        var cardWithAllMemberRemoved = await TrelloClient.RemoveAllMembersFromCardAsync(cardWithMembers.Id);
        Assert.Empty(cardWithAllMemberRemoved.MemberIds);
        WaitToAvoidRateLimits();
        await TrelloClient.RemoveAllMembersFromCardAsync(cardWithMembers.Id); //call once more to test it can handle already empty member list
    }

    private async Task TestCustomDelete(List cardList)
    {
        //Custom Delete
        WaitToAvoidRateLimits();
        var customDeleteCard = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Custom Delete Card"));

        WaitToAvoidRateLimits();
        await TrelloClient.DeleteAsync($"cards/{customDeleteCard.Id}");
    }

    private async Task TestStickers(List cardList)
    {
        WaitToAvoidRateLimits();
        //Test Sticker CRUD
        var cardForStickerTests = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Sticker Tests"));
        WaitToAvoidRateLimits();
        var stickersPresentJustAfterAdd = await TrelloClient.GetStickersOnCardAsync(cardForStickerTests.Id);
        Assert.Empty(stickersPresentJustAfterAdd);
        var addedSticker = await TrelloClient.AddStickerToCardAsync(cardForStickerTests.Id, new Sticker(StickerDefaultImageId.Clock, 20, 10, 1, 45));
        Assert.NotEmpty(addedSticker.Id);
        Assert.NotEmpty(addedSticker.ImageUrl);
        Assert.Equal(StickerDefaultImageId.Clock, addedSticker.ImageIdAsDefaultEnum);
        Assert.Equal("clock", addedSticker.ImageId);
        Assert.Equal(10, addedSticker.Left);
        Assert.Equal(20, addedSticker.Top);
        Assert.Equal(1, addedSticker.ZIndex);
        Assert.Equal(45, addedSticker.Rotation);
        WaitToAvoidRateLimits();
        var stickersPresentAfterOneAdded = await TrelloClient.GetStickersOnCardAsync(cardForStickerTests.Id);
        Assert.Single(stickersPresentAfterOneAdded);
        WaitToAvoidRateLimits();
        addedSticker.Left = 0;
        addedSticker.Top = 1;
        addedSticker.ZIndex = 2;
        addedSticker.Rotation = 3;
        var updateSticker = await TrelloClient.UpdateStickerAsync(cardForStickerTests.Id, addedSticker);
        Assert.NotEmpty(updateSticker.Id);
        Assert.NotEmpty(updateSticker.ImageUrl);
        Assert.Equal(StickerDefaultImageId.Clock, updateSticker.ImageIdAsDefaultEnum);
        Assert.Equal("clock", updateSticker.ImageId);
        Assert.Equal(0, updateSticker.Left);
        Assert.Equal(1, updateSticker.Top);
        Assert.Equal(2, updateSticker.ZIndex);
        Assert.Equal(3, updateSticker.Rotation);
        WaitToAvoidRateLimits();
        var getStricker = await TrelloClient.GetStickerAsync(cardForStickerTests.Id, updateSticker.Id);
        Assert.Equal(updateSticker.Id, getStricker.Id);
        Assert.Equal(updateSticker.ImageUrl, getStricker.ImageUrl);
        Assert.Equal(StickerDefaultImageId.Clock, getStricker.ImageIdAsDefaultEnum);
        Assert.Equal("clock", getStricker.ImageId);
        Assert.Equal(0, getStricker.Left);
        Assert.Equal(1, getStricker.Top);
        Assert.Equal(2, getStricker.ZIndex);
        Assert.Equal(3, getStricker.Rotation);
        WaitToAvoidRateLimits();
        await TrelloClient.DeleteStickerAsync(cardForStickerTests.Id, getStricker.Id);
        WaitToAvoidRateLimits();
        var stickersPresentAfterDelete = await TrelloClient.GetStickersOnCardAsync(cardForStickerTests.Id);
        Assert.Empty(stickersPresentAfterDelete);
    }

    private async Task TestComments(List cardList)
    {
        //Test Comments
        WaitToAvoidRateLimits();
        var cardForComments = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Comments Tests"));
        // ReSharper disable once RedundantAssignment
#pragma warning disable IDE0059
        var commentInput = new Comment(); //For Test-coverage
#pragma warning restore IDE0059
        commentInput = new Comment("Hello World");
        var comment = await TrelloClient.AddCommentAsync(cardForComments.Id, commentInput);
        Assert.NotEmpty(comment.Id);
        Assert.Equal("Hello World", comment.Data.Text);

        List<TrelloAction> commentsOnCard = await TrelloClient.GetPagedCommentsOnCardAsync(cardForComments.Id);
        Assert.Single(commentsOnCard);
        Assert.Equal(comment.Id, commentsOnCard.First().Id);
        Assert.Equal("Hello World", commentsOnCard.First().Data.Text);

        //Test UpdateComments
        WaitToAvoidRateLimits();
        comment.Data.Text = "New Text";
        var updatedComment = await TrelloClient.UpdateCommentActionAsync(comment);
        Assert.Equal(comment.Id, updatedComment.Id);
        Assert.Equal("New Text", updatedComment.Data.Text);

        //Test Delete Comments
        WaitToAvoidRateLimits();
        await TrelloClient.DeleteCommentActionAsync(comment.Id);

        List<TrelloAction> commentsOnCardAfterDelete = await TrelloClient.GetPagedCommentsOnCardAsync(cardForComments.Id);
        Assert.Empty(commentsOnCardAfterDelete);


        List<TrelloAction> allCommentsOnCardAfterDelete = await TrelloClient.GetAllCommentsOnCardAsync(cardForComments.Id);
        Assert.Empty(allCommentsOnCardAfterDelete);

        for (int i = 0; i < 51; i++)
        {
            await TrelloClient.AddCommentAsync(cardForComments.Id, new Comment("Comment " + i));
        }

        List<TrelloAction> moreThan50Comments = await TrelloClient.GetAllCommentsOnCardAsync(cardForComments.Id);
        Assert.Equal(51, moreThan50Comments.Count);
    }

    private async Task TestCovers(List cardList)
    {
        WaitToAvoidRateLimits();
        var card = await TrelloClient.AddCardAsync(new Card(cardList.Id, "Cover Tests"));
        Assert.Null(card.Cover.Color);
        card.Cover.Color = CardCoverColor.Blue;
        card.Cover.Size = CardCoverSize.Full;

        var updatedCard = await TrelloClient.UpdateCardAsync(card);
        Assert.Equal(CardCoverColor.Blue, updatedCard.Cover.Color);
        Assert.Equal(CardCoverSize.Full, updatedCard.Cover.Size);

        WaitToAvoidRateLimits();

        card.Cover = null;
        var updated2Card = await TrelloClient.UpdateCardAsync(card);
        Assert.Null(updated2Card.Cover.Color);

        var addCoverOnCardAsync = await TrelloClient.AddCoverToCardAsync(updated2Card.Id, new CardCover(CardCoverColor.Lime, CardCoverSize.Normal));
        Assert.Equal(CardCoverColor.Lime, addCoverOnCardAsync.Cover.Color);
        Assert.Equal(CardCoverSize.Normal, addCoverOnCardAsync.Cover.Size);

        WaitToAvoidRateLimits();

        var updateCoverOnCardAsync = await TrelloClient.UpdateCoverOnCardAsync(addCoverOnCardAsync.Id, new CardCover(CardCoverColor.Purple, CardCoverSize.Normal));
        Assert.Equal(CardCoverColor.Purple, updateCoverOnCardAsync.Cover.Color);
        Assert.Equal(CardCoverSize.Normal, updateCoverOnCardAsync.Cover.Size);

        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.UpdateCoverOnCardAsync("", null));

        var removeCoverFromCardAsync = await TrelloClient.RemoveCoverFromCardAsync(updateCoverOnCardAsync.Id);
        Assert.Null(removeCoverFromCardAsync.Cover.Color);
    }
}