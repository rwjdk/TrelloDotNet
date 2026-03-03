using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// The Type of field on a board
    /// </summary>
    public enum ListFieldsType
    {
        /// <summary>
        /// Name of the board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Name)]
        Name,

        /// <summary>
        /// If the List is Archived (closed)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Closed)]
        Closed,

        /// <summary>
        /// ID of Board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.IdBoard)]
        BoardId,

        /// <summary>
        /// Color of the List
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Color)]
        Color,

        /// <summary>
        /// The position of the list in the current board (lowest = Leftmost)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        Position,

        /// <summary>
        /// If the Board is Subscribed (watched) or not
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Subscribed)]
        Subscribed,
    }
}






