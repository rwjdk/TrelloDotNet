using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The various Membership types
    /// </summary>
    public enum MembershipType
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Administrator
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Admin)]
        Admin,

        /// <summary>
        /// Normal User
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Normal)]
        Normal,

        /// <summary>
        /// Observer User 
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Observer)]
        Observer,

        /// <summary>
        /// Ghost User  (Not yet joined)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Ghost)]
        Ghost
    }
}





