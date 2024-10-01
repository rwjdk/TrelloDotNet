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
        [JsonPropertyName("due")]
        [JsonInclude]
        public DateTimeOffset? Due { get; private set; }

        /// <summary>
        /// dueComplete
        /// </summary>
        [JsonPropertyName("dueComplete")]
        [JsonInclude]
        public bool? DueComplete { get; private set; }

        /// <summary>
        /// start
        /// </summary>
        [JsonPropertyName("start")]
        [JsonInclude]
        public DateTimeOffset? Start { get; private set; }

        /// <summary>
        /// idLabels
        /// </summary>
        [JsonPropertyName("idLabels")]
        [JsonInclude]
        public List<string> Labels { get; private set; }

        /// <summary>
        /// locationName
        /// </summary>
        [JsonPropertyName("locationName")]
        [JsonInclude]
        public string LocationName { get; private set; }

        /// <summary>
        /// address
        /// </summary>
        [JsonPropertyName("address")]
        [JsonInclude]
        public string Address { get; private set; }

        /// <summary>
        /// name
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// desc
        /// </summary>
        [JsonPropertyName("desc")]
        [JsonInclude]
        public string Description { get; private set; }

        /// <summary>
        /// idList
        /// </summary>
        [JsonPropertyName("idList")]
        [JsonInclude]
        public string ListId { get; private set; }

        /// <summary>
        /// pos
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal? Position { get; private set; }

        /// <summary>
        /// dueReminder
        /// </summary>
        [JsonPropertyName("dueReminder")]
        [JsonInclude]
        public int? DueReminder { get; private set; }

        /// <summary>
        /// coordinates
        /// </summary>
        [JsonPropertyName("coordinates")]
        [JsonInclude]
        public Coordinates Coordinates { get; private set; }

        /// <summary>
        /// cover
        /// </summary>
        [JsonPropertyName("cover")]
        [JsonInclude]
        public CardCover Cover { get; private set; }

        /// <summary>
        /// Id of the image attachment of this card to use as its cover
        /// </summary>
        [JsonPropertyName("idAttachmentCover")]
        [JsonInclude]
        public string AttachmentCover { get; private set; }

        /// <summary>
        /// Display Name of the Organization
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonInclude]
        public string DisplayName { get; private set; }

        /// <summary>
        /// Website of the Organization
        /// </summary>
        [JsonPropertyName("website")]
        [JsonInclude]
        public string Website { get; private set; }

        /// <summary>
        /// Text of Comment
        /// </summary>
        [JsonPropertyName("text")]
        [JsonInclude]
        public string Text { get; private set; }
    }
}