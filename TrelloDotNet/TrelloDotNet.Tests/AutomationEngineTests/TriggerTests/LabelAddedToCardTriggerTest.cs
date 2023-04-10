using TrelloDotNet.AutomationEngine.Model.Triggers;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.TriggerTests;

[Collection("Automation Engine Tests")]
public class LabelAddedToCardTriggerTest : TestBase
{
    [Fact]
    public async Task NameAnyTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelAddedToCard);
        var trigger = new LabelAddedToCardTrigger(LabelAddedToCardTriggerConstraint.AnyLabel) { TreatLabelNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelAddedToCard);
        var trigger = new LabelAddedToCardTrigger(LabelAddedToCardTriggerConstraint.AnyOfTheseLabelsAreAdded, webhookAction.Data.Label.Name) { TreatLabelNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task AnyButNameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelAddedToCard);
        var trigger = new LabelAddedToCardTrigger(LabelAddedToCardTriggerConstraint.AnyButTheseLabelsAreAreAdded, webhookAction.Data.Label.Name) { TreatLabelNameAsId = true };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelAddedToCard);
        var trigger = new LabelAddedToCardTrigger(LabelAddedToCardTriggerConstraint.AnyOfTheseLabelsAreAdded, webhookAction.Data.Label.Id) { TreatLabelNameAsId = false };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task TriggerException()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelAddedToCard);
        LabelAddedToCardTriggerConstraint constraint = Enum.Parse<LabelAddedToCardTriggerConstraint>("99");
        var cardMovedToListTrigger = new LabelAddedToCardTrigger(constraint, Guid.NewGuid().ToString());
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
}