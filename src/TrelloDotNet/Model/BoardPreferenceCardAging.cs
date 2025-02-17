using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Card Aging type
    /// </summary>
    public enum BoardPreferenceCardAging
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        Unknown,

        /// <summary>
        /// Regular
        /// </summary>
        [JsonPropertyName("regular")]
        Regular,

        /// <summary>
        /// Pirate
        /// </summary>
        [JsonPropertyName("pirate")]
        Pirate,
    }
}