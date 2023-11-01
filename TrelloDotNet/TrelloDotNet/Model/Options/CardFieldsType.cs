using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Type of field on a card
    /// </summary>
    public enum CardFieldsType
    {
        /// <summary>
        /// Id of the card in short form (only unique to the specific board)
        /// </summary>
        [JsonPropertyName("idShort")] IdShort,

        /// <summary>
        /// Id of the board the card is on
        /// </summary>
        [JsonPropertyName("idBoard")] BoardId,

        /// <summary>
        /// Name of the card
        /// </summary>
        [JsonPropertyName("name")] Name,

        /// <summary>
        /// Description of the card
        /// </summary>
        [JsonPropertyName("desc")] Description,

        /// <summary>
        /// If the card is Archived (closed)
        /// </summary>
        [JsonPropertyName("closed")] Closed,

        /// <summary>
        /// The position of the card in the current list
        /// </summary>
        [JsonPropertyName("pos")] Position,

        /// <summary>
        /// If the card is Watched (subscribed) by the owner of the Token used against the API
        /// </summary>
        [JsonPropertyName("subscribed")] Subscribed,

        /// <summary>
        /// Id of the List the Card belong to
        /// </summary>
        [JsonPropertyName("idList")] ListId,

        /// <summary>
        /// When there was last activity on the card
        /// </summary>
        [JsonPropertyName("dateLastActivity")] LastActivity,

        /// <summary>
        /// The Start-date of the work on the card
        /// </summary>
        [JsonPropertyName("start")] Start,

        /// <summary>
        /// The Due-date of the work on the card should be finished
        /// </summary>
        [JsonPropertyName("due")] Due,

        /// <summary>
        /// If the due work is completed
        /// </summary>
        [JsonPropertyName("dueComplete")] DueComplete,

        /// <summary>
        /// The labels (in details) that are on the card
        /// </summary>
        [JsonPropertyName("labels")] Labels,

        /// <summary>
        /// Ids of the Labels that are on the Card
        /// </summary>
        [JsonPropertyName("idLabels")] LabelIds,

        /// <summary>
        /// Ids of the Checklists on the card
        /// </summary>
        [JsonPropertyName("idChecklists")] ChecklistIds,

        /// <summary>
        /// Ids of members assigned to the card
        /// </summary>
        [JsonPropertyName("idMembers")] MemberIds,

        /// <summary>
        /// Ids of members that voted on the card
        /// </summary>
        [JsonPropertyName("idMembersVoted")] MembersVotedIds,

        /// <summary>
        /// Id of the image attachment of this card to use as its cover
        /// </summary>
        [JsonPropertyName("idAttachmentCover")]
        AttachmentCover,

        /// <summary>
        /// Url you can use to get to the card
        /// </summary>
        [JsonPropertyName("url")] Url,

        /// <summary>
        /// Short Url you can use to get to the card
        /// </summary>
        [JsonPropertyName("shortUrl")] ShortUrl,

        /// <summary>
        /// Cover of the Card
        /// </summary>
        [JsonPropertyName("cover")] Cover,
    }
}