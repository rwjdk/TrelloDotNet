using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The name of a position instead of a numeric value
    /// </summary>
    public enum NamedPosition
    {
        /// <summary>
        /// Top
        /// </summary>
        [JsonPropertyName("top")]
        Top,

        /// <summary>
        /// Bottom
        /// </summary>
        [JsonPropertyName("bottom")]
        Bottom,
    }
}