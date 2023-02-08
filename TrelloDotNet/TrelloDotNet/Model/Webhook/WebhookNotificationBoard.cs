using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// A Webhook notification from a Webhook that follows a Board (aka idModel is a boardId)
    /// </summary>
    public class WebhookNotificationBoard : WebhookNotification
    {
        /// <summary>
        /// The Board of the Webhook
        /// </summary>
        [JsonPropertyName("model")]
        [JsonInclude]
        public Board Board { get; private set; }
    }
}