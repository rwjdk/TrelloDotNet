namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Arguments for Label Removed
    /// </summary>
    public class WebhookSmartEventLabelRemoved : WebhookSmartEventCardBase
    {
        /// <summary>
        /// Id of the new Member
        /// </summary>
        public string RemovedLabelId { get; }
        /// <summary>
        /// Name of the new Member
        /// </summary>
        public string RemovedLabelName { get; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Webhook Action</param>
        internal WebhookSmartEventLabelRemoved(WebhookAction action) : base(action)
        {
            RemovedLabelId = action.Data.Label.Id;
            RemovedLabelName = action.Data.Label.Name;
        }
    }
}