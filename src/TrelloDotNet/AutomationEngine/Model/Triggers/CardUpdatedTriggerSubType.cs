namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// Allow you to specify the subtype of the generic CardUpdated Event (Example: if it was the name that was updated, the due date, or something else)
    /// </summary>
    public enum CardUpdatedTriggerSubType
    {
        /// <summary>
        /// Name of Card changed
        /// </summary>
        NameChanged,

        /// <summary>
        /// Description of Card changed
        /// </summary>
        DescriptionChanged,

        /// <summary>
        /// A Due Date was added
        /// </summary>
        DueDateAdded,

        /// <summary>
        /// A Due Date was changed
        /// </summary>
        DueDateChanged,

        /// <summary>
        /// The Due Date was marked as complete
        /// </summary>
        DueDateMarkedAsComplete,

        /// <summary>
        /// The Due Date was marked as incomplete
        /// </summary>
        DueDateMarkedAsIncomplete,

        /// <summary>
        /// The Due Date was removed
        /// </summary>
        DueDateRemoved,

        /// <summary>
        /// A Start Date was added
        /// </summary>
        StartDateAdded,

        /// <summary>
        /// The Start Date was changed
        /// </summary>
        StartDateChanged,

        /// <summary>
        /// The Start Date was Removed
        /// </summary>
        StartDateRemoved,

        /// <summary>
        /// Card was move between two lists (Tip: for a more detailed trigger use 'CardMovedToListTrigger' or 'CardMovedAwayFromListTrigger')
        /// </summary>
        MovedToOtherList,

        /// <summary>
        /// Card was moved to a higher up position in its list
        /// </summary>
        MovedHigherInList,

        /// <summary>
        /// Card was moved to a lower down position in its list
        /// </summary>
        MovedLowerInList,

        /// <summary>
        /// Card was Archived
        /// </summary>
        Archived,

        /// <summary>
        /// Card was un-archived (aka sent to board)
        /// </summary>
        Unarchived,
    }
}