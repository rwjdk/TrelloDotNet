using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent a Card on an Action
    /// </summary>
    public class TrelloActionDataCard
    {
        /// <summary>
        /// Id of the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Id of the card in short form (only unique to the specific board)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdShort)]
        [JsonInclude]
        public int IdShort { get; private set; }

        /// <summary>
        /// The short-link of the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.ShortLink)]
        [JsonInclude]
        public string ShortLink { get; private set; }

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
    }
}




