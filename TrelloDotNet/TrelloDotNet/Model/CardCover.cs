using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Cover of the Card
    /// </summary>
    public class CardCover
    {
        /// <summary>
        /// The Size of the Cover (Normal or full)
        /// </summary>
        [JsonPropertyName("size")]
        public string Size { get; set; } //todo - enum?

        /// <summary>
        /// Color of the Cover (null if Background is used instead) - Options (pink, yellow, lime, blue, black, orange, red, purple, sky, green)
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; } //todo - enum

        /// <summary>
        /// Background ImageId of the Cover
        /// </summary>
        [JsonPropertyName("idUploadedBackground")]
        public string BackgroundImageId { get; set; } //todo - is setting supported by the API (have not seen it)
    }
}