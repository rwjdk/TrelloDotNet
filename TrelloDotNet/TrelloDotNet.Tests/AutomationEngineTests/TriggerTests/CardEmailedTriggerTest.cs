using TrelloDotNet.AutomationEngine.Model.Triggers;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.TriggerTests;

[Collection("Automation Engine Tests")]
public class CardEmailedTriggerTest : TestBase
{
    [Fact]
    public async Task TriggerTrue()
    {
        var trigger = new CardEmailedTrigger();
        Assert.True(await trigger.IsTriggerMetAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.CardEmailed)));
    }
    
    [Fact]
    public async Task TriggerFalse()
    {
        var trigger = new CardEmailedTrigger();
        Assert.False(await trigger.IsTriggerMetAsync(WebhookAction.CreateDummy(TrelloClient, WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList)));
    }
}