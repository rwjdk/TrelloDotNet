using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The color of the Cover
    /// </summary>
    public enum CardCoverColor
    {
        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("null")] 
        None = 0,
        /// <summary>
        /// Pink
        /// </summary>
        [JsonPropertyName("pink")]
        Pink,
        /// <summary>
        /// Yellow
        /// </summary>
        [JsonPropertyName("yellow")]
        Yellow,
        /// <summary>
        /// Lime
        /// </summary>
        [JsonPropertyName("lime")]
        Lime,
        /// <summary>
        /// Blue
        /// </summary>
        [JsonPropertyName("blue")]
        Blue,
        /// <summary>
        /// Black
        /// </summary>
        [JsonPropertyName("black")]
        Black,
        /// <summary>
        /// Orange
        /// </summary>
        [JsonPropertyName("orange")]
        Orange,
        /// <summary>
        /// Red
        /// </summary>
        [JsonPropertyName("red")]
        Red,
        /// <summary>
        /// Purple
        /// </summary>
        [JsonPropertyName("purple")]
        Purple,
        /// <summary>
        /// Sky
        /// </summary>
        [JsonPropertyName("sky")]
        Sky,
        /// <summary>
        /// Green
        /// </summary>
        [JsonPropertyName("green")]
        Green
    }
    
}