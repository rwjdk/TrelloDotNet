using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// Trigger that occurs when a Check-item on a Card Change State
    /// </summary>
    public class CheckItemStateUpdatedOnCardTrigger : IAutomationTrigger
    {
        private ChecklistItemState State { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="state">What state should the check-items state be (or leave 'None' to catch both states)</param>
        public CheckItemStateUpdatedOnCardTrigger(ChecklistItemState state = ChecklistItemState.None)
        {
            State = state;
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
            await Task.CompletedTask;
            if (webhookAction.Type != WebhookActionTypes.UpdateCheckItemStateOnCard)
            {
                return false;
            }

            if (State != ChecklistItemState.None)
            {
                return webhookAction.Data?.CheckItem?.State == State;
            }

            return true;
        }
    }
}