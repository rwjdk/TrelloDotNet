using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Who can add/remove members to the board
    /// </summary>
    public enum BoardPreferenceWhoCanAddAndRemoveMembers
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// Any Board Member
        /// </summary>
        [JsonPropertyName("members")]
        Members,

        /// <summary>
        /// Only Admins
        /// </summary>
        [JsonPropertyName("admins")]
        Admins,
    }
}