using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;
using TrelloDotNet.Model.Actions;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Trello Card
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
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
        /// Id of the board the card is on
        /// </summary>
        /// <remarks>
        /// If you move board make sure ListId, MemberIds and LabelsId are valid for the new board
        /// </remarks>
        [JsonPropertyName("idBoard")]
        [QueryParameter]
        public string BoardId { get; set; }

        /// <summary>
        /// Name of the card
        /// </summary>
        [JsonPropertyName("name")]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// Description of the card
        /// </summary>
        [JsonPropertyName("desc")]
        [QueryParameter]
        public string Description { get; set; }

        /// <summary>
        /// If the card is Archived (closed)
        /// </summary>
        [JsonPropertyName("closed")]
        [QueryParameter]
        public bool Closed { get; set; }

        /// <summary>
        /// The position of the card in the current list
        /// </summary>
        [JsonPropertyName("pos")]
        [QueryParameter]
        public decimal Position { get; set; }

        /// <summary>
        /// If the card is Watched (subscribed) by the owner of the Token used against the API
        /// </summary>
        [JsonPropertyName("subscribed")]
        [QueryParameter]
        public bool Subscribed { get; set; }

        /// <summary>
        /// Id of the List the Card belong to
        /// </summary>
        /// <remarks>
        /// NB: If you move the card to another board set this to null (aka first column of new board) or a valid listId on the new board
        /// </remarks>
        [JsonPropertyName("idList")]
        [QueryParameter(false)]
        public string ListId { get; set; }

        /// <summary>
        /// When the Card was created [stored in UTC]
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <summary>
        /// When there was last activity on the card (aka update date) [stored UTC]
        /// </summary>
        [JsonPropertyName("dateLastActivity")]
        [JsonInclude]
        public DateTimeOffset LastActivity { get; private set; }

        /// <summary>
        /// The Start-date of the work on the card (not to be confused with Created property as this can be null) [stored in UTC]
        /// </summary>
        [JsonPropertyName("start")]
        [QueryParameter]
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// The Due-date of the work on the card should be finished [stored in UTC]
        /// </summary>
        [JsonPropertyName("due")]
        [QueryParameter]
        public DateTimeOffset? Due { get; set; }

        /// <summary>
        /// If the due work is completed
        /// </summary>
        [JsonPropertyName("dueComplete")]
        [QueryParameter]
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
        /// Ids of the Labels that are on the Card
        /// </summary>
        [JsonPropertyName("idLabels")]
        [QueryParameter]
        public List<string> LabelIds { get; set; }

        /// <summary>
        /// Ids of the Checklists on the card
        /// </summary>
        /// <remarks>
        /// NB: This is not Updateable here. Instead use the dedicated methods for the action
        /// </remarks>
        [JsonPropertyName("idChecklists")]
        [JsonInclude]
        public List<string> ChecklistIds { get; private set; }

        /// <summary>
        /// Ids of members that should be assigned to the card
        /// </summary>
        [JsonPropertyName("idMembers")]
        [QueryParameter]
        public List<string> MemberIds { get; set; }

        /// <summary>
        /// Id of the image attachment of this card to use as its cover
        /// </summary>
        [JsonPropertyName("idAttachmentCover")]
        [QueryParameter]
        [JsonInclude]
        public string AttachmentCover { get; set; }

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
        /// Cover of the Card (set to null in update scenario if you wish to remove it)
        /// </summary>
        [JsonPropertyName("cover")]
        public CardCover Cover { get; set; }

        /// <summary>
        /// Custom Fields of the Card (Only populated in Get Methods and if enabled in TrelloClientOptions)
        /// </summary>
        /// <remarks>Tip: Use Extension methods GetCustomFieldValueAsXYZ on the list for a handy way to get values</remarks>
        [JsonPropertyName("customFieldItems")]
        [JsonInclude]
        public List<CustomFieldItem> CustomFieldItems { get; private set; }

        /// <summary>
        /// Attachments of the Card (Only populated in Get Methods and if enabled in TrelloClientOptions or if GetCardOptions.IncludeAttachments is used)
        /// </summary>
        [JsonPropertyName("attachments")]
        [JsonInclude]
        public List<Attachment> Attachments { get; private set; }
        
        /// <summary>
        /// Members of the Card (Only populated if GetCardOptions.IncludeMembers is used)
        /// </summary>
        [JsonPropertyName("members")]
        [JsonInclude]
        public List<Member> Members { get; private set; }
        
        /// <summary>
        /// Board the Card is on (Only populated if GetCardOptions.IncludeBoard is used)
        /// </summary>
        [JsonPropertyName("board")]
        [JsonInclude]
        public Board Board { get; private set; }
        
        /// <summary>
        /// List the Card is is (Only populated if GetCardOptions.IncludeList is used)
        /// </summary>
        [JsonPropertyName("list")]
        [JsonInclude]
        public List List { get; private set; }

        /// <summary>
        /// Actions of the Card (Only populated if 'ActionTypes' in GetCardOptions is included)
        /// </summary>
        [JsonPropertyName("actions")]
        [JsonInclude]
        public List<TrelloAction> Actions { get; private set; }
        
        /// <summary>
        /// Checklists of the Card (Only populated if GetCardOptions.IncludeChecklist is used)
        /// </summary>
        [JsonPropertyName("checklists")]
        [JsonInclude]
        public List<Checklist> Checklists { get; private set; }

        /// <summary>
        /// Plugin data of the Card (Only populated if GetCardOptions.IncludePluginData is used)
        /// </summary>
        [JsonPropertyName("pluginData")]
        [JsonInclude]
        public List<PluginData> PluginData { get; private set; }

        /// <summary>
        /// The named position of the Card in the list: Top or Bottom
        /// </summary>
        [JsonIgnore]
        public NamedPosition? NamedPosition { internal get; set; }

        /// <summary>
        /// Constructor (Common Card fields)
        /// </summary>
        /// <param name="listId">Id of list to add the card to</param>
        /// <param name="name">Name/Title of the card</param>
        /// <param name="description">Description on the card</param>
        public Card(string listId, string name, string description = null)
        {
            Name = name;
            Description = description;
            ListId = listId;
            InitializeLists();
        }

        /// <summary>
        /// Constructor (All supported fields for add/update)
        /// </summary>
        /// <param name="listId">Id of list to add the card to</param>
        /// <param name="name">Name/Title of the card</param>
        /// <param name="description">Description on the card</param>
        /// <param name="start">Start-date (should be given in UTC)</param>
        /// <param name="due">Due-date (should be given in UTC)</param>
        /// <param name="dueComplete">If due-date is complete (normally false when you create a card)</param>
        /// <param name="labelIds">Labels set on the card</param>
        /// <param name="memberIds">Members (user) assigned to the card</param>
        public Card(string listId, string name, string description, DateTimeOffset? start, DateTimeOffset? due, bool dueComplete = false, List<string> labelIds = null, List<string> memberIds = null)
        {
            Name = name;
            Description = description;
            ListId = listId;
            Start = start;
            Due = due;
            DueComplete = dueComplete;
            LabelIds = labelIds;
            MemberIds = memberIds;
            InitializeLists();
            if (memberIds != null)
            {
                MemberIds = memberIds;
            }

            if (labelIds != null)
            {
                LabelIds = labelIds;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Card()
        {
            //Serialization
        }

        private void InitializeLists()
        {
            LabelIds = new List<string>();
            MemberIds = new List<string>();
        }
    }
}