using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [JsonPropertyName(Constants.TrelloIds.ListFields.Top)]
        Top,

        /// <summary>
        /// Left position of the sticker on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Left)]
        Left,

        /// <summary>
        /// z-index of the sticker on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.ZIndex)]
        ZIndex,

        /// <summary>
        /// Rotations of the sticker on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Rotate)]
        Rotate,

        /// <summary>
        /// Id of the image used for the sticker
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Image)]
        ImageId,

        /// <summary>
        /// URL of the image used for the sticker
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.ImageUrl)]
        ImageUrl
    }
}





