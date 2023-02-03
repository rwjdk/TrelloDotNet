using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Trello Card
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Id of the card (Long unique variant)
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Id of the card in short form (only unique to the specific board)
        /// </summary>
        [JsonPropertyName("idShort")]
        [JsonInclude]
        public int IdShort { get; private set; }

        /// <summary>
        /// Id of the board the card is on [UPDATEABLE]
        /// </summary>
        /// <remarks>
        /// If you move board make sure ListId, MemberIds and LabelsId are valid for the new board
        /// </remarks>
        [JsonPropertyName("idBoard")]
        [UriParameter]
        public string BoardId { get; set; }

        /// <summary>
        /// Name of the card [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("name")]
        [UriParameter]
        public string Name { get; set; }

        /// <summary>
        /// Description of the card [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("desc")]
        [UriParameter]
        public string Description { get; set; }

        /// <summary>
        /// If the card is Archived (closed) [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("closed")]
        [UriParameter]
        public bool Closed { get; set; }

        //todo - Add Support for position (research in order to understand)
        /*
        /// <summary>
        /// The position of the card in the current list [UPDATEABLE] 
        /// </summary>
        [JsonPropertyName("pos")]
        [UriParameter]
        public int Position { get; set; }
        */

        /// <summary>
        /// If the card is Watched (subscribed) by the owner of the Token used against the API [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("subscribed")]
        [UriParameter]
        public bool Subscribed { get; set; }

        /// <summary>
        /// Id of the List the Card belong to [UPDATEABLE]
        /// </summary>
        /// <remarks>
        /// NB: If you move the card to another board set this to null (aka first column of new board) or a valid listId on the new board
        /// </remarks>
        [JsonPropertyName("idList")]
        [UriParameter(false)]
        public string ListId { get; set; }

        /// <summary>
        /// When the Card was created [stored in UTC]
        /// </summary>
        [JsonIgnore] public DateTimeOffset Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <summary>
        /// When there was last activity on the card (aka update date) [stored UTC]
        /// </summary>
        [JsonPropertyName("dateLastActivity")]
        [JsonInclude]
        public DateTimeOffset LastActivity { get; private set; }

        /// <summary>
        /// The Start-date of the work on the card (not to be confused with Created property as this can be null) [stored in UTC] [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("start")]
        [UriParameter]
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// The Due-date of the work on the card should be finished [stored in UTC] [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("due")]
        [UriParameter]
        public DateTimeOffset? Due { get; set; }

        /// <summary>
        /// If the due work is completed [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("dueComplete")]
        [UriParameter]
        public bool DueComplete { get; set; }

        /// <summary>
        /// The labels (in details) that are on the card
        /// </summary>
        /// <remarks>
        /// NB: This is not updateable. Instead update what labels should be included via the 'LabelIds' property in update scenarios
        /// </remarks>
        [JsonPropertyName("labels")]
        [JsonInclude]
        public List<Label> Labels { get; private set; }

        /// <summary>
        /// Ids of the Labels that are on the Card [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("idLabels")]
        [UriParameter]
        public List<string> LabelIds { get; set; }

        /// <summary>
        /// Ids of the Checklists on the card
        /// </summary>
        /// <remarks>
        /// NB: This is not Updateable here. Instead use TODO: Mention method to do so
        /// </remarks>
        [JsonPropertyName("idChecklists")]
        [JsonInclude]
        public List<string> ChecklistIds { get; private set; }

        /// <summary>
        /// Ids of members that should be assigned to the card [UPDATEABLE]
        /// </summary>
        [JsonPropertyName("idMembers")]
        [UriParameter]
        public List<string> MemberIds { get; set; }

        //todo - find out what this is???
        /*
        [JsonPropertyName("idAttachmentCover")]
        [UriParameter]
        public string AttachmentCover { get; internal set; } 
        */

        /// <summary>
        /// Url you can use to get to the card
        /// </summary>
        [JsonPropertyName("url")]
        [JsonInclude]
        public string Url { get; private set; }

        /// <summary>
        /// Short Url you can use to get to the card
        /// </summary>
        [JsonPropertyName("shortUrl")]
        [JsonInclude]
        public string ShortUrl { get; private set; }

        /// <summary>
        /// Cover of the Card
        /// </summary>
        [JsonPropertyName("cover")]
        public CardCover Cover { get; set; } //todo - Add support for add/Update
        
        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}