using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

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
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CardCoverSize>))]
        public CardCoverSize Size { get; private set; }

        /// <summary>
        /// Color of the Cover (null if Background is used instead) - Options (pink, yellow, lime, blue, black, orange, red, purple, sky, green)
        /// </summary>
        [JsonPropertyName("color")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CardCoverColor>))]
        public CardCoverColor Color { get; private set; }

        /// <summary>
        /// Color of the Cover (null if Background is used instead) - Options (pink, yellow, lime, blue, black, orange, red, purple, sky, green)
        /// </summary>
        [JsonPropertyName("brightness")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CardCoverBrightness>))]
        public CardCoverBrightness Brightness { get; private set; }

        /// <summary>
        /// Background ImageId of the Cover
        /// </summary>
        [JsonPropertyName("idUploadedBackground")]
        public string BackgroundImageId { get; set; }
    }
}