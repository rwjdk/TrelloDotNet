using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// A Webhook notification from a Webhook that follows a List (aka idModel is a listId)
    /// </summary>
    public class WebhookNotificationList : WebhookNotification
    {
        /// <summary>
        /// The List of the Webhook
        /// </summary>
        [JsonPropertyName("model")]
        [JsonInclude]
        public List List { get; private set; }
    }
}