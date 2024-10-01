using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.SampleConditions;

public class ExceptionCondition : IAutomationCondition
{
    public async Task<bool> IsConditionMetAsync(WebhookAction webhookAction)
    {
        await Task.CompletedTask;
        throw new Exception("Something bad");
    }
}