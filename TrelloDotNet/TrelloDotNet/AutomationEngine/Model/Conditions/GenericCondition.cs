using System;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Conditions
{
    /// <summary>
    /// A Generic Condition that is determined to be met or not via a function
    /// </summary>
    public class GenericCondition : IAutomationCondition
    {
        private readonly Func<WebhookAction, Task<bool>> _isConditionMet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isConditionMet">The Function</param>
        public GenericCondition(Func<WebhookAction, Task<bool>> isConditionMet)
        {
            _isConditionMet = isConditionMet;
        }

        /// <summary>
        /// Method to check if the condition is met
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to check of the condition. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <returns>If condition is met or not</returns>
        public async Task<bool> IsConditionMetAsync(WebhookAction webhookAction)
        {
            return await _isConditionMet.Invoke(webhookAction);
        }
    }
}