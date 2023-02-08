using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// List of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataList
    {
        /// <summary>
        /// List Id
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// List Name
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Position (Only present when list is moved)
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal Posistion { get; private set; }
    }
}