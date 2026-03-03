using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Options.GetBoardOptions
{
    /// <summary>
    /// What collection of cards should be returned
    /// </summary>
    public enum GetBoardOptionsIncludeCards
    {
        /// <summary>
        /// No cards should be included [value in API: none]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.None)]
        None,

        /// <summary>
        /// All card (including archived cards) [value in API: all]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.All)]
        OpenAndArchivedCards,

        /// <summary>
        /// Cards that are archived [value in API: closed]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Closed)]
        ArchivedCards,

        /// <summary>
        /// Open cards (including open cards on archived lists) [value in API: open]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Open)]
        OpenCardsOnOpenAndArchivedLists,

        /// <summary>
        /// Open cards on open lists (aka the most normal to use) [value in API: visible]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Visible)]
        OpenCards
    }
}





