namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// BaseCard-class of a SmartEvent Argument
    /// </summary>
    public class WebhookSmartEventCardBase : WebhookSmartEventBase
    {
        /// <summary>
        /// Id of the Card
        /// </summary>
        public string CardId { get; }

        /// <summary>
        /// Name of the Card
        /// </summary>
        public string CardName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">The Webhook Action</param>
        protected WebhookSmartEventCardBase(WebhookAction action) : base(action)
        {
            CardId = action.Data.Card.Id;
            CardName = action.Data.Card.Name;
        }
    }
}