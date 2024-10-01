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
        [JsonPropertyName("none")]
        None,

        /// <summary>
        /// All Lists (including archived) [value in API: all]
        /// </summary>
        [JsonPropertyName("all")]
        All,

        /// <summary>
        /// Lists that are archived [value in API: closed]
        /// </summary>
        [JsonPropertyName("closed")]
        Closed,

        /// <summary>
        /// Open Lists [value in API: open]
        /// </summary>
        [JsonPropertyName("open")]
        Open,
    }
}