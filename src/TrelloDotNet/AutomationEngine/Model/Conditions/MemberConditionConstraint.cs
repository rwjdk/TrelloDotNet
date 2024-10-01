namespace TrelloDotNet.AutomationEngine.Model.Conditions
{
    /// <summary>
    /// The Member Constraint in a Condition
    /// </summary>
    public enum MemberConditionConstraint
    {
        /// <summary>
        /// Check if ANY of the provided Members are present on the card
        /// </summary>
        AnyOfThesePresent = 1,
        /// <summary>
        /// Check if ALL of the provided Members are not present on the card
        /// </summary>
        NoneOfTheseArePresent = 2,
        /// <summary>
        /// Check if ALL of the provided Members are present on the card
        /// </summary>
        AllOfThesePresent = 3,
        /// <summary>
        /// Check if there are no labels Members at all on the card
        /// </summary>
        NonePresent = 4,
    }
}