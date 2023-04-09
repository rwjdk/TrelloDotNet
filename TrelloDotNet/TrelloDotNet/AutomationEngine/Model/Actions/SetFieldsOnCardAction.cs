using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Action to set one or more fields on a card
    /// </summary>
    public class SetFieldsOnCardAction : IAutomationAction
    {
        /// <summary>
        /// List of field-values to set
        /// </summary>
        public ISetCardFieldValue[] FieldValues { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldValues">List of field-values to set</param>
        public SetFieldsOnCardAction(params ISetCardFieldValue[] fieldValues)
        {
            FieldValues = fieldValues;
        }

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
                throw new AutomationException("Could not perform SetFieldsOnCardAction as WebhookAction did not involve a Card");
            }
            var card = await webhookAction.Data.Card.GetAsync();
            bool updateNeeded = false;
            foreach (var fieldValue in FieldValues)
            {
                if (fieldValue.SetIfNeeded(card))
                {
                    updateNeeded = true;
                }
            }

            if (updateNeeded)
            {
                processingResult.ActionsExecuted++;
                await webhookAction.TrelloClient.UpdateCardAsync(card);
            }
            else
            {
                processingResult.ActionsSkipped++;
            }
        }
    }
}