using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Options.GetBoardOptions
{
    /// <summary>
    /// What type of boards should be returned
    /// </summary>
    public enum GetBoardOptionsFilter
    {
        /// <summary>
        /// All Boards regardless if they are open or note
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.All)]
        All,

        /// <summary>
        /// Closed Boards
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Closed)]
        Closed,

        /// <summary>
        /// Open Boards
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Open)]
        Open,

        /// <summary>
        /// Boards current Member have starred
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Starred)]
        Starred
    }
}





