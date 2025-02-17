using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a color of a custom field Option
    /// </summary>
    public enum CustomFieldOptionColor
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// none
        /// </summary>
        [JsonPropertyName("none")]
        None,

        /// <summary>
        /// red
        /// </summary>
        [JsonPropertyName("red")]
        Red,

        /// <summary>
        /// orange
        /// </summary>
        [JsonPropertyName("orange")]
        Orange,

        /// <summary>
        /// yellow
        /// </summary>
        [JsonPropertyName("yellow")]
        Yellow,

        /// <summary>
        /// sky
        /// </summary>
        [JsonPropertyName("sky")]
        Sky,

        /// <summary>
        /// blue
        /// </summary>
        [JsonPropertyName("blue")]
        Blue,

        /// <summary>
        /// pink
        /// </summary>
        [JsonPropertyName("pink")]
        Pink,

        /// <summary>
        /// purple
        /// </summary>
        [JsonPropertyName("purple")]
        Purple,

        /// <summary>
        /// green
        /// </summary>
        [JsonPropertyName("green")]
        Green,

        /// <summary>
        /// black
        /// </summary>
        [JsonPropertyName("black")]
        Black,

        /// <summary>
        /// lime
        /// </summary>
        [JsonPropertyName("lime")]
        Lime,
    }
}