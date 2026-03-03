using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Color of the List (Standard and Higher Subscriptions only Feature)
    /// </summary>
    public enum ListColor
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Gray (default)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Gray)]
        Gray,

        /// <summary>
        /// Green
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Green)]
        Green,

        /// <summary>
        /// Yellow
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Yellow)]
        Yellow,

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
        /// Blue
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Blue)]
        Blue,

        /// <summary>
        /// Teal
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Teal)]
        Teal,

        /// <summary>
        /// Lime
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Lime)]
        Lime,

        /// <summary>
        /// Magenta
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Magenta)]
        Magenta,
    }
}






