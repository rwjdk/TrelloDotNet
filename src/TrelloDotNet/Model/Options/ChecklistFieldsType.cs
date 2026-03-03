using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [JsonPropertyName(Constants.TrelloIds.ListFields.Name)]
        Name,

        /// <summary>
        /// Id of the Card the checklist is on
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdCard)]
        CardId,

        /// <summary>
        /// The position of the Checklist on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        Position
    }
}





