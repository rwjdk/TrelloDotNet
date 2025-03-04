﻿using System.Text.Json.Serialization;

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
        [JsonPropertyName("gray")]
        Gray,

        /// <summary>
        /// Green
        /// </summary>
        [JsonPropertyName("green")]
        Green,

        /// <summary>
        /// Yellow
        /// </summary>
        [JsonPropertyName("yellow")]
        Yellow,

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
        /// Blue
        /// </summary>
        [JsonPropertyName("blue")]
        Blue,

        /// <summary>
        /// Teal
        /// </summary>
        [JsonPropertyName("teal")]
        Teal,

        /// <summary>
        /// Lime
        /// </summary>
        [JsonPropertyName("lime")]
        Lime,

        /// <summary>
        /// Magenta
        /// </summary>
        [JsonPropertyName("magenta")]
        Magenta,
    }
}