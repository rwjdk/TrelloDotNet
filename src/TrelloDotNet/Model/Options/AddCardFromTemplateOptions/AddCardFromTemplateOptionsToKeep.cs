using System;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Options.AddCardFromTemplateOptions
{
    /// <summary>
    /// What source types to keep
    /// </summary>
    [Flags]
    public enum AddCardFromTemplateOptionsToKeep
    {
        /// <summary>
        /// Copy Everything
        /// </summary>
        [JsonPropertyName("all")]
        All = -1,

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonPropertyName("attachments")]
        Attachments = 1,

        /// <summary>
        /// Checklists
        /// </summary>
        [JsonPropertyName("checklists")]
        Checklists = 2,

        /// <summary>
        /// Comments
        /// </summary>
        [JsonPropertyName("comments")]
        Comments = 4,

        /// <summary>
        /// Custom Fields
        /// </summary>
        [JsonPropertyName("customFields")]
        CustomFields = 8,

        /// <summary>
        /// Due date
        /// </summary>
        [JsonPropertyName("due")]
        Due = 16,

        /// <summary>
        /// Labels
        /// </summary>
        [JsonPropertyName("labels")]
        Labels = 32,

        /// <summary>
        /// Members
        /// </summary>
        [JsonPropertyName("members")]
        Members = 64,

        /// <summary>
        /// Start Date
        /// </summary>
        [JsonPropertyName("start")]
        Start = 128,

        /// <summary>
        /// Stickers
        /// </summary>
        [JsonPropertyName("stickers")]
        Stickers = 254
    }
}