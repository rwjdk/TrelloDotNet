using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Webhook Data Attachment
    /// </summary>
    public class WebhookActionDataAttachment
    {
        /// <summary>
        /// Id of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// URL of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Url)]
        [JsonInclude]
        public string Url { get; private set; }
    }
}





