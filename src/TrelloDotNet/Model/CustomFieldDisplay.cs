using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Display options of a Custom Field
    /// </summary>
    public class CustomFieldDisplay
    {
        /// <summary>
        /// If the Field is shown on the front of card or not
        /// </summary>
        [JsonPropertyName("cardFront")]
        [JsonInclude]
        public bool ShowFieldOnFrontOfCard { get; private set; }
    }
}