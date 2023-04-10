using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.AutomationEngine.Model.Triggers;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.TriggerTests;

[Collection("Automation Engine Tests")]
public class CardMovedToListTriggerTests : TestBase
{
    [Fact]
    public async Task NameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, webhookAction.Data.ListAfter.Name) { TreatListNameAsId = true };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, Guid.NewGuid().ToString()) { TreatListNameAsId = true };
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, webhookAction.Data.ListAfter.Id);
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameStartWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "My")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.StartsWith
        };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task AnyButNameStartWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyButTheseListsAreMovedTo, "My")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.StartsWith
        };
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameEndsWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "st")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.EndsWith
        };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButNameEndsWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyButTheseListsAreMovedTo, "st")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.EndsWith
        };
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameContainsTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "yLi")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.Contains
        };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task AnyButNameContainsTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyButTheseListsAreMovedTo, "yLi")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.Contains
        };
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameRegExTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "M\\w+t")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.RegEx
        };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task AnyButNameRegExTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyButTheseListsAreMovedTo, "M\\w+t")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.RegEx
        };
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, Guid.NewGuid().ToString());
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButThisFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyButTheseListsAreMovedTo, webhookAction.Data.ListAfter.Id);
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButThisTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyButTheseListsAreMovedTo, Guid.NewGuid().ToString());
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task TriggerException()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        CardMovedToListTriggerConstraint constraint = Enum.Parse<CardMovedToListTriggerConstraint>("99");
        var cardMovedToListTrigger = new CardMovedToListTrigger(constraint, Guid.NewGuid().ToString());
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>  await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task WrongWebActionException()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.NoListAfter);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "MyList");
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
}