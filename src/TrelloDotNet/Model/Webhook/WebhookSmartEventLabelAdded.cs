namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Arguments for Label Added
    /// </summary>
    public class WebhookSmartEventLabelAdded : WebhookSmartEventCardBase
    {
        /// <summary>
        /// Id of the new Member
        /// </summary>
        public string AddedLabelId { get; }
        /// <summary>
        /// Name of the new Member
        /// </summary>
        public string AddedLabelName { get; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Webhook Action</param>
        internal WebhookSmartEventLabelAdded(WebhookAction action) : base(action)
        {
            AddedLabelId = action.Data.Label.Id;
            AddedLabelName = action.Data.Label.Name;
        }
    }
}