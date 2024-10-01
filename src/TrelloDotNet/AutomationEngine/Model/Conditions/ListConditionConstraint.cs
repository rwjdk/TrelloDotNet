namespace TrelloDotNet.AutomationEngine.Model.Conditions
{
    /// <summary>
    /// The Constraint of a List Condition
    /// </summary>
    public enum ListConditionConstraint
    {
        /// <summary>
        /// If any of the provided lists is the list of the event
        /// </summary>
        AnyOfTheseLists = 1,

        /// <summary>
        /// If none of the provided lists is the list of the event (used for "Any list but 'xyz'" scenarios)
        /// </summary>
        NoneOfTheseLists = 2
    }
}