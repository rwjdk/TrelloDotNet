using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Options.GetListOptions
{
    /// <summary>
    /// What collection of cards should be returned
    /// </summary>
    public enum GetListOptionsIncludeCards
    {
        /// <summary>
        /// No cards should be included [value in API: none]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.None)]
        None,

        /// <summary>
        /// All card (including archived cards) [value in API: all]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.All)]
        All,

        /// <summary>
        /// Cards that are archived [value in API: closed]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Closed)]
        ArchivedCards,

        /// <summary>
        /// Open cards (including open cards on archived lists) [value in API: open]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Open)]
        OpenCards,
    }
}





