﻿using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.AutomationEngine.Model.Triggers;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests;

public class CardMovedToListTriggerTests : TestBase
{
    [Fact]
    public async Task NameTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyOfTheseListsAreMovedTo, webhookAction.Data.ListAfter.Name) { TreatListNameAsId = true };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task NameFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyOfTheseListsAreMovedTo, Guid.NewGuid().ToString()) { TreatListNameAsId = true };
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task IdTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyOfTheseListsAreMovedTo, webhookAction.Data.ListAfter.Id);
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task NameStartWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyOfTheseListsAreMovedTo, "My")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.StartsWith
        };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task NameEndsWithTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyOfTheseListsAreMovedTo, "st")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.EndsWith
        };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task NameContainsTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyOfTheseListsAreMovedTo, "yLi")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.Contains
        };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task NameregExTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyOfTheseListsAreMovedTo, "M\\w+t")
        {
            TreatListNameAsId = true,
            ListNameMatchCriteria = StringMatchCriteria.RegEx
        };
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
    
    [Fact]
    public async Task IdFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyOfTheseListsAreMovedTo, Guid.NewGuid().ToString());
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButThisFalseTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyButTheseListsAreMovedTo, webhookAction.Data.ListAfter.Id);
        Assert.False(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }

    [Fact]
    public async Task AnyButThisTrueTrigger()
    {
        var webhookAction = WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList);
        var cardMovedToListTrigger = new CardMovedToListTrigger(CardMovedToListTriggerContraint.AnyButTheseListsAreMovedTo, Guid.NewGuid().ToString());
        Assert.True(await cardMovedToListTrigger.IsTriggerMetAsync(webhookAction));
    }
}