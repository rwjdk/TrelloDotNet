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
        [JsonPropertyName(Constants.TrelloIds.CardFields.All)]
        All,

        /// <summary>
        /// Only Closed (Archived) cards
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Closed)]
        Closed,

        /// <summary>
        /// Only open cards (including those on archived lists)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Open)]
        Open,

        /// <summary>
        /// Only open cards (excluding those on archived lists)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Visible)]
        Visible
    }
}





