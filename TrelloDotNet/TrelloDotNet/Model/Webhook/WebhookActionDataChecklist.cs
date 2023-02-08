using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Checklist of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataChecklist
    {
        /// <summary>
        /// Checklist Id
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Checklist Name
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }
    }
}