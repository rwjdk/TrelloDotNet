﻿using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Type of Custom Field
    /// </summary>
    public enum CustomFieldType
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("checkbox")]
        Checkbox,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("date")]
        Date,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("list")]
        List,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("number")]
        Number,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("text")]
        Text
    }
}