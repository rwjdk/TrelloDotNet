using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.AutomationEngineTests.SampleActions;

public class ExceptionAction : IAutomationAction
{
    public async Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult)
    {
        await Task.CompletedTask;
        throw new Exception("Something bad");
    }
}