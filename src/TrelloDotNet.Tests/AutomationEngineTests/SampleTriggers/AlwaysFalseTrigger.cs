using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.SampleTriggers;

public class AlwaysFalseTrigger : IAutomationTrigger
{
    public async Task<bool> IsTriggerMetAsync(WebhookAction webhookAction)
    {
        await Task.CompletedTask;
        return false;
    }
}