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
        [JsonPropertyName(Constants.TrelloIds.BoardFields.IdBoard)]
        BoardId,

        /// <summary>
        /// Name of the label
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.LabelFields.Name)]
        Name,

        /// <summary>
        /// Color of the label
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Color)]
        Color,
    }
}






