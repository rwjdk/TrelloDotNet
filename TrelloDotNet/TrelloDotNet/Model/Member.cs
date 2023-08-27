using System.Collections.Generic;
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
        /// The full name related to the account, if it is public.
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

        /// <summary>
        /// The initials related to the account, if it is public.
        /// </summary>
        [JsonPropertyName("initials")]
        [JsonInclude]
        public string Initials { get; private set; }

        /// <summary>
        /// The url of this member's avatar
        /// </summary>
        [JsonPropertyName("avatarUrl")]
        [JsonInclude]
        public string AvatarUrl { get; private set; }

        /// <summary>
        /// Whether the user has confirmed their email address for their account.
        /// </summary>
        [JsonPropertyName("confirmed")]
        [JsonInclude]
        public bool Confirmed { get; private set; }

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