namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// The Constraints on a CardMoved To List Trigger
    /// </summary>
    public enum CardMovedToListTriggerContraint
    {
        /// <summary>
        /// That any of the provided Lists are being moved to
        /// </summary>
        AnyOfTheseListsAreMovedTo = 1,
        /// <summary>
        /// That any but the provided lists are being moved to (Example, any but the 'Done' Column)
        /// </summary>
        AnyButTheseListsAreMovedTo = 2

    }
}