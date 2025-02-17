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
        [JsonPropertyName("name")]
        Name,

        /// <summary>
        /// Description of the Board
        /// </summary>
        [JsonPropertyName("desc")]
        Description,

        /// <summary>
        /// URL for the Board
        /// </summary>
        [JsonPropertyName("url")]
        Url,

        /// <summary>
        /// Short Version URL for the Board
        /// </summary>
        [JsonPropertyName("shortUrl")]
        ShortUrl,

        /// <summary>
        /// If the Board is Archived (closed)
        /// </summary>
        [JsonPropertyName("closed")]
        Closed,

        /// <summary>
        /// Id of Organization
        /// </summary>
        [JsonPropertyName("idOrganization")]
        OrganizationId,

        /// <summary>
        /// Id of Enterprise
        /// </summary>
        [JsonPropertyName("idEnterprise")]
        EnterpriseId,

        /// <summary>
        /// If the Board is pinned or not
        /// </summary>
        [JsonPropertyName("pinned")]
        Pinned,

        /// <summary>
        /// The Preferences of the board
        /// </summary>
        [JsonPropertyName("prefs")]
        Preferences,
    }
}