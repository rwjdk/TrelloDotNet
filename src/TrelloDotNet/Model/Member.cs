using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represents a Member (User) in Trello
    /// </summary>
    [DebuggerDisplay("{FullName} (Id: {Id})")]
    public class Member
    {
        /// <summary>
        /// ID of the Member
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Full name of the Member, if it is public
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
        /// Initials of the Member, if it is public
        /// </summary>
        [JsonPropertyName("initials")]
        [JsonInclude]
        public string Initials { get; private set; }

        /// <summary>
        /// Base URL of the Member's avatar
        /// </summary>
        [JsonPropertyName("avatarUrl")]
        [JsonInclude]
        public string AvatarUrl { get; private set; }

        /// <summary>
        /// 30x30 pixel version of the Member's avatar image
        /// </summary>
        public string AvatarUrl30 => AvatarUrl != null ? $"{AvatarUrl}/30.png" : null;

        /// <summary>
        /// 50x50 pixel version of the Member's avatar image
        /// </summary>
        public string AvatarUrl50 => AvatarUrl != null ? $"{AvatarUrl}/50.png" : null;

        /// <summary>
        /// 170x170 pixel version of the Member's avatar image
        /// </summary>
        public string AvatarUrl170 => AvatarUrl != null ? $"{AvatarUrl}/170.png" : null;

        /// <summary>
        /// Original version of the Member's avatar image
        /// </summary>
        public string AvatarUrlOriginal => AvatarUrl != null ? $"{AvatarUrl}/original.png" : null;

        /// <summary>
        /// Indicates whether the Member has confirmed their email address
        /// </summary>
        [JsonPropertyName("confirmed")]
        [JsonInclude]
        public bool Confirmed { get; private set; }

        /// <summary>
        /// Email of the Member (only populated if field is included in GetMemberOptions)
        /// </summary>
        [JsonPropertyName("email")]
        [JsonInclude]
        public string Email { get; private set; }

        /// <summary>
        /// Last interaction date and time (when the Member did something in the system)
        /// </summary>
        [JsonPropertyName("dateLastImpression")]
        [JsonInclude]
        public DateTimeOffset? LastActivity { get; private set; }

        /// <summary>
        /// Last login date and time (when the Member visited Trello.com)
        /// </summary>
        [JsonPropertyName("dateLastActive")]
        [JsonInclude]
        public DateTimeOffset? LastLogin { get; private set; }

        /// <summary>
        /// Type of the Member (admin, normal, observer, ghost). Only populated if field is included in GetMemberOptions
        /// </summary>
        [JsonPropertyName("memberType")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<MembershipType>))]
        public MembershipType MemberType { get; private set; }

        /// <summary>
        /// Creates a dummy Member for testing purposes
        /// </summary>
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