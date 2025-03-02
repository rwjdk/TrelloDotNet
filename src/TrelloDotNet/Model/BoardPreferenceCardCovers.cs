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
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

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