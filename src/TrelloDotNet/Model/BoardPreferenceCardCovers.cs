using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// If Card Covers should be shown or not
    /// </summary>
    public enum BoardPreferenceCardCovers
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        Unknown,

        /// <summary>
        /// Show
        /// </summary>
        [JsonPropertyName("true")]
        Show,

        /// <summary>
        /// Do Not Show
        /// </summary>
        [JsonPropertyName("false")]
        DoNotShow
    }
}