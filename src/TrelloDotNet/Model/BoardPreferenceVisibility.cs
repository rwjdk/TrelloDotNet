using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Visibility of a board
    /// </summary>
    public enum BoardPreferenceVisibility
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// Board is Private
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Private)]
        Private,

        /// <summary>
        /// Board is Workspace (All workspace members can see/edit this board)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Org)]
        Workspace,

        /// <summary>
        /// The Board is Public
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Public)]
        Public
    }
}





