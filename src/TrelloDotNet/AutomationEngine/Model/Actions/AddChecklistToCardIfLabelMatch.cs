using System;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// A set of labels and checklist Actions to apply if one or more of the labels are on the card
    /// </summary>
    public class AddChecklistToCardIfLabelMatch
    {
        /// <summary>
        /// Label Ids to check if any of them are on the card
        /// </summary>
        public string[] LabelIdsToMatch { get; }
        
        /// <summary>
        /// Label Ids to check is not present on the card (if present do not add checklist)
        /// </summary>
        public string[] LabelIdsThatCantBePresent { get; }
        
        /// <summary>
        /// Actions to perform if one or more labels exist on the card
        /// </summary>
        public AddChecklistToCardAction[] AddChecklistToCardActions { get; }

        /// <summary>
        /// Set this to 'True' if you supplied names of labels instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and are less resilient to renaming of things.
        /// </summary>
        public bool TreatLabelNameAsId { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="labelIdsToMatch">Label Ids to check if any of them are on the card</param>
        /// <param name="addChecklistToCardActions">Actions to perform if one or more labels exist on the card</param>
        public AddChecklistToCardIfLabelMatch(string[] labelIdsToMatch, params AddChecklistToCardAction[] addChecklistToCardActions)
        {
            LabelIdsToMatch = labelIdsToMatch;
            AddChecklistToCardActions = addChecklistToCardActions;
            LabelIdsThatCantBePresent = Array.Empty<string>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="labelToMatch">Label Id to check if any of them are on the card</param>
        /// <param name="addChecklistToCardActions">Actions to perform if one or more labels exist on the card</param>
        public AddChecklistToCardIfLabelMatch(string labelToMatch, params AddChecklistToCardAction[] addChecklistToCardActions)
        {
            AddChecklistToCardActions = addChecklistToCardActions;
            LabelIdsToMatch = new[] { labelToMatch };
            LabelIdsThatCantBePresent = Array.Empty<string>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="labelIdsToMatch">Label Ids to check if any of them are on the card</param>
        /// <param name="labelIdsThatCantBePresent">Label Ids to check is not present on the card (if present do not add checklist)</param>
        /// <param name="addChecklistToCardActions">Actions to perform if one or more labels exist on the card</param>
        public AddChecklistToCardIfLabelMatch(string[] labelIdsToMatch, string[] labelIdsThatCantBePresent, params AddChecklistToCardAction[] addChecklistToCardActions)
        {
            LabelIdsToMatch = labelIdsToMatch;
            LabelIdsThatCantBePresent = labelIdsThatCantBePresent;
            AddChecklistToCardActions = addChecklistToCardActions;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="labelIdToMatch">Label Id to check if any of them are on the card</param>
        /// <param name="labelIdThatCantBePresent">Label Id that are not allowed to be present on the card</param>
        /// <param name="addChecklistToCardActions">Actions to perform if one or more labels exist on the card</param>
        public AddChecklistToCardIfLabelMatch(string labelIdToMatch, string labelIdThatCantBePresent, params AddChecklistToCardAction[] addChecklistToCardActions)
        {
            AddChecklistToCardActions = addChecklistToCardActions;
            LabelIdsToMatch = new[] { labelIdToMatch };
            LabelIdsThatCantBePresent = new[] { labelIdThatCantBePresent };
        }
    }
}