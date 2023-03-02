using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Action Data about CheckItems
    /// </summary>
    public class WebhookActionDataCheckItem
    {
        /// <summary>
        /// Id of the Item 
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Item
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// State of the item (incomplete or complete)
        /// </summary>
        [JsonPropertyName("state")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<ChecklistItemState>))]
        public ChecklistItemState State { get; private set; }
    }
}