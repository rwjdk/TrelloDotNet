using TrelloDotNet.AutomationEngine.Model.Triggers;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.TriggerTests;

public class LabelRemovedFromCardTriggerTest : TestBase
{
    [Fact]
    public async Task NameAnyTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelRemovedFromCard);
        var trigger = new LabelRemovedFromCardTrigger(LabelRemovedFromCardTriggerConstraint.AnyLabel) { TreatLabelNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelRemovedFromCard);
        var trigger = new LabelRemovedFromCardTrigger(LabelRemovedFromCardTriggerConstraint.AnyOfTheseLabelsAreRemoved, webhookAction.Data.Label.Name) { TreatLabelNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task AnyButNameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelRemovedFromCard);
        var trigger = new LabelRemovedFromCardTrigger(LabelRemovedFromCardTriggerConstraint.AnyButTheseLabelsAreRemoved, webhookAction.Data.Label.Name) { TreatLabelNameAsId = true };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelRemovedFromCard);
        var trigger = new LabelRemovedFromCardTrigger(LabelRemovedFromCardTriggerConstraint.AnyOfTheseLabelsAreRemoved, webhookAction.Data.Label.Id) { TreatLabelNameAsId = false };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task TriggerException()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.LabelRemovedFromCard);
        LabelRemovedFromCardTriggerConstraint contraint = Enum.Parse<LabelRemovedFromCardTriggerConstraint>("99");
        var cardMovedToListTrigger = new LabelRemovedFromCardTrigger(contraint, Guid.NewGuid().ToString());
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
}