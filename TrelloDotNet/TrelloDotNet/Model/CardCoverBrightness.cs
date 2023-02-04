using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The 'Brightness' of the Cover aka if text should be shown black or white on it to contrast image
    /// </summary>
    public enum CardCoverBrightness
    {
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