using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// This Automation Action Removes a Cover from a card
    /// </summary>
    /// <remarks>
    /// This is often used to warn about something irregular on a Card, Example when it is moved to 'Done'
    /// </remarks>
    public class RemoveCoverFromCardAction : IAutomationAction
    {
        /// <summary>
        /// The method called when an automation should be performed
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to the Execution. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <param name="processingResult">An object you can use to report back to the user if the action was performed and details about it</param>
        /// <returns>Void</returns>
        public async Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult)
        {
            if (webhookAction.Data?.Card == null)
            {
                throw new AutomationException("Could not perform RemoveCoverFromCardAction as WebhookAction did not involve a Card");
            }
            var trelloClient = webhookAction.TrelloClient;
            var card = await webhookAction.Data.Card.GetAsync();
            card.Cover = null;
            await trelloClient.UpdateCardAsync(card);
            processingResult.AddToLog($"Removed Cover from Card '{webhookAction.Data.Card.Name}'");
            processingResult.ActionsExecuted++;
        }
    }
}