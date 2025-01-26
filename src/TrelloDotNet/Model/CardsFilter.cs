using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Filter of cards on a board
    /// </summary>
    public enum CardsFilter
    {
        /// <summary>
        /// All Cards
        /// </summary>
        [JsonPropertyName("all")]
        All,

        /// <summary>
        /// Only Closed (Archived) cards
        /// </summary>
        [JsonPropertyName("closed")]
        Closed,

        /// <summary>
        /// Only open cards (including those on archived lists)
        /// </summary>
        [JsonPropertyName("open")]
        Open,

        /// <summary>
        /// Only open cards (excluding those on archived lists)
        /// </summary>
        [JsonPropertyName("visible")]
        Visible
    }
}