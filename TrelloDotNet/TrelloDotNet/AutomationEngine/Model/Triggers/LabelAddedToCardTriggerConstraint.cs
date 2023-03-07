namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// The Constraint links to a Label Added Trigger
    /// </summary>
    public enum LabelAddedToCardTriggerConstraint
    {
        /// <summary>
        /// Trigger is true if any of the provided Labels are added
        /// </summary>
        AnyOfTheseLabelsAreAdded = 1,
        
        /// <summary>
        /// Trigger is true if any but the provided Labels are added (Example: Any label but the 'Documentation' label)
        /// </summary>
        AnyButTheseLabelsAreAreAdded = 2,

        /// <summary>
        /// Trigger is true if any label is added (no matter which)
        /// </summary>
        AnyLabel = 3
    }
}