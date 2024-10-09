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
        [JsonPropertyName("all")]
        All,

        /// <summary>
        /// Closed Boards
        /// </summary>
        [JsonPropertyName("closed")]
        Closed,

        /// <summary>
        /// Open Boards
        /// </summary>
        [JsonPropertyName("open")]
        Open,

        /// <summary>
        /// Boards current Member have starred
        /// </summary>
        [JsonPropertyName("starred")]
        Starred
    }
}