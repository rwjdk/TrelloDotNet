using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// A Webhook notification from a Webhook that follows a Card (aka idModel is a cardId)
    /// </summary>
    public class WebhookNotificationCard : WebhookNotification
    {
        /// <summary>
        /// The card of the Webhook
        /// </summary>
        [JsonPropertyName("model")]
        [JsonInclude]
        public Card Card { get; private set; }
    }
}