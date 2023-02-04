using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Filter of lists on a board
    /// </summary>
    public enum ListFilter
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
        Open
    }
}