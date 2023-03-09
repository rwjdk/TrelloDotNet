using System.Linq;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// This Automation Action adds a specific Checklist to a card if it is not already present.
    /// </summary>
    /// <remarks>
    /// This is often used for automation of Definition of Done
    /// </remarks>
    public class AddChecklistToCardAction : IAutomationAction
    {
        /// <summary>
        /// The Checklist Object to Add (if a checklist with same name is not already present)
        /// </summary>
        public Checklist ChecklistToAdd { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="checklistToAdd">The Checklist Object to Add (if a checklist with same name is not already present)</param>
        public AddChecklistToCardAction(Checklist checklistToAdd)
        {
            ChecklistToAdd = checklistToAdd;
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
            var existing = existingOnCard.FirstOrDefault(x => x.Name == ChecklistToAdd.Name);
            if (existing == null)
            {
                await webhookAction.TrelloClient.AddChecklistAsync(cardId, ChecklistToAdd);
                processingResult.AddToLog($"Added Checklist '{ChecklistToAdd.Name}' to card '{webhookAction.Data.Card.Name}'");
                processingResult.ActionsExecuted++;
            }
            else
            {
                processingResult.AddToLog($"SKIPPED: Checklist '{ChecklistToAdd.Name}' was already on card '{webhookAction.Data.Card.Name}'");
                processingResult.ActionsSkipped++;
            }
        }
    }
}