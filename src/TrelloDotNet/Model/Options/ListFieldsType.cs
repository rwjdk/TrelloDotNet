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
        [JsonPropertyName("name")]
        Name,

        /// <summary>
        /// If the List is Archived (closed)
        /// </summary>
        [JsonPropertyName("closed")]
        Closed,

        /// <summary>
        /// ID of Board
        /// </summary>
        [JsonPropertyName("idBoard")]
        BoardId,

        /// <summary>
        /// Color of the List
        /// </summary>
        [JsonPropertyName("color")]
        Color,

        /// <summary>
        /// The position of the list in the current board (lowest = Leftmost)
        /// </summary>
        [JsonPropertyName("pos")]
        Position,

        /// <summary>
        /// If the Board is Subscribed (watched) or not
        /// </summary>
        [JsonPropertyName("subscribed")]
        Subscribed,
    }
}