using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a member (User)
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Id of the Member
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Full Name of the Member
        /// </summary>
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        /// <summary>
        /// Username of the Member
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{FullName} (Id: {Id})";
        }
    }
}