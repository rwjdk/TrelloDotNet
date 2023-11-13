using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent a sticker-field
    /// </summary>
    public enum StickerFieldsType
    {
        /// <summary>
        /// Top position of the sticker on the card
        /// </summary>
        [JsonPropertyName("top")]
        Top,
        /// <summary>
        /// Left position of the sticker on the card
        /// </summary>
        [JsonPropertyName("left")]
        Left,
        /// <summary>
        /// z-index of the sticker on the card
        /// </summary>
        [JsonPropertyName("zIndex")]
        ZIndex,
        /// <summary>
        /// Rotations of the sticker on the card
        /// </summary>
        [JsonPropertyName("rotate")]
        Rotate,
        /// <summary>
        /// Id of the image used for the sticker
        /// </summary>
        [JsonPropertyName("image")]
        ImageId,
        /// <summary>
        /// URL of the image used for the sticker
        /// </summary>
        [JsonPropertyName("imageUrl")]
        ImageUrl
    }
}