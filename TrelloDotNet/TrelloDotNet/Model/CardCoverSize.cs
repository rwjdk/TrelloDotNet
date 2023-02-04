using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Size of the Card Cover
    /// </summary>
    public enum CardCoverSize //todo - use
    {
        /// <summary>
        /// Normal (aka a colored bar at the top)
        /// </summary>
        [JsonPropertyName("normal")]
        Normal,
        /// <summary>
        /// Full (aka entire card is filled with the color)
        /// </summary>
        [JsonPropertyName("full")]
        Full
    }
}