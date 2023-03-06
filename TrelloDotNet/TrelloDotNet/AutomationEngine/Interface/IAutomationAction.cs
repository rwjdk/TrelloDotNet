using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Interface
{
    /// <summary>
    /// Interface for an Automation Action
    /// </summary>
    public interface IAutomationAction
    {
        /// <summary>
        /// The method called when an automation should be performed
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to the Execution. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <param name="processingResult">An object you can use to report back to the user if the action was performed and details about it</param>
        /// <returns>Void</returns>
        Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult);
    }
}