using System.Text.Json.Serialization;

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
        Observer
    }
}