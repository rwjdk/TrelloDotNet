﻿using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// State of the Checklist-item (complete or incomplete)
    /// </summary>
    public enum ChecklistItemState
    {
        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("null")]
        None = 0,
        /// <summary>
        /// incomplete
        /// </summary>
        [JsonPropertyName("incomplete")]
        Incomplete,
        /// <summary>
        /// complete
        /// </summary>
        [JsonPropertyName("complete")]
        Complete
    }
}