using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// If the Check Completed Status should be shown on the Card Front
    /// </summary>
    public enum BoardPreferenceShowCompletedStatusOnCardFront
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
        /// Do not show
        /// </summary>
        [JsonPropertyName("false")]
        DoNotShow
    }
}