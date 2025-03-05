using System;
using System.Globalization;
using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        /// Text Value
        /// </summary>
        [JsonIgnore]
        public string Text => TextAsString;

        /// <summary>
        /// Number Value representation
        /// </summary>
        [JsonPropertyName("number")]
        [JsonInclude]
        public string NumberAsString { get; private set; }

        /// <summary>
        /// Number Value
        /// </summary>
        [JsonIgnore]
        public decimal? Number => string.IsNullOrWhiteSpace(NumberAsString) ? new decimal?() : decimal.Parse(NumberAsString, NumberStyles.Number, CultureInfo.InvariantCulture);

        /// <summary>
        /// Date Value representation
        /// </summary>
        [JsonPropertyName("date")]
        [JsonInclude]
        public string DateAsString { get; private set; }

        /// <summary>
        /// Date Value
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Date => string.IsNullOrWhiteSpace(DateAsString) ? new DateTimeOffset?() : DateTimeOffset.ParseExact(DateAsString, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None);
        //2024-09-18T10:00:00.000Z

        /// <summary>
        /// Checkbox Value representation
        /// </summary>
        [JsonPropertyName("checked")]
        [JsonInclude]
        public string CheckedAsString { get; private set; }

        /// <summary>
        /// Checkbox Value
        /// </summary>
        [JsonIgnore]
        public bool Checked => CheckedAsString == "true";
    }
}