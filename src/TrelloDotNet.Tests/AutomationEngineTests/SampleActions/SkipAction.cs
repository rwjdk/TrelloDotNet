using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.SampleActions;

public class SkipAction : IAutomationAction
{
    public async Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult)
    {
        await Task.CompletedTask;
        processingResult.ActionsSkipped++;
    }
}