using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Board of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataBoard
    {
        /// <summary>
        /// Id of the Board
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the board
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Short Version of Id for the Board
        /// </summary>
        [JsonPropertyName("shortLink")]
        [JsonInclude]
        public string ShortLink { get; private set; }
    }
}