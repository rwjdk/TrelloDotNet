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
        /// Administrator
        /// </summary>
        [JsonPropertyName("admin")]
        Admin,

        /// <summary>
        /// Normal User
        /// </summary>
        [JsonPropertyName("normal")]
        Normal,

        /// <summary>
        /// Observer User 
        /// </summary>
        [JsonPropertyName("observer")]
        Observer,

        /// <summary>
        /// Ghost User  (Not yet joined)
        /// </summary>
        [JsonPropertyName("ghost")]
        Ghost
    }
}