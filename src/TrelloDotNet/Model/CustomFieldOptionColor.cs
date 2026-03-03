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
        [JsonPropertyName(Constants.TrelloIds.CustomFieldFields.None)]
        None,

        /// <summary>
        /// red
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Red)]
        Red,

        /// <summary>
        /// orange
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Orange)]
        Orange,

        /// <summary>
        /// yellow
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Yellow)]
        Yellow,

        /// <summary>
        /// sky
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Sky)]
        Sky,

        /// <summary>
        /// blue
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Blue)]
        Blue,

        /// <summary>
        /// pink
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Pink)]
        Pink,

        /// <summary>
        /// purple
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Purple)]
        Purple,

        /// <summary>
        /// green
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Green)]
        Green,

        /// <summary>
        /// black
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Black)]
        Black,

        /// <summary>
        /// lime
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Lime)]
        Lime,
    }
}






