using System.Linq;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// This Automation Action Removes a Checklist from a Card if it is present
    /// </summary>
    public class RemoveChecklistFromCardAction : IAutomationAction
    {
        /// <summary>
        /// Name of the Checklist to remove
        /// </summary>
        public string ChecklistNameToRemove { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="checklistNameToRemove">Name of the Checklist to remove</param>
        public RemoveChecklistFromCardAction(string checklistNameToRemove)
        {
            ChecklistNameToRemove = checklistNameToRemove;
        }

        /// <summary>
        /// The method called when an automation should be performed
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to the Execution. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <param name="processingResult">An object you can use to report back to the user if the action was performed and details about it</param>
        /// <returns>Void</returns>
        public async Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult)
        {
            var cardId = webhookAction.Data.Card.Id;
            var existingOnCard = await webhookAction.TrelloClient.GetChecklistsOnCardAsync(cardId);
            var existing = existingOnCard.FirstOrDefault(x => x.Name == ChecklistNameToRemove);
            if (existing == null)
            {
                processingResult.AddToLog($"SKIPPED: Checklist '{ChecklistNameToRemove}' was not present on card '{webhookAction.Data.Card.Name}'");
                processingResult.ActionsSkipped++;
            }
            else
            {
                await webhookAction.TrelloClient.DeleteChecklistAsync(existing.Id);
                processingResult.AddToLog($"Removed Checklist '{ChecklistNameToRemove}' from card '{webhookAction.Data.Card.Name}'");
                processingResult.ActionsExecuted++;
            }
        }
    }
}