namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// The Constraints on a CardMoved Away From List Trigger
    /// </summary>
    public enum CardMovedAwayFromListTriggerConstraint
    {
        /// <summary>
        /// That any of the provided Lists are being moved away from
        /// </summary>
        AnyOfTheseListsAreMovedAwayFrom = 1,

        /// <summary>
        /// That any but the provided lists are being moved away from (Example, any but the 'Done' Column)
        /// </summary>
        AnyButTheseListsAreMovedAwayFrom = 2
    }
}