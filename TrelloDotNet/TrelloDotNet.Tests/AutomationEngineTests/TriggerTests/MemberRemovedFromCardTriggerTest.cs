using TrelloDotNet.AutomationEngine.Model.Triggers;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.TriggerTests;

[Collection("Automation Engine Tests")]
public class MemberRemovedFromCardTriggerTest : TestBase
{
    [Fact]
    public async Task NameAnyTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberRemovedFromCard);
        var trigger = new MemberRemovedFromCardTrigger(MemberRemovedFromCardTriggerConstraint.AnyMember) { TreatMemberNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberRemovedFromCard);
        var trigger = new MemberRemovedFromCardTrigger(MemberRemovedFromCardTriggerConstraint.AnyOfTheseMembersAreRemoved, webhookAction.Data.Member.Name) { TreatMemberNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task AnyButNameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberRemovedFromCard);
        var trigger = new MemberRemovedFromCardTrigger(MemberRemovedFromCardTriggerConstraint.AnyButTheseMembersAreRemoved, webhookAction.Data.Member.Name) { TreatMemberNameAsId = true };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberRemovedFromCard);
        var trigger = new MemberRemovedFromCardTrigger(MemberRemovedFromCardTriggerConstraint.AnyOfTheseMembersAreRemoved, webhookAction.Data.Member.Id) { TreatMemberNameAsId = false };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task TriggerException()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberRemovedFromCard);
        MemberRemovedFromCardTriggerConstraint constraint = Enum.Parse<MemberRemovedFromCardTriggerConstraint>("99");
        var cardMovedToListTrigger = new MemberRemovedFromCardTrigger(constraint, Guid.NewGuid().ToString());
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
}