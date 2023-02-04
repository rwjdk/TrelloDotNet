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
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Full Name of the Member
        /// </summary>
        [JsonPropertyName("fullName")]
        [JsonInclude]
        public string FullName { get; private set; }

        /// <summary>
        /// Username of the Member
        /// </summary>
        [JsonPropertyName("username")]
        [JsonInclude]
        public string Username { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{FullName} (Id: {Id})";
        }
    }
}