using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a sticker on a Card
    /// </summary>
    [DebuggerDisplay("Sticker: {ImageId} (Id: {Id})")]
    public class Sticker
    {
        /// <summary>
        /// Id of the Sticker
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The top position of the sticker, from -60 to 100
        /// </summary>
        [JsonPropertyName("top")]
        [QueryParameter]
        public decimal Top { get; set; }

        /// <summary>
        /// Id of the Sticker Image (real Id for custom stickers and string-identifier for default stickers)
        /// </summary>
        [JsonPropertyName("image")]
        [QueryParameter]
        public string ImageId { get; set; }

        /// <summary>
        /// Id of the Sticker Image as Enum
        /// </summary>
        [JsonIgnore]
        public StickerDefaultImageId ImageIdAsDefaultEnum => StringToDefaultImageId(ImageId);


        /// <summary>
        /// URL of the Sticker Image
        /// </summary>
        [JsonPropertyName("imageUrl")]
        [JsonInclude]
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The left position of the sticker, from -60 to 100
        /// </summary>
        [JsonPropertyName("left")]
        [QueryParameter]
        public decimal Left { get; set; }

        /// <summary>
        /// The z-index of the sticker
        /// </summary>
        [JsonPropertyName("zIndex")]
        [QueryParameter]
        public decimal ZIndex { get; set; }

        /// <summary>
        /// The Rotation of the sticker, from -60 to 100
        /// </summary>
        [JsonPropertyName("rotate")]
        [QueryParameter]
        public decimal Rotation { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Sticker()
        {
            //Serialization
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="imageId">The Default Sticker Type</param>
        /// <param name="top">The top position of the sticker, from -60 to 100</param>
        /// <param name="left">The left position of the sticker, from -60 to 100</param>
        /// <param name="zIndex">The z-index of the sticker</param>
        /// <param name="rotation">The Rotation of the sticker, from -60 to 100</param>
        public Sticker(StickerDefaultImageId imageId, decimal top = 0, decimal left = 0, decimal zIndex = 0, decimal rotation = 0) : this(null, top, left, zIndex, rotation)
        {
            ImageId = DefaultImageToString(imageId);
        }

        /// <summary>
        /// Convert a StickerDefaultImageId enum to its matching ImageId String
        /// </summary>
        /// <param name="imageId">The ImageId Enum value</param>
        /// <returns>The String</returns>
        /// <exception cref="ArgumentOutOfRangeException">If image Id is not a known value</exception>
        public static string DefaultImageToString(StickerDefaultImageId imageId)
        {
            switch (imageId)
            {
                case StickerDefaultImageId.Check:
                    return "check";
                case StickerDefaultImageId.Heart:
                    return "heart";
                case StickerDefaultImageId.Warning:
                    return "warning";
                case StickerDefaultImageId.Clock:
                    return "clock";
                case StickerDefaultImageId.Smile:
                    return "smile";
                case StickerDefaultImageId.Laugh:
                    return "laugh";
                case StickerDefaultImageId.Huh:
                    return "huh";
                case StickerDefaultImageId.Frown:
                    return "frown";
                case StickerDefaultImageId.ThumbsUp:
                    // ReSharper disable once StringLiteralTypo
                    return "thumbsup";
                case StickerDefaultImageId.ThumbsDown:
                    // ReSharper disable once StringLiteralTypo
                    return "thumbsdown";
                case StickerDefaultImageId.Star:
                    return "star";
                case StickerDefaultImageId.RocketShip:
                    // ReSharper disable once StringLiteralTypo
                    return "rocketship";
                default:
                    throw new ArgumentOutOfRangeException(nameof(imageId), imageId, null);
            }
        }

        /// <summary>
        /// Turn an ImageId into its 'StickerDefaultImageId' value (or value NotADefault if it is not a known value )
        /// </summary>
        /// <param name="imageId">ImageId string to convert</param>
        /// <returns>The Enum Value</returns>
        public static StickerDefaultImageId StringToDefaultImageId(string imageId)
        {
            switch (imageId)
            {
                case "check":
                    return StickerDefaultImageId.Check;
                case "heart":
                    return StickerDefaultImageId.Heart;
                case "warning":
                    return StickerDefaultImageId.Warning;
                case "clock":
                    return StickerDefaultImageId.Clock;
                case "smile":
                    return StickerDefaultImageId.Smile;
                case "laugh":
                    return StickerDefaultImageId.Laugh;
                case "huh":
                    return StickerDefaultImageId.Huh;
                case "frown":
                    return StickerDefaultImageId.Frown;
                // ReSharper disable once StringLiteralTypo
                case "thumbsup":
                    return StickerDefaultImageId.ThumbsUp;
                // ReSharper disable once StringLiteralTypo
                case "thumbsdown":
                    return StickerDefaultImageId.ThumbsDown;
                case "star":
                    return StickerDefaultImageId.Star;
                // ReSharper disable once StringLiteralTypo
                case "rocketship":
                    return StickerDefaultImageId.RocketShip;
                default:
                    return StickerDefaultImageId.NotADefault;
            }
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="imageId">Id of the Sticker Image (real Id for custom stickers and string-identifier for default stickers)</param>
        /// <param name="top">The top position of the sticker, from -60 to 100</param>
        /// <param name="left">The left position of the sticker, from -60 to 100</param>
        /// <param name="zIndex">The z-index of the sticker</param>
        /// <param name="rotation">The Rotation of the sticker, from -60 to 100</param>
        public Sticker(string imageId, decimal top = 0, decimal left = 0, decimal zIndex = 0, decimal rotation = 0)
        {
            Top = top;
            ImageId = imageId;
            Left = left;
            ZIndex = zIndex;
            Rotation = rotation;
        }
    }
}