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
        [JsonPropertyName(Constants.TrelloIds.CustomFieldFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Id of the CustomField (aka the generic Id of the custom field across all cards)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CustomFieldFields.IdCustomField)]
        [JsonInclude]
        public string CustomFieldId { get; private set; }

        /// <summary>
        /// Value Text of the Option
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CustomFieldFields.Value)]
        [JsonInclude]
        public CustomFieldOptionValue Value { get; private set; }

        /// <summary>
        /// Color of the option
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Color)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CustomFieldOptionColor>))]
        public CustomFieldOptionColor Color { get; private set; }


        /// <summary>
        /// The position of the Option
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        [JsonInclude]
        public decimal Position { get; private set; }
    }
}






