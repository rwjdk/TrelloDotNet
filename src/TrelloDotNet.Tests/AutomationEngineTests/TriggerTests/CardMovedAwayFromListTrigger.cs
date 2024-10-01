using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.AutomationEngine.Model.Triggers;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.TriggerTests;

public class CardMovedAwayFromListTriggerTest : TestBase
{
    [Fact]
    public async Task NameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, webhookAction.Data.ListAfter.Name) { TreatListNameAsId = true };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, Guid.NewGuid().ToString()) { TreatListNameAsId = true };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, webhookAction.Data.ListAfter.Id);
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameStartWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, "My")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.StartsWith
        };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButNameStartWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyButTheseListsAreMovedAwayFrom, "My")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.StartsWith
        };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameEndsWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, "st")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.EndsWith
        };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButNameEndsWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyButTheseListsAreMovedAwayFrom, "st")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.EndsWith
        };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameContainsTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, "yLi")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.Contains
        };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButNameContainsTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyButTheseListsAreMovedAwayFrom, "yLi")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.Contains
        };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameRegExTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, "M\\w+t")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.RegEx
        };
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButNameRegExTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyButTheseListsAreMovedAwayFrom, "M\\w+t")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.RegEx
        };
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, Guid.NewGuid().ToString());
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButThisFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyButTheseListsAreMovedAwayFrom, webhookAction.Data.ListAfter.Id);
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButThisTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyButTheseListsAreMovedAwayFrom, Guid.NewGuid().ToString());
        Assert.True(await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task TriggerException()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        CardMovedAwayFromListTriggerConstraint constraint = Enum.Parse<CardMovedAwayFromListTriggerConstraint>("99");
        var trigger = new CardMovedAwayFromListTrigger(constraint, Guid.NewGuid().ToString());
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await trigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task WrongWebActionException()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.NoListBefore);
        var trigger = new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, "MyList");
        Assert.False(await trigger.IsTriggerMetAsync(webhookAction));
    }
}