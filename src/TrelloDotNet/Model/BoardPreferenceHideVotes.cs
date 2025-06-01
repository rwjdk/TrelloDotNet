using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Hide Votes
    /// </summary>
    public enum BoardPreferenceHideVotes
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// Hide votes from other members
        /// </summary>
        [JsonPropertyName("true")]
        Hide,

        /// <summary>
        /// Do not hide votes from other members
        /// </summary>
        [JsonPropertyName("false")]
        DoNotHide
    }
}