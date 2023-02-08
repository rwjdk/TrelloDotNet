using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Card of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataCard
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
        /// Position
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal? Posistion { get; private set; }

        /// <summary>
        /// If the due work is completed
        /// </summary>
        [JsonPropertyName("dueComplete")]
        [JsonInclude]
        public bool? DueComplete { get; private set; }
    }
}