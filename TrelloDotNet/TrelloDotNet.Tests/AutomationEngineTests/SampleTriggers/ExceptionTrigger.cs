using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.SampleTriggers;

public class ExceptionTrigger : IAutomationTrigger
{
    public async Task<bool> IsTriggerMetAsync(WebhookAction webhookAction)
    {
        await Task.CompletedTask;
        throw new Exception("Something bad");
    }
}