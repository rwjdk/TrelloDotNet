using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Cover of the Card
    /// </summary>
    [DebuggerDisplay("Color: {Color} - Image: {BackgroundImageId}")]
    public class CardCover
    {
        /// <summary>
        /// The Size of the Cover (Normal or full)
        /// </summary>
        [JsonPropertyName("size")]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CardCoverSize>))]
        public CardCoverSize? Size { get; set; }

        /// <summary>
        /// Color of the Cover (null if Background is used instead) - Options (pink, yellow, lime, blue, black, orange, red, purple, sky, green)
        /// </summary>
        [JsonPropertyName("color")]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CardCoverColor>))]
        public CardCoverColor? Color { get; set; }

        /// <summary>
        /// Brightness of the Cover - Options (None, Dark, Light) [This option is only used when BackgroundImageId is used]
        /// </summary>
        [JsonPropertyName("brightness")]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<CardCoverBrightness>))]
        public CardCoverBrightness? Brightness { get; set; }

        /// <summary>
        /// Background ImageId of the Cover
        /// </summary>
        /// <remarks>
        /// Remember to set Color to None or Null, else that will override and color is used instead
        /// </remarks>
        [JsonPropertyName("idUploadedBackground")]
        public string BackgroundImageId { get; set; }

        /// <summary>
        /// Constructor (Color Cover)
        /// </summary>
        /// <param name="color">Color of the Cover</param>
        /// <param name="size">Size of the Cover</param>
        public CardCover(CardCoverColor color, CardCoverSize size)
        {
            Size = size;
            Color = color;
        }

        /// <summary>
        /// Constructor (Image based Cover)
        /// </summary>
        /// <param name="backgroundImageId">Id of Background Image</param>
        /// <param name="brightness">Brightness of the Cover</param>
        public CardCover(string backgroundImageId, CardCoverBrightness? brightness)
        {
            Brightness = brightness;
            BackgroundImageId = backgroundImageId;
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public CardCover()
        {
            //Serialization
        }

        internal void PrepareForAddUpdate()
        {
            if (Color == CardCoverColor.None)
            {
                Color = null; //Ensure property is not included
            }

            if (Size == CardCoverSize.None)
            {
                Size = null; //Ensure property is not included
            }

            if (Brightness == CardCoverBrightness.None)
            {
                Brightness = null; //Ensure property is not included
            }
        }
    }
}