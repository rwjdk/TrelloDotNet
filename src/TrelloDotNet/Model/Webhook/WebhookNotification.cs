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
        [JsonPropertyName("action")]
        [JsonInclude]
        public WebhookAction Action { get; private set; }

        /// <summary>
        /// The Webhook that sent the notification
        /// </summary>
        [JsonPropertyName("webhook")]
        [JsonInclude]
        public Webhook Webhook { get; private set; }
    }
}