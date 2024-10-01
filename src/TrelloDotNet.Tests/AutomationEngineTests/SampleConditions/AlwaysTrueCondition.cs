using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.SampleConditions;

public class AlwaysTrueCondition : IAutomationCondition
{
    public async Task<bool> IsConditionMetAsync(WebhookAction webhookAction)
    {
        await Task.CompletedTask;
        return true;
    }
}