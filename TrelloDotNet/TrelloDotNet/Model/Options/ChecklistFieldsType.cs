using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Type of field on a Checklist
    /// </summary>
    public enum ChecklistFieldsType
    {
        /// <summary>
        /// Name of the Checklist
        /// </summary>
        [JsonPropertyName("name")]
        Name,

        /// <summary>
        /// Id of the Card the checklist is on
        /// </summary>
        [JsonPropertyName("idCard")]
        CardId,

        /// <summary>
        /// The position of the Checklist on the card
        /// </summary>
        [JsonPropertyName("pos")]
        Position
    }
}