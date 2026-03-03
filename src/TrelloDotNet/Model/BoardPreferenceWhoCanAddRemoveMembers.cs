using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Who can write comments on the cards
    /// </summary>
    public enum BoardPreferenceWhoCanComment
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// Commenting disabled
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Disabled)]
        Disabled,

        /// <summary>
        /// Admins and Board Members
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Members)]
        Members,

        /// <summary>
        /// Admins and Board Members + Observers
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Observers)]
        Observers,

        /// <summary>
        /// Admins and Board Members + Observers and Workspace Members (Require the Board to be a Public or Workspace Board)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Org)]
        Workspace,

        /// <summary>
        /// Everyone (Public - Require the Board to also be Public)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Public)]
        Public,
    }
}





