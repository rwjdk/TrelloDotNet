using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdShort)]
        IdShort,

        /// <summary>
        /// Id of the board the card is on
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.IdBoard)]
        BoardId,

        /// <summary>
        /// Name of the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Name)]
        Name,

        /// <summary>
        /// Description of the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Desc)]
        Description,

        /// <summary>
        /// If the card is Archived (closed)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Closed)]
        Closed,

        /// <summary>
        /// The position of the card in the current list
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        Position,

        /// <summary>
        /// If the card is Watched (subscribed) by the owner of the Token used against the API
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Subscribed)]
        Subscribed,

        /// <summary>
        /// Id of the List the Card belong to
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.IdList)]
        ListId,

        /// <summary>
        /// When there was last activity on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.DateLastActivity)]
        LastActivity,

        /// <summary>
        /// The Start-date of the work on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Start)]
        Start,

        /// <summary>
        /// The Due-date of the work on the card should be finished
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Due)]
        Due,

        /// <summary>
        /// If the Card is completed
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.DueComplete)]
        DueComplete,

        /// <summary>
        /// The labels (in details) that are on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.LabelFields.Labels)]
        Labels,

        /// <summary>
        /// Ids of the Labels that are on the Card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdLabels)]
        LabelIds,

        /// <summary>
        /// Ids of the Checklists on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdChecklists)]
        ChecklistIds,

        /// <summary>
        /// Ids of members assigned to the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdMembers)]
        MemberIds,

        /// <summary>
        /// Ids of members that voted on the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdMembersVoted)]
        MembersVotedIds,

        /// <summary>
        /// Id of the image attachment of this card to use as its cover
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdAttachmentCover)]
        AttachmentCover,

        /// <summary>
        /// Url you can use to get to the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Url)]
        Url,

        /// <summary>
        /// Short Url you can use to get to the card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.ShortUrl)]
        ShortUrl,

        /// <summary>
        /// Cover of the Card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Cover)]
        Cover,

        /// <summary>
        /// If the Card is a Template
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IsTemplate)]
        IsTemplate,

        /// <summary>
        /// The role of the Card
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.CardRole)]
        CardRole
    }
}





