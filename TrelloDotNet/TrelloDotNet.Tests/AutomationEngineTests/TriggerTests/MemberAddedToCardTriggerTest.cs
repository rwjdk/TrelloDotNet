using TrelloDotNet.AutomationEngine.Model.Triggers;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.TriggerTests;

public class MemberAddedToCardTriggerTest : TestBase
{
    [Fact]
    public async Task NameAnyTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberAddedToCard);
        var trigger = new MemberAddedToCardTrigger(MemberAddedToCardTriggerConstraint.AnyMember) { TreatMemberNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberAddedToCard);
        var trigger = new MemberAddedToCardTrigger(MemberAddedToCardTriggerConstraint.AnyOfTheseMembersAreAdded, webhookAction.Data.Member.Name) { TreatMemberNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task AnyButNameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberAddedToCard);
        var trigger = new MemberAddedToCardTrigger(MemberAddedToCardTriggerConstraint.AnyButTheseMembersAreAreAdded, webhookAction.Data.Member.Name) { TreatMemberNameAsId = true };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberAddedToCard);
        var trigger = new MemberAddedToCardTrigger(MemberAddedToCardTriggerConstraint.AnyOfTheseMembersAreAdded, webhookAction.Data.Member.Id) { TreatMemberNameAsId = false };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task TriggerException()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MemberAddedToCard);
        MemberAddedToCardTriggerConstraint constraint = Enum.Parse<MemberAddedToCardTriggerConstraint>("99");
        var cardMovedToListTrigger = new MemberAddedToCardTrigger(constraint, Guid.NewGuid().ToString());
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
}