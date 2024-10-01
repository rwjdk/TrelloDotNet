using TrelloDotNet.Model;
using TrelloDotNet.Model.Actions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class CardTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board = fixture.Board!;

    [Fact]
    public async Task Attachments()
    {
        Card card = await AddDummyCard(_board.Id, "Attachments");
        Attachment att1 = await TrelloClient.AddAttachmentToCardAsync(card.Id, new AttachmentUrlLink("https://www.rwj.dk", "Some webpage URL"));

        Attachment att2;
        try
        {
            att2 = await AddImageAttachment(card);
        }
        catch
        {
            try
            {
                await Task.Delay(1000);
                att2 = await AddImageAttachment(card);
            }
            catch
            {
                await Task.Delay(1000);
                att2 = await AddImageAttachment(card);
            }
        }

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
        Attachment attachment1 = attachments.Single(x => x.Id == att1.Id);
        Attachment attachment2 = attachments.Single(x => x.Id == att2.Id);

        Assert.Equal(att1.Url, attachment1.Url);
        Assert.Equal(att1.Name, attachment1.Name);
        Assert.Equal(att1.IsUpload, attachment1.IsUpload);
        Assert.Equal(att1.EdgeColor, attachment1.EdgeColor);
        Assert.Equal(att1.Bytes, attachment1.Bytes);
        Assert.Equal(att1.Position, attachment1.Position);
        Assert.Equal(att1.Created, attachment1.Created);
        Assert.Equal(att1.MemberId, attachment1.MemberId);
        Assert.Equal(att1.MimeType, attachment1.MimeType);
        Assert.Equal(att1.Date, attachment1.Date);


        Assert.Equal(att2.FileName, attachment2.FileName);
        Assert.Equal(att2.Name, attachment2.Name);

        await TrelloClient.DeleteAttachmentOnCardAsync(card.Id, attachment1.Id);
        var attachmentsAfterDelete = await TrelloClient.GetAttachmentsOnCardAsync(card.Id);
        Assert.Single(attachmentsAfterDelete);
        attachment2 = attachmentsAfterDelete.Single(x => x.Id == att2.Id);
        Assert.Equal(att2.FileName, attachment2.FileName);
        Assert.Equal(att2.Name, attachment2.Name);
    }

    private async Task<Attachment> AddImageAttachment(Card card)
    {
        await using Stream stream = File.Open("TestData" + Path.DirectorySeparatorChar + "TestImage.png", FileMode.Open);
        var attachmentFileUpload = new AttachmentFileUpload(stream, "MyFileName.png", "SomeName");
        Attachment att2 = await TrelloClient.AddAttachmentToCardAsync(card.Id, attachmentFileUpload, true);
        return att2;
    }

    [Fact]
    public async Task GetCard()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        var inputCard = new Card(list.Id, Guid.NewGuid().ToString())
        {
            Due = DateTimeOffset.Now,
            Description = Guid.NewGuid().ToString()
        };
        Card addCard = await TrelloClient.AddCardAsync(inputCard);
        var getCard = await TrelloClient.GetCardAsync(addCard.Id);
        Assert.Equal(addCard.Description, getCard.Description);
        Assert.Equal(addCard.DueComplete, getCard.DueComplete);
    }

    [Fact]
    public async Task AddCard()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        var member = (await TrelloClient.GetMembersOfBoardAsync(_board.Id)).First();
        var memberIds = new List<string> { member.Id };
        var allLabelsOnBoard = await TrelloClient.GetLabelsOfBoardAsync(_board.Id);
        DateTimeOffset? start = new DateTimeOffset(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        DateTimeOffset? due = new DateTimeOffset(new DateTime(2099, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        //Add Card
        var labelIds = new List<string>
        {
            allLabelsOnBoard[1].Id,
            allLabelsOnBoard[2].Id
        };
        var input = new Card(list.Id, "Card", "Description", start, due, false, labelIds, memberIds)
        {
            Cover = new CardCover(CardCoverColor.Blue, CardCoverSize.Normal)
        };
        var addedCard = await TrelloClient.AddCardAsync(input);
        AssertTimeIsNow(addedCard.Created);
        AssertTimeIsNow(addedCard.LastActivity);
        Assert.Equal("Card", addedCard.Name);
        Assert.Equal("Description", addedCard.Description);
        Assert.Equal(start, addedCard.Start);
        Assert.Equal(due, addedCard.Due);
        Assert.NotNull(addedCard.Cover);
        Assert.Equal(CardCoverColor.Blue, addedCard.Cover.Color);
        Assert.Equal(CardCoverSize.Normal, addedCard.Cover.Size);
        Assert.False(addedCard.DueComplete);
        Assert.Equal(2, addedCard.Labels.Count);
        Assert.Equal(allLabelsOnBoard[1].Color, addedCard.Labels[0].Color);
        Assert.Equal(allLabelsOnBoard[2].Color, addedCard.Labels[1].Color);
        Assert.Single(addedCard.MemberIds);
        Assert.Single(await TrelloClient.GetCardsInListAsync(list.Id));
        Assert.NotEmpty(addedCard.Url);
        Assert.NotEmpty(addedCard.ShortUrl);
        Assert.NotEmpty(addedCard.IdShort.ToString());
        Assert.NotNull(addedCard.Cover);
        Assert.Equal(CardCoverSize.Normal, addedCard.Cover.Size);
        Assert.Equal(CardCoverBrightness.Dark, addedCard.Cover.Brightness);
        Assert.Null(addedCard.Cover.BackgroundImageId);
        Assert.Empty(addedCard.ChecklistIds);
        Assert.Null(addedCard.CustomFieldItems);


        var membersOfCardAsync = await TrelloClient.GetMembersOfCardAsync(addedCard.Id);
        Assert.Single(membersOfCardAsync);
    }

    [Fact]
    public async Task UpdateCard()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        Card addedCard = await TrelloClient.AddCardAsync(new Card(list.Id, "X", "X"));
        //Update Card
        addedCard.DueComplete = true;
        addedCard.Description = "New Description";
        var updateCard = await TrelloClient.UpdateCardAsync(addedCard);
        Assert.True(updateCard.DueComplete);
        Assert.Equal("New Description", updateCard.Description);
    }

    [Fact]
    public async Task Checklists()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        Card card = await TrelloClient.AddCardAsync(new Card(list.Id, "Card"));

        var member = (await TrelloClient.GetMembersOfBoardAsync(_board.Id)).First();
        DateTimeOffset? due = new DateTimeOffset(new DateTime(2099, 1, 1, 12, 0, 0, DateTimeKind.Utc));

        var checklists = await TrelloClient.GetChecklistsOnBoardAsync(_board.Id);
        Assert.Empty(checklists);

        var checklistItems = new List<ChecklistItem>
        {
            new("ItemA", due, member.Id),
            new("ItemB"),
            new("ItemC", null, member.Id)
        };
        var newChecklist = new Checklist("Sample Checklist", checklistItems);
        var addedChecklist = await TrelloClient.AddChecklistAsync(card.Id, newChecklist, true);
        await TrelloClient.AddChecklistAsync(card.Id, newChecklist, true);

        Assert.Equal("Sample Checklist", addedChecklist.Name);
        Assert.Equal(card.Id, addedChecklist.CardId);
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

        var doneCard = await TrelloClient.AddCardAsync(new Card(list.Id, "Card 2"));
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true, null);
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true, null);

        //Test adding a checklist with no items
        await TrelloClient.AddChecklistAsync(doneCard.Id, new Checklist("Empty Checklist"));

        var checklistsNow = await TrelloClient.GetChecklistsOnBoardAsync(_board.Id);
        Assert.Equal(3, checklistsNow.Count);

        await TrelloClient.DeleteChecklistItemAsync(checklistsNow[0].Id, checklistsNow[0].Items[0].Id);
        var checklistsNowAfterOneDelete = await TrelloClient.GetChecklistsOnBoardAsync(_board.Id);
        Assert.Equal(checklistsNow[0].Items.Count - 1, checklistsNowAfterOneDelete[0].Items.Count);

        await TrelloClient.DeleteChecklistAsync(addedChecklist.Id);
        var checklistsNow2 = await TrelloClient.GetChecklistsOnBoardAsync(_board.Id);
        Assert.Equal(2, checklistsNow2.Count);
    }

    [Fact]
    public async Task Delete()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        Card card = await TrelloClient.AddCardAsync(new Card(list.Id, "Card"));
        await TrelloClient.DeleteCardAsync(card.Id);
        var cardsAfterDelete = await TrelloClient.GetCardsOnBoardAsync(_board.Id);
        Assert.True(cardsAfterDelete.All(x => x.Id != card.Id));
    }

    [Fact]
    public async Task ArchiveAndReopen()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));

        //Archive and Reopen
        var cardForArchiveAndReopen = await TrelloClient.AddCardAsync(new Card(list.Id, "Card for archive and reopen"));
        Assert.False(cardForArchiveAndReopen.Closed);

        var archivedCard = await TrelloClient.ArchiveCardAsync(cardForArchiveAndReopen.Id);
        Assert.True(archivedCard.Closed);

        var reopendCard = await TrelloClient.ReOpenCardAsync(archivedCard.Id);
        Assert.False(reopendCard.Closed);
    }

    [Fact]
    public async Task Dates()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        //Test: Set dates
        var cardWithDates = await TrelloClient.AddCardAsync(new Card(list.Id, "Date Card"));
        DateTimeOffset testStartDate = new(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        var testDueDate = testStartDate.AddDays(2);

        var cardWithStart = await TrelloClient.SetStartDateOnCardAsync(cardWithDates.Id, testStartDate);
        Assert.Equal(testStartDate, cardWithStart.Start);

        var cardWithDue = await TrelloClient.SetDueDateOnCardAsync(cardWithDates.Id, testDueDate);
        Assert.Equal(testStartDate, cardWithDue.Start);
        Assert.Equal(testDueDate, cardWithDue.Due);

        var cardWithStartAndDue = await TrelloClient.SetStartDateAndDueDateOnCardAsync(cardWithDates.Id, testStartDate.AddDays(1), testDueDate.AddDays(1), true);
        Assert.Equal(testStartDate.AddDays(1), cardWithStartAndDue.Start);
        Assert.Equal(testDueDate.AddDays(1), cardWithStartAndDue.Due);
    }

    [Fact]
    public async Task AddRemoveLabels()
    {
        var allLabelsOnBoard = await TrelloClient.GetLabelsOfBoardAsync(_board.Id);
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        //Test: Add/Remove Labels
        var cardWithLabels = await TrelloClient.AddCardAsync(new Card(list.Id, "Label Card"));
        Assert.Empty(cardWithLabels.LabelIds);

        var cardWithLabelsAdded = await TrelloClient.AddLabelsToCardAsync(cardWithLabels.Id, allLabelsOnBoard.Select(x => x.Id).ToArray());
        Assert.Equal(allLabelsOnBoard.Count, cardWithLabelsAdded.LabelIds.Count);

        await TrelloClient.AddLabelsToCardAsync(cardWithLabels.Id, allLabelsOnBoard.Select(x => x.Id).ToArray()); //Call once more to test it can handle added something already there

        var cardWithSingleLabelsRemoved = await TrelloClient.RemoveLabelsFromCardAsync(cardWithLabels.Id, allLabelsOnBoard.First().Id);
        Assert.Equal(allLabelsOnBoard.Count - 1, cardWithSingleLabelsRemoved.LabelIds.Count);

        await TrelloClient.RemoveLabelsFromCardAsync(cardWithLabels.Id, allLabelsOnBoard.First().Id); //Call once more to test it can handle removing something already not there

        var cardWithAllLabelsRemoved = await TrelloClient.RemoveAllLabelsFromCardAsync(cardWithLabels.Id);
        Assert.Empty(cardWithAllLabelsRemoved.LabelIds);

        await TrelloClient.RemoveAllLabelsFromCardAsync(cardWithLabels.Id); //Call once more to test it can handle removing from already empty
    }

    [Fact]
    public async Task AddRemoveMembers()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        var member = (await TrelloClient.GetMembersOfBoardAsync(_board.Id)).First();
        var memberIds = new List<string> { member.Id };
        //Test: Add/Remove Members
        var cardWithMembers = await TrelloClient.AddCardAsync(new Card(list.Id, "Member Card"));
        Assert.Empty(cardWithMembers.MemberIds);

        var cardWithMemberAdded = await TrelloClient.AddMembersToCardAsync(cardWithMembers.Id, memberIds.ToArray());
        Assert.Single(cardWithMemberAdded.MemberIds);

        await TrelloClient.AddMembersToCardAsync(cardWithMembers.Id, memberIds.ToArray()); //Call once more to test it can handle added something already there

        var cardWithSingleMemberRemoved = await TrelloClient.RemoveMembersFromCardAsync(cardWithMembers.Id, memberIds.First());
        Assert.Empty(cardWithSingleMemberRemoved.MemberIds);

        await TrelloClient.RemoveMembersFromCardAsync(cardWithMembers.Id, memberIds.First()); //Call once more to test if remove something not there works

        await TrelloClient.AddMembersToCardAsync(cardWithMembers.Id, memberIds.ToArray()); //Re-add as test-board only have a single member
        var cardWithAllMemberRemoved = await TrelloClient.RemoveAllMembersFromCardAsync(cardWithMembers.Id);
        Assert.Empty(cardWithAllMemberRemoved.MemberIds);
        await TrelloClient.RemoveAllMembersFromCardAsync(cardWithMembers.Id); //call once more to test it can handle already empty member list
    }

    [Fact]
    public async Task CustomDelete()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        //Custom Delete
        var customDeleteCard = await TrelloClient.AddCardAsync(new Card(list.Id, "Custom Delete Card"));

        await TrelloClient.DeleteAsync($"cards/{customDeleteCard.Id}");
    }

    [Fact]
    public async Task Stickers()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        //Test Sticker CRUD
        var cardForStickerTests = await TrelloClient.AddCardAsync(new Card(list.Id, "Sticker Tests"));
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
        var stickersPresentAfterOneAdded = await TrelloClient.GetStickersOnCardAsync(cardForStickerTests.Id);
        Assert.Single(stickersPresentAfterOneAdded);
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
        var getStricker = await TrelloClient.GetStickerAsync(cardForStickerTests.Id, updateSticker.Id);
        Assert.Equal(updateSticker.Id, getStricker.Id);
        Assert.Equal(updateSticker.ImageUrl, getStricker.ImageUrl);
        Assert.Equal(StickerDefaultImageId.Clock, getStricker.ImageIdAsDefaultEnum);
        Assert.Equal("clock", getStricker.ImageId);
        Assert.Equal(0, getStricker.Left);
        Assert.Equal(1, getStricker.Top);
        Assert.Equal(2, getStricker.ZIndex);
        Assert.Equal(3, getStricker.Rotation);
        await TrelloClient.DeleteStickerAsync(cardForStickerTests.Id, getStricker.Id);
        var stickersPresentAfterDelete = await TrelloClient.GetStickersOnCardAsync(cardForStickerTests.Id);
        Assert.Empty(stickersPresentAfterDelete);
    }

    [Fact]
    public async Task Comments()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        //Test Comments
        var cardForComments = await TrelloClient.AddCardAsync(new Card(list.Id, "Comments Tests"));
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
        comment.Data.Text = "New Text";
        var updatedComment = await TrelloClient.UpdateCommentActionAsync(comment);
        Assert.Equal(comment.Id, updatedComment.Id);
        Assert.Equal("New Text", updatedComment.Data.Text);

        //Test Delete Comments
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

    [Fact]
    public async Task Covers()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        var card = await TrelloClient.AddCardAsync(new Card(list.Id, "Cover Tests"));
        Assert.Null(card.Cover.Color);
        card.Cover.Color = CardCoverColor.Blue;
        card.Cover.Size = CardCoverSize.Full;

        var updatedCard = await TrelloClient.UpdateCardAsync(card);
        Assert.Equal(CardCoverColor.Blue, updatedCard.Cover.Color);
        Assert.Equal(CardCoverSize.Full, updatedCard.Cover.Size);

        card.Cover = null;
        var updated2Card = await TrelloClient.UpdateCardAsync(card);
        Assert.Null(updated2Card.Cover.Color);

        var addCoverOnCardAsync = await TrelloClient.AddCoverToCardAsync(updated2Card.Id, new CardCover(CardCoverColor.Lime, CardCoverSize.Normal));
        Assert.Equal(CardCoverColor.Lime, addCoverOnCardAsync.Cover.Color);
        Assert.Equal(CardCoverSize.Normal, addCoverOnCardAsync.Cover.Size);

        var updateCoverOnCardAsync = await TrelloClient.UpdateCoverOnCardAsync(addCoverOnCardAsync.Id, new CardCover(CardCoverColor.Purple, CardCoverSize.Normal));
        Assert.Equal(CardCoverColor.Purple, updateCoverOnCardAsync.Cover.Color);
        Assert.Equal(CardCoverSize.Normal, updateCoverOnCardAsync.Cover.Size);

        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.UpdateCoverOnCardAsync("", null));

        var removeCoverFromCardAsync = await TrelloClient.RemoveCoverFromCardAsync(updateCoverOnCardAsync.Id);
        Assert.Null(removeCoverFromCardAsync.Cover.Color);
    }
}