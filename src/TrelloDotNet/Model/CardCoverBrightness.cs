using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The 'Brightness' of the Cover aka if text should be shown black or white on it to contrast image
    /// </summary>
    public enum CardCoverBrightness
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("null")]
        None = 0,

        /// <summary>
        /// Dark
        /// </summary>
        [JsonPropertyName("dark")]
        Dark,

        /// <summary>
        /// Light
        /// </summary>
        [JsonPropertyName("light")]
        Light
    }
}