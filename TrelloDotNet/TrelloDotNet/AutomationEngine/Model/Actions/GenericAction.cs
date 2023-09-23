using System;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// A Generic Action execute a function
    /// </summary>
    public class GenericAction : IAutomationAction
    {
        private readonly Func<WebhookAction, ProcessingResult, Task> _actionToPerform;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="actionToPerform">The Function</param>
        public GenericAction(Func<WebhookAction, ProcessingResult, Task> actionToPerform)
        {
            _actionToPerform = actionToPerform;
        }

        /// <summary>
        /// The method called when an automation should be performed
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to the Execution. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <param name="processingResult">An object you can use to report back to the user if the action was performed and details about it</param>
        /// <returns>Void</returns>
        public async Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult)
        {
            await _actionToPerform.Invoke(webhookAction, processingResult);
        }
    }
}