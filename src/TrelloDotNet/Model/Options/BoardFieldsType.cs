using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// The Type of field on a board
    /// </summary>
    public enum BoardFieldsType
    {
        /// <summary>
        /// Name of the board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Name)]
        Name,

        /// <summary>
        /// Description of the Board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Desc)]
        Description,

        /// <summary>
        /// URL for the Board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Url)]
        Url,

        /// <summary>
        /// Short Version URL for the Board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.ShortUrl)]
        ShortUrl,

        /// <summary>
        /// If the Board is Archived (closed)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Closed)]
        Closed,

        /// <summary>
        /// Id of Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.IdOrganization)]
        OrganizationId,

        /// <summary>
        /// Id of Enterprise
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.IdEnterprise)]
        EnterpriseId,

        /// <summary>
        /// If the Board is pinned or not
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Pinned)]
        Pinned,

        /// <summary>
        /// If the Board is Subscribed (watched) or not
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Subscribed)]
        Subscribed,

        /// <summary>
        /// The Preferences of the board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Prefs)]
        Preferences,
    }
}





