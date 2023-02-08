namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// The Argument of a 'OnCardMovedToNewList' Smart event
    /// </summary>
    public class WebhookSmartEventCardMovedToNewList : WebhookSmartEventCardBase
    {
        /// <summary>
        /// The Id of the list the card was moved to
        /// </summary>
        public string NewListId { get; }
        /// <summary>
        /// The name of the list the card was moved to
        /// </summary>
        public string NewListName { get; }
        /// <summary>
        /// The id of the list the card was moved from
        /// </summary>
        public string OldListId { get; }
        /// <summary>
        /// The name of the list the card was moved from
        /// </summary>
        public string OldListName { get; }

        internal WebhookSmartEventCardMovedToNewList(WebhookAction action) : base(action)
        {
            NewListId = action.Data.ListAfter.Id;
            NewListName = action.Data.ListAfter.Name;
            OldListId = action.Data.ListBefore.Id;
            OldListName = action.Data.ListBefore.Name;
        }
    }
}