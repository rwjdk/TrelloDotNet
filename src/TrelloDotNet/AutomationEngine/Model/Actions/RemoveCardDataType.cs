namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Fields to remove values from
    /// </summary>
    public enum RemoveCardDataType
    {
        /// <summary>
        /// Start Date (will be blanked)
        /// </summary>
        StartDate,

        /// <summary>
        /// Due Date (will be blanked)
        /// </summary>
        DueDate,

        /// <summary>
        /// Card Completed (will be marked as False)
        /// </summary>
        DueComplete,

        /// <summary>
        /// Description (will be blanked)
        /// </summary>
        Description,

        /// <summary>
        /// All Labels on the card will be removed
        /// </summary>
        AllLabels,

        /// <summary>
        /// All Members on the card will be removed
        /// </summary>
        AllMembers,

        /// <summary>
        /// All Checklists on the card will be removed
        /// </summary>
        AllChecklists,

        /// <summary>
        /// All Attachments on the card will be removed
        /// </summary>
        AllAttachments,

        /// <summary>
        /// All Comments on the card will be removed
        /// </summary>
        AllComments,

        /// <summary>
        /// Cover of the card will be removed
        /// </summary>
        Cover,

        /// <summary>
        /// Stickers of the card will be removed
        /// </summary>
        AllStickers,
    }
}