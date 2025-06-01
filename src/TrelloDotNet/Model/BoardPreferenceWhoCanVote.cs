using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Who can Vote on the board (given the official PowerUp 'Votes' is enabled)
    /// </summary>
    public enum BoardPreferenceWhoCanVote
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// Voting disabled
        /// </summary>
        [JsonPropertyName("disabled")]
        Disabled,

        /// <summary>
        /// Admins and Board Members
        /// </summary>
        [JsonPropertyName("members")]
        Members,

        /// <summary>
        /// Admins and Board Members + Observers
        /// </summary>
        [JsonPropertyName("observers")]
        Observers,

        /// <summary>
        /// Admins and Board Members + Observers and Workspace Members (Require the Board to be a Public or Workspace Board)
        /// </summary>
        [JsonPropertyName("org")]
        Workspace,

        /// <summary>
        /// Everyone (Public - Require the Board to also be Public)
        /// </summary>
        [JsonPropertyName("public")]
        Public,
    }
}