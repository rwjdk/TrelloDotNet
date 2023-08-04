using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.AutomationEngine.Model.Actions;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.ActionTests;

public class ActionTests : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string? _boardId;

    public ActionTests(TestFixtureWithNewBoard fixture)
    {
        _boardId = fixture.BoardId;
    }

    [Fact]
    public async Task TestRemoveChecklistToCardAction()
    {
        var list = await TrelloClient.AddListAsync(new List(Guid.NewGuid().ToString(), _boardId));
        var card = await TrelloClient.AddCardAsync(new Card(list.Id, "Some Card"));
        var checklist = new Checklist("My Checklist", new List<ChecklistItem> { new("A"), new("B") });
        await TrelloClient.AddChecklistAsync(card.Id, checklist);
        var processingResult = new ProcessingResult();
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardUpdated, cardToSimulate: card);

        IAutomationAction action = new RemoveChecklistFromCardAction(checklist.Name);
        await action.PerformActionAsync(webhookAction, processingResult);
        await action.PerformActionAsync(webhookAction, processingResult);
        var checklists = await TrelloClient.GetChecklistsOnCardAsync(card.Id);

        Assert.Empty(checklists);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(1, processingResult.ActionsSkipped);

        await Assert.ThrowsAsync<AutomationException>(async () => await action.PerformActionAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated), processingResult));
    }

    [Fact]
    public async Task TestAddChecklistToCardAction()
    {
        var list = await TrelloClient.AddListAsync(new List(Guid.NewGuid().ToString(), _boardId));
        var card = await TrelloClient.AddCardAsync(new Card(list.Id, "Some Card"));
        var processingResult = new ProcessingResult();
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardUpdated, cardToSimulate: card);

        var checklistToAdd = new Checklist("My Checklist", new List<ChecklistItem> { new("A"), new("B")});
        IAutomationAction action = new AddChecklistToCardAction(checklistToAdd);

        await action.PerformActionAsync(webhookAction, processingResult);
        var checklists = await TrelloClient.GetChecklistsOnCardAsync(card.Id);
        Assert.Single(checklists);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(0, processingResult.ActionsSkipped);

        await action.PerformActionAsync(webhookAction, processingResult);
        checklists = await TrelloClient.GetChecklistsOnCardAsync(card.Id);
        Assert.Single(checklists);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(1, processingResult.ActionsSkipped);

        await Assert.ThrowsAsync<AutomationException>(async () => await action.PerformActionAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated), processingResult));
    }

    [Fact]
    public async Task TestAddStickerToCardAction()
    {
        var list = await TrelloClient.AddListAsync(new List(Guid.NewGuid().ToString(), _boardId));
        var card = await TrelloClient.AddCardAsync(new Card(list.Id, "Some Card"));
        var processingResult = new ProcessingResult();
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardUpdated, cardToSimulate: card);

        const StickerDefaultImageId stickerDefaultImageId = StickerDefaultImageId.Check;
        IAutomationAction action = new AddStickerToCardAction(new Sticker(stickerDefaultImageId));
        await action.PerformActionAsync(webhookAction, processingResult);
        var stickers = await TrelloClient.GetStickersOnCardAsync(card.Id);
        Assert.Single(stickers);
        Assert.Equal(stickerDefaultImageId, stickers.First().ImageIdAsDefaultEnum);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(0, processingResult.ActionsSkipped);

        await action.PerformActionAsync(webhookAction, processingResult);
        stickers = await TrelloClient.GetStickersOnCardAsync(card.Id);
        Assert.Single(stickers);
        Assert.Equal(stickerDefaultImageId, stickers.First().ImageIdAsDefaultEnum);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(1, processingResult.ActionsSkipped);


        await Assert.ThrowsAsync<AutomationException>(async () => await action.PerformActionAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated), processingResult));
    }

    [Fact]
    public async Task TestRemoveCoverFromCardAction()
    {
        var list = await TrelloClient.AddListAsync(new List(Guid.NewGuid().ToString(), _boardId));
        var card = await TrelloClient.AddCardAsync(new Card(list.Id, "Some Card"));
        await TrelloClient.AddCoverToCardAsync(card.Id, new CardCover(CardCoverColor.Lime, CardCoverSize.Normal));

        var processingResult = new ProcessingResult();
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardUpdated, cardToSimulate: card);

        IAutomationAction action = new RemoveCoverFromCardAction();
        await action.PerformActionAsync(webhookAction, processingResult);
        var cardAfterAction = await TrelloClient.GetCardAsync(card.Id);
        Assert.Null(cardAfterAction.Cover.Color);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(0, processingResult.ActionsSkipped);

        await Assert.ThrowsAsync<AutomationException>(async () => await action.PerformActionAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated), processingResult));
    }

    [Fact]
    public async Task TestSetFieldsOnCardAction()
    {
        var list = await TrelloClient.AddListAsync(new List(Guid.NewGuid().ToString(), _boardId));
        const string orgName = "Some Card";
        const string someDescription = "Some Description";
        var card = await TrelloClient.AddCardAsync(new Card(list.Id, orgName, someDescription));
        await TrelloClient.AddStickerToCardAsync(card.Id, new Sticker(StickerDefaultImageId.Check));
        var processingResult = new ProcessingResult();
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardUpdated, cardToSimulate: card);

        const string name = "x **NAME**";
        const string description = "y **DESCRIPTION**";
        var start = DateTimeOffset.UtcNow;
        var due = start.AddDays(2);
        const bool dueComplete = true;
        IAutomationAction action = new SetFieldsOnCardAction(
            new SetCardNameFieldValue(name),
            new SetCardDescriptionFieldValue(description, SetFieldsOnCardValueCriteria.OverwriteAnyPreviousValue),
            new SetCardStartFieldValue(start),
            new SetCardDueFieldValue(due),
            new SetCardDueCompleteFieldValue(dueComplete));

        await action.PerformActionAsync(webhookAction, processingResult);

        var cardAfterAction = await TrelloClient.GetCardAsync(card.Id);
        Assert.Equal("x Some Card",cardAfterAction.Name);
        Assert.Equal("y Some Description",cardAfterAction.Description);
        Assert.Equal(start.Date,cardAfterAction.Start!.Value.Date);
        Assert.Equal(due.Date,cardAfterAction.Due!.Value.Date);
        Assert.Equal(dueComplete,cardAfterAction.DueComplete);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(0, processingResult.ActionsSkipped);

        IAutomationAction actionThatIsSkipped = new SetFieldsOnCardAction(
            new SetCardNameFieldValue(name+"xxx", SetFieldsOnCardValueCriteria.OnlySetIfBlank),
            new SetCardDescriptionFieldValue(description+"yyy"),
            new SetCardStartFieldValue(start.AddDays(1)),
            new SetCardDueFieldValue(due.AddDays(1)));

        await actionThatIsSkipped.PerformActionAsync(webhookAction, processingResult);

        var cardAfterSkipAction = await TrelloClient.GetCardAsync(card.Id);
        Assert.Equal("x Some Card", cardAfterSkipAction.Name);
        Assert.Equal("y Some Description", cardAfterSkipAction.Description);
        Assert.Equal(start.Date, cardAfterSkipAction.Start!.Value.Date);
        Assert.Equal(due.Date, cardAfterSkipAction.Due!.Value.Date);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(1, processingResult.ActionsSkipped);

        cardAfterSkipAction.Name = string.Empty;
        cardAfterSkipAction.Description = string.Empty;
        await TrelloClient.UpdateCardAsync(cardAfterSkipAction);

        IAutomationAction actionThatIsOverwrite = new SetFieldsOnCardAction(
            new SetCardNameFieldValue("xxx", SetFieldsOnCardValueCriteria.OnlySetIfBlank),
            new SetCardDescriptionFieldValue("yyy"),
            new SetCardStartFieldValue(start.AddDays(1), SetFieldsOnCardValueCriteria.OverwriteAnyPreviousValue),
            new SetCardDueFieldValue(due.AddDays(1), SetFieldsOnCardValueCriteria.OverwriteAnyPreviousValue));

        await actionThatIsOverwrite.PerformActionAsync(webhookAction, processingResult);

        var cardAfterOverwriteAction = await TrelloClient.GetCardAsync(card.Id);
        Assert.Equal("xxx", cardAfterOverwriteAction.Name);
        Assert.Equal("yyy", cardAfterOverwriteAction.Description);
        Assert.Equal(start.AddDays(1).Date, cardAfterOverwriteAction.Start!.Value.Date);
        Assert.Equal(due.AddDays(1).Date, cardAfterOverwriteAction.Due!.Value.Date);
        Assert.Equal(2, processingResult.ActionsExecuted);
        Assert.Equal(1, processingResult.ActionsSkipped);

        await Assert.ThrowsAsync<AutomationException>(async () => await action.PerformActionAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated), processingResult));
    }

    [Fact]
    private async Task TestRemoveStickerFromCardAction()
    {
        var list = await TrelloClient.AddListAsync(new List(Guid.NewGuid().ToString(), _boardId));
        var card = await TrelloClient.AddCardAsync(new Card(list.Id, "Some Card"));
        await TrelloClient.AddStickerToCardAsync(card.Id, new Sticker(StickerDefaultImageId.Check));
        var processingResult = new ProcessingResult();
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardUpdated, cardToSimulate: card);

        IAutomationAction action = new RemoveStickerFromCardAction(StickerDefaultImageId.Check);
        await action.PerformActionAsync(webhookAction, processingResult);

        action = new RemoveStickerFromCardAction("check"); //Second call to test no sticker exists
        await action.PerformActionAsync(webhookAction, processingResult); 
        var stickers = await TrelloClient.GetStickersOnCardAsync(card.Id);

        Assert.Empty(stickers);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(1, processingResult.ActionsSkipped);

        await Assert.ThrowsAsync<AutomationException>(async () => await action.PerformActionAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated), processingResult));
    }

    [Fact]
    private async Task AddCoverOnCardAction()
    {
        var list = await TrelloClient.AddListAsync(new List(Guid.NewGuid().ToString(), _boardId));
        var card = await TrelloClient.AddCardAsync(new Card(list.Id, "Some Card"));
        var processingResult = new ProcessingResult();
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardUpdated, cardToSimulate: card);

        const CardCoverColor cardCoverColor = CardCoverColor.Black;
        const CardCoverSize cardCoverSize = CardCoverSize.Full;
        IAutomationAction action = new AddCoverOnCardAction(new CardCover(cardCoverColor, cardCoverSize));
        await action.PerformActionAsync(webhookAction, processingResult);
        var cardAfterPerformAction = await TrelloClient.GetCardAsync(card.Id);
        Assert.Equal(cardCoverColor, cardAfterPerformAction.Cover.Color);
        Assert.Equal(cardCoverSize, cardAfterPerformAction.Cover.Size);
        Assert.Equal(1, processingResult.ActionsExecuted);
        Assert.Equal(0, processingResult.ActionsSkipped);

        await Assert.ThrowsAsync<AutomationException>(async () => await action.PerformActionAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.BoardUpdated), processingResult));
    }
}