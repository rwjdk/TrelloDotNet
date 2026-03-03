using System.Text.Json.Serialization;

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
        [JsonPropertyName(Constants.TrelloIds.ChecklistFields.Checkbox)]
        Checkbox,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Date)]
        Date,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.List)]
        List,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CustomFieldFields.Number)]
        Number,

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Text)]
        Text
    }
}





