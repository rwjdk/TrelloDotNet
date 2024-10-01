using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Custom Field Option (Only present for Custom Fields of Type 'List')
    /// </summary>
    public class CustomFieldOption
    {
        /// <summary>
        /// Id of the CustomField Option Value
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Id of the CustomField (aka the generic Id of the custom field across all cards)
        /// </summary>
        [JsonPropertyName("idCustomField")]
        [JsonInclude]
        public string CustomFieldId { get; private set; }

        /// <summary>
        /// Value Text of the Option
        /// </summary>
        [JsonPropertyName("value")]
        [JsonInclude]
        public CustomFieldOptionValue Value { get; private set; }

        /// <summary>
        /// Color of the option
        /// </summary>
        [JsonPropertyName("color")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CustomFieldOptionColor>))]
        public CustomFieldOptionColor Color { get; private set; }


        /// <summary>
        /// The position of the Option
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal Position { get; private set; }
    }
}