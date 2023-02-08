namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Arguments for Checklist Complete
    /// </summary>
    public class WebhookSmartEventChecklistComplete : WebhookSmartEventCardBase
    {
        /// <summary>
        /// Id of the Checklist
        /// </summary>
        public string ChecklistId { get; }
        /// <summary>
        /// Name of the Checklist
        /// </summary>
        public string ChecklistName { get; }
        /// <summary>
        /// Id of the last CheckItem that was completed
        /// </summary>
        public string LastCheckItemCompletedId { get; }
        /// <summary>
        /// Name of the last CheckItem that was completed
        /// </summary>
        public string LastCheckItemCompletedName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Webhook Action</param>
        internal WebhookSmartEventChecklistComplete(WebhookAction action) : base(action)
        {
            ChecklistId = action.Data.Checklist.Id;
            ChecklistName = action.Data.Checklist.Name;
            LastCheckItemCompletedId = action.Data.CheckItem.Id;
            LastCheckItemCompletedName = action.Data.CheckItem.Name;
        }
    }
}