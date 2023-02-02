using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Filter of Lists
    /// </summary>
    public enum ListFilter
    {
        /// <summary>
        /// All Lists regardless if the are Archived (closed) or not
        /// </summary>
        [JsonPropertyName("all")]
        All,
        /// <summary>
        /// All Archived (closed) lists
        /// </summary>
        [JsonPropertyName("closed")]
        Closed,
        /// <summary>
        /// All open active Lists
        /// </summary>
        [JsonPropertyName("open")]
        Open
    }
}