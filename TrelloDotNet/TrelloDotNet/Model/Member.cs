using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a member (User)
    /// </summary>
    [DebuggerDisplay("{FullName} (Id: {Id})")]
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

        internal static Member CreateDummy()
        {
            return new Member()
            {
                FullName = "Rasmus",
                Id = "63d1239e857afaa8b003c633",
                Username = "rwj"
            };
        }
    }
}