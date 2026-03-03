using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The color of the Cover
    /// </summary>
    public enum CardCoverColor
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.NullValue)]
        None = 0,

        /// <summary>
        /// Pink
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Pink)]
        Pink,

        /// <summary>
        /// Yellow
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Yellow)]
        Yellow,

        /// <summary>
        /// Lime
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Lime)]
        Lime,

        /// <summary>
        /// Blue
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Blue)]
        Blue,

        /// <summary>
        /// Black
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Black)]
        Black,

        /// <summary>
        /// Orange
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Orange)]
        Orange,

        /// <summary>
        /// Red
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Red)]
        Red,

        /// <summary>
        /// Purple
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Purple)]
        Purple,

        /// <summary>
        /// Sky
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Sky)]
        Sky,

        /// <summary>
        /// Green
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Green)]
        Green
    }
}






