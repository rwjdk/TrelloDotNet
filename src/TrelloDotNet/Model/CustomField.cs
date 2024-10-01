using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent the metadata of a CustomField
    /// </summary>
    [DebuggerDisplay("{Name} - '{Type}' (Id: {Id})")]
    public class CustomField
    {
        /// <summary>
        /// Id of the CustomField
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the CustomField
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// The position of the Custom Field
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal Position { get; private set; }

        /// <summary>
        /// The Type of the Custom Field (Checkbox, Date, List, Number or Text)
        /// </summary>
        [JsonPropertyName("type")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CustomFieldType>))]
        public CustomFieldType Type { get; private set; }

        /// <summary>
        /// When the Custom Field was created [stored in UTC]
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);

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

        /// <summary>
        /// FieldGroup (From Trello Docs (https://developer.atlassian.com/cloud/trello/guides/rest-api/getting-started-with-custom-fields/): In order for us to accommodate grouping field values across boards, we needed a way to determine whether or not "different" custom fields should be considered the "same". We have proposed a "matching" logic relies on a generating a unique hash property on the CustomField entity named fieldGroup. The hash (SHA256) is composed of the following custom field entity properties name, and type. The hash is generated when a field is created and re-generated when the field is updated. By comparing the hashes of two fields, we can roughly determine whether the fields are similar enough that we would consider them the same.)
        /// </summary>
        [JsonPropertyName("fieldGroup")]
        [JsonInclude]
        public string FieldGroup { get; private set; }

        /// <summary>
        /// The Display Options of the Custom Field
        /// </summary>
        [JsonPropertyName("display")]
        [JsonInclude]
        public CustomFieldDisplay Display { get; private set; }

        /// <summary>
        /// If the Field Is Suggested (Not documented in docs what this represent :-( )
        /// </summary>
        [JsonPropertyName("isSuggestedField")]
        [JsonInclude]
        public bool IsSuggestedField { get; private set; }
        
        /// <summary>
        /// Options of the Custom field (Only present for Custom Fields of Type 'List')
        /// </summary>
        [JsonPropertyName("options")]
        [JsonInclude]
        public List<CustomFieldOption> Options { get; private set; }
    }
}