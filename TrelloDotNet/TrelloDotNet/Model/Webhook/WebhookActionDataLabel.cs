using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Label of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataLabel
    {
        /// <summary>
        /// Label Id
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Label Name
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Color of Label
        /// </summary>
        [JsonPropertyName("color")]
        [JsonInclude]
        public string Color { get; private set; }
    }
}