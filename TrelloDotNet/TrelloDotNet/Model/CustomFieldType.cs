using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Type of a Custom Field
    /// </summary>
    public enum CustomFieldType
    {
        /// <summary>
        /// Checkbox Field
        /// </summary>
        [JsonPropertyName("checkbox")]
        Checkbox,
        /// <summary>
        /// List (Dropdown)
        /// </summary>
        [JsonPropertyName("list")]
        List,
        /// <summary>
        /// Numeric Field
        /// </summary>
        [JsonPropertyName("number")]
        Number,
        /// <summary>
        /// Text Field
        /// </summary>
        [JsonPropertyName("text")]
        Text,
        /// <summary>
        /// Date Field
        /// </summary>
        [JsonPropertyName("date")]
        Date
    }
}