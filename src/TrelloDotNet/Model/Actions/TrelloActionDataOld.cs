using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Collection of Old values of the Action Data
    /// </summary>
    public class TrelloActionDataOld
    {
        /// <summary>
        /// due
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Due)]
        [JsonInclude]
        public DateTimeOffset? Due { get; private set; }

        /// <summary>
        /// dueComplete
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.DueComplete)]
        [JsonInclude]
        public bool? DueComplete { get; private set; }

        /// <summary>
        /// start
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Start)]
        [JsonInclude]
        public DateTimeOffset? Start { get; private set; }

        /// <summary>
        /// idLabels
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdLabels)]
        [JsonInclude]
        public List<string> Labels { get; private set; }

        /// <summary>
        /// locationName
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.LocationName)]
        [JsonInclude]
        public string LocationName { get; private set; }

        /// <summary>
        /// address
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Address)]
        [JsonInclude]
        public string Address { get; private set; }

        /// <summary>
        /// name
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// desc
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Desc)]
        [JsonInclude]
        public string Description { get; private set; }

        /// <summary>
        /// idList
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.IdList)]
        [JsonInclude]
        public string ListId { get; private set; }

        /// <summary>
        /// pos
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        [JsonInclude]
        public decimal? Position { get; private set; }

        /// <summary>
        /// dueReminder
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.DueReminder)]
        [JsonInclude]
        public int? DueReminder { get; private set; }

        /// <summary>
        /// coordinates
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Coordinates)]
        [JsonInclude]
        public Coordinates Coordinates { get; private set; }

        /// <summary>
        /// cover
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Cover)]
        [JsonInclude]
        public CardCover Cover { get; private set; }

        /// <summary>
        /// Id of the image attachment of this card to use as its cover
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdAttachmentCover)]
        [JsonInclude]
        public string AttachmentCover { get; private set; }

        /// <summary>
        /// Display Name of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.DisplayName)]
        [JsonInclude]
        public string DisplayName { get; private set; }

        /// <summary>
        /// Website of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Website)]
        [JsonInclude]
        public string Website { get; private set; }

        /// <summary>
        /// Text of Comment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Text)]
        [JsonInclude]
        public string Text { get; private set; }
    }
}




