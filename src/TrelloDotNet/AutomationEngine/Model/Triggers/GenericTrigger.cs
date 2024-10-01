using System;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// A Generic Trigger that is determined to be met or not via a function
    /// </summary>
    public class GenericTrigger : IAutomationTrigger
    {
        private readonly Func<WebhookAction, Task<bool>> _isTriggerMet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isTriggerMet">The Function</param>
        public GenericTrigger(Func<WebhookAction, Task<bool>> isTriggerMet)
        {
            _isTriggerMet = isTriggerMet;
        }
        
        /// <summary>
        /// If the Trigger is met
        /// </summary>
        /// <remarks>
        /// While this is built to support async execution, It is best practice to keep a Trigger as light as possible only checking values against the webhook Action and not make any API Calls. The reason for this is that Triggers are called quite often, so it is better to make simple triggers and supplement with Conditions
        /// </remarks>
        /// <param name="webhookAction">The Webhook Action that led to the check of the trigger. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <returns>If trigger is met or not</returns>
        public async Task<bool> IsTriggerMetAsync(WebhookAction webhookAction)
        {
            return await _isTriggerMet.Invoke(webhookAction);
        }
    }
}