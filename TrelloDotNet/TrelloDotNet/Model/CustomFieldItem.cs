using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a CustomFieldItem (aka the one holding a value)
    /// </summary>
    [DebuggerDisplay("{Value} (Id: {Id})")]
    public class CustomFieldItem
    {
        /// <summary>
        /// Id of the CustomFieldItem (Unique here, not to be confused with CustomFieldId that is the generic Id of the field)
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The value of the Custom Id (Depending on it's type)
        /// </summary>
        [JsonPropertyName("value")]
        [JsonInclude]
        public CustomFieldItemValue Value { get; private set; }

        /// <summary>
        /// The Id of the Value (if the field is type dropdown; else raw value is in the Value-object)
        /// </summary>
        [JsonPropertyName("idValue")]
        [JsonInclude]
        public string ValueId { get; private set; }

        /// <summary>
        /// Id of the CustomField (aka the generic Id of the custom field across all cards)
        /// </summary>
        [JsonPropertyName("idCustomField")]
        [JsonInclude]
        public string CustomFieldId { get; private set; }

        /// <summary>
        /// Id of the type the item is linked to (Card, Board or Member)
        /// </summary>
        [JsonPropertyName("idModel")]
        [JsonInclude]
        public string ModelId { get; private set; }

        /// <summary>
        /// The type the ModelId (Card, Board or Member)
        /// </summary>
        [JsonPropertyName("modelType")]
        [JsonInclude]
        public string ModelType { get; private set; }
    }
}