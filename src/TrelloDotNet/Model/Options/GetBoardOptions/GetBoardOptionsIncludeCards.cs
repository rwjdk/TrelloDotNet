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
        [JsonPropertyName("none")]
        None,

        /// <summary>
        /// All card (including archived cards) [value in API: all]
        /// </summary>
        [JsonPropertyName("all")]
        OpenAndArchivedCards,

        /// <summary>
        /// Cards that are archived [value in API: closed]
        /// </summary>
        [JsonPropertyName("closed")]
        ArchivedCards,

        /// <summary>
        /// Open cards (including open cards on archived lists) [value in API: open]
        /// </summary>
        [JsonPropertyName("open")]
        OpenCardsOnOpenAndArchivedLists,

        /// <summary>
        /// Open cards on open lists (aka the most normal to use) [value in API: visible]
        /// </summary>
        [JsonPropertyName("visible")]
        OpenCards
    }
}