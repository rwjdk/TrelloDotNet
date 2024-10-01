using System.Threading.Tasks;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Interface
{
    /// <summary>
    /// Interface for an condition-implementation
    /// </summary>
    public interface IAutomationCondition
    {
        /// <summary>
        /// Method to check if the condition is met
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to check of the condition. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <returns>If condition is met or not</returns>
        Task<bool> IsConditionMetAsync(WebhookAction webhookAction);
    }
}