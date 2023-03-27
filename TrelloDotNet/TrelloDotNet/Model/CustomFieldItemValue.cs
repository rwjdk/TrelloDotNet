using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent the value of the CustomFieldItem
    /// </summary>
    public class CustomFieldItemValue
    {
        /// <summary>
        /// Text Value representation
        /// </summary>
        [JsonPropertyName("text")]
        [JsonInclude]
        public string TextAsString { get; private set; }

        /// <summary>
        /// Number Value representation
        /// </summary>
        [JsonPropertyName("number")]
        [JsonInclude]
        public string NumberAsString { get; private set; }

        /// <summary>
        /// Date Value representation
        /// </summary>
        [JsonPropertyName("date")]
        [JsonInclude]
        public string DateAsString { get; private set; }

        /// <summary>
        /// Checkbox Value representation
        /// </summary>
        [JsonPropertyName("checked")]
        [JsonInclude]
        public string CheckedAsString { get; private set; }
    }
}