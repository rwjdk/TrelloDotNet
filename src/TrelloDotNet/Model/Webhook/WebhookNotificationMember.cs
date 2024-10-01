using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// A Webhook notification from a Webhook that follows a Member (aka idModel is a memberId)
    /// </summary>
    public class WebhookNotificationMember : WebhookNotification
    {
        /// <summary>
        /// The member of the Webhook
        /// </summary>
        [JsonPropertyName("model")]
        [JsonInclude]
        public Member Member { get; private set; }
    }
}