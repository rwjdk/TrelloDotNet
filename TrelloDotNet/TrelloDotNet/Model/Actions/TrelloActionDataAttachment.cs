using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Data Attachment
    /// </summary>
    public class TrelloActionDataAttachment
    {
        /// <summary>
        /// Id of the Attachment
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Attachment
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// URL of the Attachment
        /// </summary>
        [JsonPropertyName("url")]
        [JsonInclude]
        public string Url { get; private set; }
    }
}