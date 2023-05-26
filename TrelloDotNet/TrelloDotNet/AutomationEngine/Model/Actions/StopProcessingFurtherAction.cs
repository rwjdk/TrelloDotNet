using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// An action that will stop any further processing of automations after this one for the given Webhook Receive Request
    /// </summary>
    public class StopProcessingFurtherAction : IAutomationAction
    {
        /// <summary>
        /// The method called when an automation should be performed
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to the Execution. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <param name="processingResult">An object you can use to report back to the user if the action was performed and details about it</param>
        /// <returns>Void</returns>
        public Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult)
        {
            processingResult.Log.Add(new ProcessingResultLogEntry("StopProcessingFurtherAction was called."));
            throw new StopProcessingFurtherActionException();
        }
    }
}