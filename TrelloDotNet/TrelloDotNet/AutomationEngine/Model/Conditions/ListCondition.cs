using System;
using System.Linq;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Conditions
{
    /// <summary>
    /// Condition that check if a card is on a certain list or the event involved a certain list
    /// </summary>
    public class ListCondition : IAutomationCondition
    {
        /// <summary>
        /// The constraint of the Condition
        /// </summary>
        public ListConditionContraint Contraint { get; }
        /// <summary>
        /// The Ids of the List or Lists to check. Tip: These can be List-names instead of Ids if you set 'TreatListNameAsId' to True
        /// </summary>
        public string[] ListIds { get; }

        /// <summary>
        /// Set this to 'True' if you supplied names of Lists instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and are less resilient to renaming of things.
        /// </summary>
        public bool TreatListNameAsId { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contraint">The constraint of the Condition</param>
        /// <param name="listIds">The Ids of the List or Lists to check. Tip: These can be List-names instead of Ids if you set 'TreatListNameAsId' to True</param>
        public ListCondition(ListConditionContraint contraint, params string[] listIds)
        {
            Contraint = contraint;
            ListIds = listIds;
        }

        /// <summary>
        /// Method to check if the condition is met
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to check of the condition. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <returns>If condition is met or not</returns>
        public async Task<bool> IsConditionMetAsync(WebhookAction webhookAction)
        {
            if (webhookAction.Data?.List != null)
            {
                //List can be checked via Webhook
                var listPartToCheck = TreatListNameAsId ? webhookAction.Data?.List?.Name : webhookAction.Data?.List?.Id;
                switch (Contraint)
                {
                    case ListConditionContraint.AnyOfTheseLists:
                        return ListIds.Contains(listPartToCheck);
                    case ListConditionContraint.NoneOfTheseLists:
                        return !ListIds.Contains(listPartToCheck);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }

            if (webhookAction.Data?.Card != null)
            {
                var card = await webhookAction.TrelloClient.GetCardAsync(webhookAction.Data.Card.Id);
                if (TreatListNameAsId)
                {
                    //Get list of card to check name
                    var list = await webhookAction.TrelloClient.GetListAsync(card.ListId);
                    switch (Contraint)
                    {
                        case ListConditionContraint.AnyOfTheseLists:
                            return ListIds.Contains(list.Name);
                        case ListConditionContraint.NoneOfTheseLists:
                            return !ListIds.Contains(list.Name);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                switch (Contraint)
                {
                    case ListConditionContraint.AnyOfTheseLists:
                        return ListIds.Contains(card.ListId);
                    case ListConditionContraint.NoneOfTheseLists:
                        return !ListIds.Contains(card.ListId);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return false;
        }
    }
}