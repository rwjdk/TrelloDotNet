using System.Text.Json.Serialization;

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
        /// Unknown option - API Reference does not explain (Create GitHub Issue if you know so we can fix this comment :-))
        /// </summary>
        [JsonPropertyName("visible")]
        Visible
    }
}