namespace TrelloDotNet.AutomationEngine.Model.Conditions
{
    /// <summary>
    /// The Label Constraint in a Condition
    /// </summary>
    public enum LabelConditionConstraint
    {
        /// <summary>
        /// Check if ANY of the provided labels are present on the card
        /// </summary>
        AnyOfThesePresent = 1,
        /// <summary>
        /// Check if ALL of the provided labels are not present on the card
        /// </summary>
        NoneOfTheseArePresent = 2,
        /// <summary>
        /// Check if ALL of the provided labels are present on the card
        /// </summary>
        AllOfThesePresent = 3,
        /// <summary>
        /// Check if there are no labels present at all on the card
        /// </summary>
        NonePresent = 4,
    }
}