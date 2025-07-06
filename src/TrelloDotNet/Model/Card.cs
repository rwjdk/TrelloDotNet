using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;
using TrelloDotNet.Model.Actions;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represents a Card.
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class Card
    {
        /// <summary>
        /// The ID of the card (globally unique across all boards).
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The short ID of the card (unique within its board).
        /// </summary>
        [JsonPropertyName("idShort")]
        [JsonInclude]
        public int IdShort { get; private set; }

        /// <summary>
        /// The ID of the board this card belongs to.
        /// </summary>
        /// <remarks>
        /// If you move the card to another board, ensure ListId, MemberIds, and LabelIds are valid for the new board.
        /// </remarks>
        [JsonPropertyName("idBoard")]
        [QueryParameter]
        public string BoardId { get; set; }

        /// <summary>
        /// The name (title) of the card.
        /// </summary>
        [JsonPropertyName("name")]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// The description of the card.
        /// </summary>
        [JsonPropertyName("desc")]
        [QueryParameter]
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether the card is archived (closed).
        /// </summary>
        [JsonPropertyName("closed")]
        [QueryParameter]
        public bool Closed { get; set; }

        /// <summary>
        /// The position of the card within its current list. Lower numbers are higher in the list.
        /// </summary>
        [JsonPropertyName("pos")]
        [QueryParameter]
        public decimal Position { get; set; }

        /// <summary>
        /// Indicates if the card is watched (subscribed) by the owner of the API token.
        /// </summary>
        [JsonPropertyName("subscribed")]
        [QueryParameter]
        public bool Subscribed { get; set; }

        /// <summary>
        /// The ID of the list this card currently belongs to.
        /// </summary>
        /// <remarks>
        /// If you move the card to another board, set this to null (to place in the first column of the new board) or to a valid list ID on the new board.
        /// </remarks>
        [JsonPropertyName("idList")]
        [QueryParameter(false)]
        public string ListId { get; set; }

        /// <summary>
        /// The date and time when the card was created, derived from the card's ID. Stored in UTC.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <summary>
        /// The date and time of the last activity on the card (such as an update or comment). Stored in UTC.
        /// <remarks>
        /// This property is also affected by the Position property, so moving another card to the same list can affect this timestamp.
        /// </remarks>
        /// </summary>
        [JsonPropertyName("dateLastActivity")]
        [JsonInclude]
        public DateTimeOffset LastActivity { get; private set; }

        /// <summary>
        /// The start date for work on the card. Not the same as Created. Stored in UTC.
        /// </summary>
        [JsonPropertyName("start")]
        [QueryParameter]
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// The due date of the card. Stored in UTC.
        /// </summary>
        [JsonPropertyName("due")]
        [QueryParameter]
        public DateTimeOffset? Due { get; set; }

        /// <summary>
        /// Indicates whether the card is marked as complete.
        /// </summary>
        [JsonPropertyName("dueComplete")]
        [QueryParameter]
        public bool DueComplete { get; set; }

        /// <summary>
        /// The detailed label objects currently assigned to the card. Read-only; to update labels, use the LabelIds property.
        /// </summary>
        /// <remarks>
        /// This property is not updateable. To change labels, update the LabelIds property instead.
        /// </remarks>
        [JsonPropertyName("labels")]
        [JsonInclude]
        public List<Label> Labels { get; private set; }

        /// <summary>
        /// The list of label IDs currently assigned to the card.
        /// </summary>
        [JsonPropertyName("idLabels")]
        [QueryParameter]
        public List<string> LabelIds { get; set; }

        /// <summary>
        /// The list of checklist IDs attached to the card. Read-only; use dedicated methods to modify checklists.
        /// </summary>
        /// <remarks>
        /// This property is not updateable here. Use dedicated methods to add or remove checklists.
        /// </remarks>
        [JsonPropertyName("idChecklists")]
        [JsonInclude]
        public List<string> ChecklistIds { get; private set; }

        /// <summary>
        /// The list of member IDs assigned to the card.
        /// </summary>
        [JsonPropertyName("idMembers")]
        [QueryParameter]
        public List<string> MemberIds { get; set; }

        /// <summary>
        /// The list of member IDs who have voted on this card.
        /// </summary>
        [JsonPropertyName("idMembersVoted")]
        [JsonInclude]
        public List<string> MembersVotedIds { get; private set; }

        /// <summary>
        /// The ID of the image attachment to use as the card's cover image.
        /// </summary>
        [JsonPropertyName("idAttachmentCover")]
        [QueryParameter]
        [JsonInclude]
        public string AttachmentCover { get; set; }

        /// <summary>
        /// The full URL to access this card directly in Trello.
        /// </summary>
        [JsonPropertyName("url")]
        [JsonInclude]
        public string Url { get; private set; }

        /// <summary>
        /// The short URL to access this card directly in Trello.
        /// </summary>
        [JsonPropertyName("shortUrl")]
        [JsonInclude]
        public string ShortUrl { get; private set; }

        /// <summary>
        /// The cover of the card, which can be a color or an image.
        /// </summary>
        [JsonPropertyName("cover")]
        public CardCover Cover { get; set; }

        /// <summary>
        /// The custom field items assigned to the card. Only populated if GetCardOptions.IncludeCustomFieldItems is used.
        /// </summary>
        /// <remarks>Use extension methods such as GetCustomFieldValueAsXYZ for convenient value retrieval.</remarks>
        [JsonPropertyName("customFieldItems")]
        [JsonInclude]
        public List<CustomFieldItem> CustomFieldItems { get; private set; }

        /// <summary>
        /// The list of attachments on the card. Only populated if GetCardOptions.IncludeAttachments is used.
        /// </summary>
        [JsonPropertyName("attachments")]
        [JsonInclude]
        public List<Attachment> Attachments { get; internal set; }

        /// <summary>
        /// The list of members assigned to the card. Only populated if GetCardOptions.IncludeMembers is used.
        /// </summary>
        [JsonPropertyName("members")]
        [JsonInclude]
        public List<Member> Members { get; private set; }

        /// <summary>
        /// The list of members who have voted for the card. Only populated if GetCardOptions.IncludeMemberVotes is used.
        /// </summary>
        [JsonPropertyName("membersVoted")]
        [JsonInclude]
        public List<Member> MembersVoted { get; private set; }

        /// <summary>
        /// The board this card is on. Only populated if GetCardOptions.IncludeBoard is used.
        /// </summary>
        [JsonPropertyName("board")]
        [JsonInclude]
        public Board Board { get; internal set; }

        /// <summary>
        /// The list this card is in. Only populated if GetCardOptions.IncludeList is used.
        /// </summary>
        [JsonPropertyName("list")]
        [JsonInclude]
        public List List { get; internal set; }

        /// <summary>
        /// The list of actions performed on this card. Only populated if 'ActionTypes' in GetCardOptions is included.
        /// </summary>
        [JsonPropertyName("actions")]
        [JsonInclude]
        public List<TrelloAction> Actions { get; private set; }

        /// <summary>
        /// The list of checklists attached to the card. Only populated if GetCardOptions.IncludeChecklist is used.
        /// </summary>
        [JsonPropertyName("checklists")]
        [JsonInclude]
        public List<Checklist> Checklists { get; internal set; }

        /// <summary>
        /// The plugin data associated with the card. Only populated if GetCardOptions.IncludePluginData is used.
        /// </summary>
        [JsonPropertyName("pluginData")]
        [JsonInclude]
        public List<PluginData> PluginData { get; private set; }

        /// <summary>
        /// The list of stickers attached to the card. Only populated if GetCardOptions.IncludeStickers is used.
        /// </summary>
        [JsonPropertyName("stickers")]
        [JsonInclude]
        public List<Sticker> Stickers { get; private set; }

        /// <summary>
        /// The role of the card, used to indicate if the card is a mirror, separator or other special function.
        /// </summary>
        [JsonPropertyName("cardRole")]
        [JsonInclude]
        public string CardRole { get; private set; }

        /// <summary>
        /// If the card is a mirror, this contains the ID of the original card being mirrored.
        /// </summary>
        [JsonPropertyName("mirrorSourceId")]
        [JsonInclude]
        public string MirrorSourceId { get; private set; }

        /// <summary>
        /// Indicates whether this card is a mirror of another card.
        /// </summary>
        public bool IsCardMirror => CardRole == "mirror";

        /// <summary>
        /// Indicates whether this card is a Separator
        /// </summary>
        public bool IsSeparator => CardRole == "separator";

        /// <summary>
        /// The named position of the card in the list, such as Top or Bottom. Used for positioning cards by name instead of numeric value.
        /// </summary>
        [JsonIgnore]
        public NamedPosition? NamedPosition { internal get; set; }

        /// <summary>
        /// Indicates whether this card is a template card, which can be used as a reusable pattern for new cards.
        /// </summary>
        [JsonPropertyName("isTemplate")]
        [QueryParameter]
        public bool IsTemplate { get; set; }

        /// <summary>
        /// Initializes a new instance of the Card class with the most common fields.
        /// </summary>
        /// <param name="listId">The ID of the list to add the card to.</param>
        /// <param name="name">The name or title of the card.</param>
        /// <param name="description">The description of the card (optional).</param>
        [Obsolete("Should not be used anymore. Instead use 'AddCardOptions' or Object initializer")]
        public Card(string listId, string name, string description = null)
        {
            Name = name;
            Description = description;
            ListId = listId;
            InitializeLists();
        }

        /// <summary>
        /// Initializes a new instance of the Card class with all supported fields for add or update operations.
        /// </summary>
        /// <param name="listId">The ID of the list to add the card to.</param>
        /// <param name="name">The name or title of the card.</param>
        /// <param name="description">The description of the card.</param>
        /// <param name="start">The start date for the card (should be in UTC).</param>
        /// <param name="due">The due date for the card (should be in UTC).</param>
        /// <param name="dueComplete">Indicates if the card is complete (usually false when creating a new card).</param>
        /// <param name="labelIds">The list of label IDs to assign to the card.</param>
        /// <param name="memberIds">The list of member IDs to assign to the card.</param>
        [Obsolete("Should not be used anymore. Instead use 'AddCardOptions' or Object initializer")]
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
        /// Initializes a new instance of the Card class for serialization purposes.
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