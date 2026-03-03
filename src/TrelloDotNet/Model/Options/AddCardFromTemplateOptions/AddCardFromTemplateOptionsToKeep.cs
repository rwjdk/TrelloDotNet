using System;
using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [JsonPropertyName(Constants.TrelloIds.CardFields.All)]
        All = -1,

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Attachments)]
        Attachments = 1,

        /// <summary>
        /// Checklists
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.Checklists)]
        Checklists = 2,

        /// <summary>
        /// Comments
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Comments)]
        Comments = 4,

        /// <summary>
        /// Custom Fields
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CustomFieldFields.CustomFields)]
        CustomFields = 8,

        /// <summary>
        /// Due date
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Due)]
        Due = 16,

        /// <summary>
        /// Labels
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.LabelFields.Labels)]
        Labels = 32,

        /// <summary>
        /// Members
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Members)]
        Members = 64,

        /// <summary>
        /// Start Date
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Start)]
        Start = 128,

        /// <summary>
        /// Stickers
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Stickers)]
        Stickers = 254
    }
}





