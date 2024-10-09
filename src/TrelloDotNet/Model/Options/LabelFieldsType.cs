using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Type of field on a card
    /// </summary>
    public enum LabelFieldsType
    {
        /// <summary>
        /// Id of the board the label is on
        /// </summary>
        [JsonPropertyName("idBoard")]
        BoardId,

        /// <summary>
        /// Name of the label
        /// </summary>
        [JsonPropertyName("name")]
        Name,

        /// <summary>
        /// Color of the label
        /// </summary>
        [JsonPropertyName("color")]
        Color,
    }
}