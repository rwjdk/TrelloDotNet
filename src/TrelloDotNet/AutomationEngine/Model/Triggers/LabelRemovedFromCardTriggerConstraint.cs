namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// The Constraint links to a Label Removed Trigger
    /// </summary>
    public enum LabelRemovedFromCardTriggerConstraint
    {
        /// <summary>
        /// Any of these labels are removed
        /// </summary>
        AnyOfTheseLabelsAreRemoved = 1,

        /// <summary>
        /// Any except these labels are removed (Example: Any label that is not the 'Documentation' label)
        /// </summary>
        AnyButTheseLabelsAreRemoved = 2,

        /// <summary>
        /// Any Label is removed
        /// </summary>
        AnyLabel = 3
    }
}