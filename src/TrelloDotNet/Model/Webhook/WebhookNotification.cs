using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Base Webhook Notification (Action Part)
    /// </summary>
    public class WebhookNotification
    {
        /// <summary>
        /// The Action of the Webhook-notification
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Action)]
        [JsonInclude]
        public WebhookAction Action { get; private set; }

        /// <summary>
        /// The Webhook that sent the notification
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Webhook)]
        [JsonInclude]
        public Webhook Webhook { get; private set; }
    }
}



