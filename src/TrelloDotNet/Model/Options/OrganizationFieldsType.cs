using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// The various fields on an Organization (Workspace)
    /// </summary>
    public enum OrganizationFieldsType
    {
        /// <summary>
        /// Technical name of the Workspace (See DisplayName for the 'real' name)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Name)]
        Name,

        /// <summary>
        /// The Display (Human) name of the Workspace
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.DisplayName)]
        DisplayName,

        /// <summary>
        /// Description of the Workspace
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Desc)]
        Description,

        /// <summary>
        /// List of the Board Ids that is related to the Workspace
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.IdBoards)]
        BoardIds,

        /// <summary>
        /// The Url of the Workspace
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Url)]
        Url,

        /// <summary>
        /// Website of the Workspace
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Website)]
        Website,

        /// <summary>
        /// Memberships of the Organization (aka who can do what)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Memberships)]
        Memberships,
    }
}





