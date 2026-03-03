using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Options.GetBoardOptions
{
    /// <summary>
    /// What collection of Lists should be returned
    /// </summary>
    public enum GetBoardOptionsIncludeLists
    {
        /// <summary>
        /// No Lists should be included [value in API: none]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.None)]
        None,

        /// <summary>
        /// All Lists (including archived) [value in API: all]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.All)]
        All,

        /// <summary>
        /// Lists that are archived [value in API: closed]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Closed)]
        Closed,

        /// <summary>
        /// Open Lists [value in API: open]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Open)]
        Open,
    }
}





