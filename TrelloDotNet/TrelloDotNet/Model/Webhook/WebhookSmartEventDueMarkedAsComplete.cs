namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Arguments for Due Marked as Complete
    /// </summary>
    public class WebhookSmartEventDueMarkedAsComplete : WebhookSmartEventCardBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Webhook Action</param>
        internal WebhookSmartEventDueMarkedAsComplete(WebhookAction action) : base(action)
        {
            //Empty
        }
    }
}