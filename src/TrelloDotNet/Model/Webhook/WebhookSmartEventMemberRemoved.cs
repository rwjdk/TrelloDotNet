namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Arguments for Member Removed
    /// </summary>
    public class WebhookSmartEventMemberRemoved : WebhookSmartEventCardBase
    {
        /// <summary>
        /// Id of the new Member
        /// </summary>
        public string RemovedMemberId { get; }

        /// <summary>
        /// Name of the new Member
        /// </summary>
        public string RemovedMemberName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Webhook Action</param>
        internal WebhookSmartEventMemberRemoved(WebhookAction action) : base(action)
        {
            RemovedMemberId = action.Data.Member.Id;
            RemovedMemberName = action.Data.Member.Name;
        }
    }
}