namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Arguments for Member Added
    /// </summary>
    public class WebhookSmartEventMemberAdded : WebhookSmartEventCardBase
    {
        /// <summary>
        /// Id of the new Member
        /// </summary>
        public string AddedMemberId { get; }

        /// <summary>
        /// Name of the new Member
        /// </summary>
        public string AddedMemberName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Webhook Action</param>
        internal WebhookSmartEventMemberAdded(WebhookAction action) : base(action)
        {
            AddedMemberId = action.Data.Member.Id;
            AddedMemberName = action.Data.Member.Name;
        }
    }
}