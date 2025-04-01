using TrelloDotNet.Model;
using TrelloDotNet.Model.Actions;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.AddCardFromTemplateOptions;
using TrelloDotNet.Model.Options.AddCardOptions;
using TrelloDotNet.Model.Options.CopyCardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.MirrorCardOptions;
using TrelloDotNet.Model.Options.MoveCardToBoardOptions;
using TrelloDotNet.Model.Options.MoveCardToListOptions;
using TrelloDotNet.Model.Webhook;

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

        Card cardAfterAttachments = await TrelloClient.GetCardAsync(card.Id, new GetCardOptions
        {
            IncludeAttachments = GetCardOptionsIncludeAttachments.True
        });
        Assert.Equal(2, cardAfterAttachments.Attachments.Count);
        Assert.Equal(att2.Id, cardAfterAttachments.AttachmentCover);

        var attachments = await TrelloClient.GetAttachmentsOnCardAsync(card.Id);
        Assert.Equal(2, attachments.Count);
        Attachment attachment1 = attachments.Single(x => x.Id == att1.Id);
        Attachment attachment2 = attachments.Single(x => x.Id == att2.Id);

        Stream stream = await TrelloClient.DownloadAttachmentAsync(card.Id, attachment1.Id);
        Assert.NotNull(stream);

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
        var inputCard = new AddCardOptions(list.Id, Guid.NewGuid().ToString())
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
        var input = new AddCardOptions(list.Id, "Card", "Description")
        {
            Start = start,
            Due = due,
            DueComplete = false,
            LabelIds = labelIds,
            MemberIds = memberIds,
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

        var membersOfCardAsync = await TrelloClient.GetMembersOfCardAsync(addedCard.Id);
        Assert.Single(membersOfCardAsync);
    }

    [Fact]
    public async Task AddCardFull()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        var member = (await TrelloClient.GetMembersOfBoardAsync(_board.Id)).First();
        var allLabelsOnBoard = await TrelloClient.GetLabelsOfBoardAsync(_board.Id);
        await using Stream stream = File.Open("TestData" + Path.DirectorySeparatorChar + "TestImage.png", FileMode.Open);
        await TrelloClient.AddCardAsync(new AddCardOptions
        {
            //Mandatory options
            ListId = list.Id,
            Name = "My Card",

            //Optional options
            Description = "Description of My Card",
            Start = DateTimeOffset.Now,
            Due = DateTimeOffset.Now.AddDays(3),
            Cover = new CardCover(CardCoverColor.Blue, CardCoverSize.Normal),
            LabelIds = allLabelsOnBoard.Select(x => x.Id).ToList(),
            MemberIds = [member.Id],
            Checklists =
            [
                new Checklist("Checklist 1", [
                    new ChecklistItem("Item 1"),
                    new ChecklistItem("Item 2"),
                    new ChecklistItem("Item 3")
                ]),

                new Checklist("Checklist 2", [
                    new ChecklistItem("Item A"),
                    new ChecklistItem("Item B"),
                    new ChecklistItem("Item C")
                ])
            ],
            AttachmentUrlLinks =
            [
                new AttachmentUrlLink("https://www.google.com", "Google")
            ],
            AttachmentFileUploads =
            [
                new AttachmentFileUpload(stream, "X", "Z")
            ]
        });
    }

    [Fact]
    public async Task UpdateCard()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        Card addedCard = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "X", "X"));
        var updateCard = await TrelloClient.UpdateCardAsync(addedCard.Id, [
            CardUpdate.DueComplete(true),
            CardUpdate.Description("New Description"),
            CardUpdate.Name("NewName"),
            CardUpdate.Closed(true),
            CardUpdate.Cover(new CardCover(CardCoverColor.Black, CardCoverSize.Full)),
            CardUpdate.DueDate(DateTimeOffset.UtcNow),
            CardUpdate.StartDate(DateTimeOffset.UtcNow),
            CardUpdate.Position(NamedPosition.Bottom),
            CardUpdate.Members(new List<string>()),
            CardUpdate.Labels(new List<string>()),
            CardUpdate.Board(_board.Id),
            CardUpdate.List(list.Id),
            CardUpdate.IsTemplate(false),
        ]);
        Assert.True(updateCard.DueComplete);
        Assert.Equal("New Description", updateCard.Description);
        updateCard = await TrelloClient.UpdateCardAsync(addedCard.Id, [
            CardUpdate.DueComplete(false),
            CardUpdate.Description("New Description"),
            CardUpdate.Name("NewName"),
            CardUpdate.Closed(false),
            CardUpdate.Cover(new CardCover(CardCoverColor.Black, CardCoverSize.Full)),
            CardUpdate.DueDate(DateTimeOffset.UtcNow),
            CardUpdate.StartDate(DateTimeOffset.UtcNow),
            CardUpdate.Position(1),
            CardUpdate.Members(new List<Member>()),
            CardUpdate.Labels(new List<Label>()),
            CardUpdate.Board(_board),
            CardUpdate.List(list),
            CardUpdate.IsTemplate(true),
        ]);
    }

    [Fact]
    public async Task CopyCard()
    {
        (List list, Card card) = await AddDummyCardAndList(_board.Id);
        Card copy = await TrelloClient.CopyCardAsync(new CopyCardOptions
        {
            Name = "NewName",
            NamedPosition = NamedPosition.Top,
            Keep = CopyCardOptionsToKeep.Due | CopyCardOptionsToKeep.Labels | CopyCardOptionsToKeep.Start,
            SourceCardId = card.Id,
            TargetListId = list.Id
        });

        Assert.NotEqual(copy.Id, card.Id);
        Assert.NotEqual(copy.Name, card.Name);
    }

    [Fact]
    public async Task Checklists()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        Card card = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Card"));

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

        var doneCard = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Card 2"));
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true, cancellationToken: CancellationToken.None);
        await TrelloClient.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true, null);

        //Test adding a checklist with no items
        await TrelloClient.AddChecklistAsync(doneCard.Id, new Checklist("Empty Checklist"));

        var checklistsNow = await TrelloClient.GetChecklistsOnBoardAsync(_board.Id);
        Assert.Equal(3, checklistsNow.Count);

        await Task.Delay(1000);
        Checklist checklist = checklistsNow[0];
        await TrelloClient.DeleteChecklistItemAsync(checklist.Id, checklist.Items[0].Id);
        await Task.Delay(1000);
        var checklistsNowAfterOneDelete = await TrelloClient.GetChecklistsOnBoardAsync(_board.Id);
        Assert.Equal(checklist.Items.Count - 1, checklistsNowAfterOneDelete.First(x => x.Id == checklist.Id).Items.Count);

        await TrelloClient.DeleteChecklistAsync(addedChecklist.Id);
        var checklistsNow2 = await TrelloClient.GetChecklistsOnBoardAsync(_board.Id);
        Assert.Equal(2, checklistsNow2.Count);

        ChecklistItem item = await TrelloClient.AddChecklistItemAsync(checklistsNow2[0].Id, new ChecklistItem("SomeMore"));
        Assert.Equal(checklistsNow2[0].Id, item.ChecklistId);
        Assert.Equal("SomeMore", item.Name);
        Assert.Equal(ChecklistItemState.Incomplete, item.State);

        Checklist updateChecklist = await TrelloClient.UpdateChecklistAsync(checklistsNow2[0].Id, "SomethingNew", NamedPosition.Bottom);
        Assert.Equal("SomethingNew", updateChecklist.Name);
    }

    [Fact]
    public async Task Delete()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        Card card = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Card"));
        await TrelloClient.DeleteCardAsync(card.Id);
        var cardsAfterDelete = await TrelloClient.GetCardsOnBoardAsync(_board.Id);
        Assert.True(cardsAfterDelete.All(x => x.Id != card.Id));
    }

    [Fact]
    public async Task ArchiveAndReopen()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));

        //Archive and Reopen
        var cardForArchiveAndReopen = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Card for archive and reopen"));
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
        var cardWithDates = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Date Card"));
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
        var cardWithLabels = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Label Card"));
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
        var cardWithMembers = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Member Card"));
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
        var customDeleteCard = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Custom Delete Card"));

        await TrelloClient.DeleteAsync($"cards/{customDeleteCard.Id}");
    }

    [Fact]
    public async Task Stickers()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        //Test Sticker CRUD
        var cardForStickerTests = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Sticker Tests"));
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
    public async Task CommentReactions()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        //Test Comments
        var cardForComments = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Comments Tests"));

        var commentInput = new Comment("Hello World");
        var comment = await TrelloClient.AddCommentAsync(cardForComments.Id, commentInput);

        CommentReaction reaction = await TrelloClient.AddCommentReactionAsync(comment.Id, new Reaction
        {
            Native = "👍"
        });

        Assert.Equal("👍", reaction.Emoji.Native);
        Assert.NotNull(reaction.MemberId);
        Assert.NotNull(reaction.EmojiId);
        Assert.NotNull(reaction.Id);
        Assert.NotNull(reaction.CommentId);
        Assert.NotNull(reaction.Member);
        Assert.NotNull(reaction.Member.FullName);
        Assert.NotNull(reaction.Emoji.Name);
        Assert.NotNull(reaction.Emoji.ShortName);
        Assert.NotNull(reaction.Emoji.UnifiedId);

        var reactions = await TrelloClient.GetCommentReactionsAsync(comment.Id);
        Assert.Single(reactions);
        Assert.Equal("👍", reactions[0].Emoji.Native);

        await TrelloClient.DeleteReactionFromCommentAsync(comment.Id, reaction.Id);
        reactions = await TrelloClient.GetCommentReactionsAsync(comment.Id);
        Assert.Empty(reactions);
    }

    [Fact]
    public async Task Comments()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        //Test Comments
        var cardForComments = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Comments Tests"));
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
        var card = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, "Cover Tests"));
        Assert.Null(card.Cover.Color);

        var updatedCard = await TrelloClient.UpdateCoverOnCardAsync(card.Id, new CardCover(CardCoverColor.Blue, CardCoverSize.Full));
        Assert.Equal(CardCoverColor.Blue, updatedCard.Cover.Color);
        Assert.Equal(CardCoverSize.Full, updatedCard.Cover.Size);

        var updated2Card = await TrelloClient.RemoveCoverFromCardAsync(card.Id);
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

    [Fact]
    public async Task MoveCards()
    {
        List list1 = await AddDummyList(_board.Id);
        List list2 = await AddDummyList(_board.Id);

        var card1 = await AddDummyCardToList(list1);
        // ReSharper disable once UnusedVariable
        var card2 = await AddDummyCardToList(list1);
        // ReSharper disable once UnusedVariable
        var card3 = await AddDummyCardToList(list1);

        var card4 = await AddDummyCardToList(list2);
        var card5 = await AddDummyCardToList(list2);
        // ReSharper disable once UnusedVariable
        var card6 = await AddDummyCardToList(list2);

        await TrelloClient.MoveCardToListAsync(card1.Id, list2.Id, new MoveCardToListOptions
        {
            NamedPositionOnNewList = NamedPosition.Top
        });

        var list1Cards = await TrelloClient.GetCardsInListAsync(list1.Id);
        var list2Cards = await TrelloClient.GetCardsInListAsync(list2.Id);

        Assert.Equal(2, list1Cards.Count);
        Assert.Equal(4, list2Cards.Count);

        Assert.Equal(card1.Id, list2Cards.OrderBy(x => x.Position).First().Id);

        await TrelloClient.MoveCardToBottomOfCurrentListAsync(card1.Id);
        list2Cards = await TrelloClient.GetCardsInListAsync(list2.Id);

        Assert.Equal(card4.Id, list2Cards.OrderBy(x => x.Position).First().Id);

        await TrelloClient.MoveCardToTopOfCurrentListAsync(card5.Id);

        list2Cards = await TrelloClient.GetCardsInListAsync(list2.Id);

        Assert.Equal(card5.Id, list2Cards.OrderBy(x => x.Position).First().Id);
    }

    [Fact]
    public async Task GetCardsOnBoard()
    {
        List list1 = await AddDummyList(_board.Id);

        await AddDummyCardToList(list1);
        await AddDummyCardToList(list1);
        await AddDummyCardToList(list1);

        var cards = await TrelloClient.GetCardsOnBoardAsync(_board.Id, new GetCardOptions
        {
            Filter = CardsFilter.All,
            BoardFields = new BoardFields(BoardFieldsType.Name),
            ActionsTypes = new ActionTypesToInclude(WebhookActionTypes.CreateCard),
            CardFields = new CardFields(CardFieldsType.Name),
            Limit = 999,
            OrderBy = CardsOrderBy.CreateDateAsc,
            IncludeBoard = true,
            FilterConditions = [CardsFilterCondition.Name(CardsConditionString.NoneOfThese, Guid.NewGuid().ToString())],
            AdditionalParameters = [new QueryParameter("x", "z")],
            AttachmentFields = AttachmentFields.All,
            Before = "",
            ChecklistFields = ChecklistFields.All,
            IncludeAttachments = GetCardOptionsIncludeAttachments.True,
            IncludeChecklists = true,
            IncludeCustomFieldItems = true,
            IncludeList = true,
            IncludeMemberVotes = true,
            IncludeMembers = true,
            IncludePluginData = true,
            IncludeStickers = true,
            MemberFields = MemberFields.All,
            MembersVotedFields = MemberFields.All,
            Since = "",
            StickerFields = StickerFields.All
        });

        foreach (Card card in cards)
        {
            Assert.NotNull(card.List);
            Assert.NotNull(card.Board);
            Assert.NotNull(card.Checklists);
            Assert.NotNull(card.Stickers);
            Assert.NotNull(card.Actions);
            Assert.NotNull(card.Attachments);
        }
    }

    [Fact]
    public async Task GetCardsForMember()
    {
        List list1 = await AddDummyList(_board.Id);

        await AddDummyCardToList(list1);
        await AddDummyCardToList(list1);
        await AddDummyCardToList(list1);

        Member tokenMemberAsync = await TrelloClient.GetTokenMemberAsync();
        var cards = await TrelloClient.GetCardsForMemberAsync(tokenMemberAsync.Id, new GetCardOptions
        {
            Filter = CardsFilter.All,
            BoardFields = new BoardFields(BoardFieldsType.Name),
            ActionsTypes = new ActionTypesToInclude(WebhookActionTypes.CreateCard),
            CardFields = new CardFields(CardFieldsType.Name),
            Limit = 999,
            OrderBy = CardsOrderBy.CreateDateAsc,
            IncludeBoard = true,
            FilterConditions = [CardsFilterCondition.Name(CardsConditionString.NoneOfThese, Guid.NewGuid().ToString())],
            AdditionalParameters = [new QueryParameter("x", "z")],
            AttachmentFields = AttachmentFields.All,
            Before = "",
            ChecklistFields = ChecklistFields.All,
            IncludeAttachments = GetCardOptionsIncludeAttachments.True,
            IncludeChecklists = true,
            IncludeCustomFieldItems = true,
            IncludeList = true,
            IncludeMemberVotes = true,
            IncludeMembers = true,
            IncludePluginData = true,
            IncludeStickers = true,
            MemberFields = MemberFields.All,
            MembersVotedFields = MemberFields.All,
            Since = "",
            StickerFields = StickerFields.All
        });

        foreach (Card card in cards)
        {
            Assert.NotNull(card.List);
            Assert.NotNull(card.Board);
            Assert.NotNull(card.Checklists);
            Assert.NotNull(card.Stickers);
            Assert.NotNull(card.Actions);
            Assert.NotNull(card.Attachments);
        }
    }

    [Theory]
    [InlineData(false, false, MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndColorAndCreateMissing, MoveCardToBoardOptionsMemberOptions.KeepMembersAlsoOnNewBoardAndRemoveRest)]
    [InlineData(true, true, MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndColorAndCreateMissing, MoveCardToBoardOptionsMemberOptions.KeepMembersAlsoOnNewBoardAndRemoveRest)]
    [InlineData(true, true, MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndColorAndRemoveMissing, MoveCardToBoardOptionsMemberOptions.RemoveAllMembersOnCard)]
    [InlineData(true, true, MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndCreateMissing, MoveCardToBoardOptionsMemberOptions.KeepMembersAlsoOnNewBoardAndRemoveRest)]
    [InlineData(true, true, MoveCardToBoardOptionsLabelOptions.RemoveAllLabelsOnCard, MoveCardToBoardOptionsMemberOptions.RemoveAllMembersOnCard)]
    public async Task MoveCardToBoard(bool removeDueDate, bool removeStartDate, MoveCardToBoardOptionsLabelOptions labelOptions, MoveCardToBoardOptionsMemberOptions memberOptions)
    {
        var existingCards = await TrelloClient.GetCardsOnBoardAsync(_board.Id);
        foreach (Card existingCard in existingCards)
        {
            await TrelloClient.DeleteCardAsync(existingCard.Id);
        }

        var board = new Board("UnitTestBoard-" + Guid.NewGuid().ToString())
        {
            OrganizationId = _board.OrganizationId
        };
        Board secondBoard = await TrelloClient.AddBoardAsync(board);

        List list1 = await AddDummyList(_board.Id);
        List list2 = await AddDummyList(secondBoard.Id);

        var member = (await TrelloClient.GetMembersOfBoardAsync(_board.Id)).First();
        var allLabelsOnBoard = await TrelloClient.GetLabelsOfBoardAsync(_board.Id);
        foreach (Label label in allLabelsOnBoard)
        {
            label.Name = Guid.NewGuid().ToString();
            await TrelloClient.UpdateLabelAsync(label);
        }

        var card1 = await AddDummyCardToList(list1, start: DateTimeOffset.UtcNow, due: DateTimeOffset.UtcNow.AddDays(1));
        await TrelloClient.AddMembersToCardAsync(card1.Id, member.Id);
        await TrelloClient.AddLabelsToCardAsync(card1.Id, allLabelsOnBoard.Select(x => x.Id).ToArray());

        await TrelloClient.MoveCardToBoardAsync(card1.Id, secondBoard.Id, new MoveCardToBoardOptions
        {
            NewListId = list2.Id,
            NamedPositionOnNewList = NamedPosition.Bottom,
            PositionOnNewList = 12,
            LabelOptions = labelOptions,
            MemberOptions = memberOptions,
            RemoveDueDate = removeDueDate,
            RemoveStartDate = removeStartDate
        });

        var cardsOnBoardAsync = await TrelloClient.GetCardsOnBoardAsync(secondBoard.Id);
        Assert.Single(cardsOnBoardAsync);
        var card = cardsOnBoardAsync[0];
        Assert.Equal(card1.Id, card.Id);
        Assert.True(removeDueDate ? card.Due == null : card.Due != null);
        Assert.True(removeStartDate ? card.Start == null : card.Start != null);
        switch (labelOptions)
        {
            case MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndColorAndCreateMissing:
                Assert.Equal(allLabelsOnBoard.Count, card.LabelIds.Count);
                break;
            case MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndRemoveMissing:
                Assert.Equal(allLabelsOnBoard.Count, card.LabelIds.Count);
                break;
            case MoveCardToBoardOptionsLabelOptions.RemoveAllLabelsOnCard:
                Assert.Empty(card.LabelIds);
                break;
        }
    }

    [Fact]
    public async Task MirrorCard()
    {
        var board = new Board("UnitTestBoard-" + Guid.NewGuid())
        {
            OrganizationId = _board.OrganizationId
        };
        Board secondBoard = await TrelloClient.AddBoardAsync(board);

        List sourceList = await AddDummyList(_board.Id);
        List targetList = await AddDummyList(secondBoard.Id);

        var member = (await TrelloClient.GetMembersOfBoardAsync(_board.Id)).First();
        var allLabelsOnBoard = await TrelloClient.GetLabelsOfBoardAsync(_board.Id);

        var sourceCard = await AddDummyCardToList(sourceList,
            start: DateTimeOffset.UtcNow,
            due: DateTimeOffset.UtcNow.AddDays(1),
            description: "Test Description");

        await TrelloClient.AddMembersToCardAsync(sourceCard.Id, member.Id);
        await TrelloClient.AddLabelsToCardAsync(sourceCard.Id, allLabelsOnBoard.Select(x => x.Id).ToArray());

        var mirroredCard = await TrelloClient.MirrorCardAsync(new MirrorCardOptions
        {
            SourceCardId = sourceCard.Id,
            TargetListId = targetList.Id,
            NamedPosition = NamedPosition.Bottom
        });

        Assert.NotEqual(sourceCard.Id, mirroredCard.Id);
        Assert.Equal(sourceCard.ShortUrl, mirroredCard.Name);
        Assert.Equal(targetList.Id, mirroredCard.ListId);
        Assert.Equal(secondBoard.Id, mirroredCard.BoardId);
    }

    [Fact]
    public async Task AddCardFromTemplate()
    {
        List sourceList = await AddDummyList(_board.Id);
        List targetList = await AddDummyList(_board.Id);

        var member = (await TrelloClient.GetMembersOfBoardAsync(_board.Id)).First();
        var allLabelsOnBoard = await TrelloClient.GetLabelsOfBoardAsync(_board.Id);

        var templateCard = await AddDummyCardToList(sourceList,
            start: DateTimeOffset.UtcNow,
            due: DateTimeOffset.UtcNow.AddDays(1),
            description: "Template Description");

        templateCard = await TrelloClient.AddMembersToCardAsync(templateCard.Id, member.Id);
        templateCard = await TrelloClient.AddLabelsToCardAsync(templateCard.Id, allLabelsOnBoard.Select(x => x.Id).ToArray());

        var newCard = await TrelloClient.AddCardFromTemplateAsync(new AddCardFromTemplateOptions
        {
            SourceTemplateCardId = templateCard.Id,
            TargetListId = targetList.Id,
            Name = "New Card From Template",
            NamedPosition = NamedPosition.Bottom,
            Keep = AddCardFromTemplateOptionsToKeep.All
        });

        Assert.NotEqual(templateCard.Id, newCard.Id);
        Assert.Equal("New Card From Template", newCard.Name);
        Assert.Equal(targetList.Id, newCard.ListId);
        Assert.Equal(_board.Id, newCard.BoardId);
        Assert.Equal(templateCard.Description, newCard.Description);
        Assert.Equal(templateCard.Start, newCard.Start);
        Assert.Equal(templateCard.Due, newCard.Due);
        Assert.Equal(templateCard.MemberIds.Count, newCard.MemberIds.Count);
        Assert.Equal(templateCard.LabelIds.Count, newCard.LabelIds.Count);
    }
}