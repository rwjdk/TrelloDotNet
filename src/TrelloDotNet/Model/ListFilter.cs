using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [JsonPropertyName(Constants.TrelloIds.ListFields.All)]
        All,

        /// <summary>
        /// Only Closed (Archived) cards
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Closed)]
        Closed,

        /// <summary>
        /// Only open cards (including those on archived lists)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Open)]
        Open
    }
}





