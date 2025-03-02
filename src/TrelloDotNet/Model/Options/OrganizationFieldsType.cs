using System.Text.Json.Serialization;

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
        [JsonPropertyName("name")]
        Name,

        /// <summary>
        /// The Display (Human) name of the Workspace
        /// </summary>
        [JsonPropertyName("displayName")]
        DisplayName,

        /// <summary>
        /// Description of the Workspace
        /// </summary>
        [JsonPropertyName("desc")]
        Description,

        /// <summary>
        /// List of the Board Ids that is related to the Workspace
        /// </summary>
        [JsonPropertyName("idBoards")]
        BoardIds,

        /// <summary>
        /// The Url of the Workspace
        /// </summary>
        [JsonPropertyName("url")]
        Url,

        /// <summary>
        /// Website of the Workspace
        /// </summary>
        [JsonPropertyName("website")]
        Website,

        /// <summary>
        /// Memberships of the Organization (aka who can do what)
        /// </summary>
        [JsonPropertyName("memberships")]
        Memberships,
    }
}