using System.Text.Json.Serialization;

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