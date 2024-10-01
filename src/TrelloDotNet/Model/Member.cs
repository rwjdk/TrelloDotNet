using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

// ReSharper disable UnusedMember.Global

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
        /// The base url of this member's avatar
        /// </summary>
        [JsonPropertyName("avatarUrl")]
        [JsonInclude]
        public string AvatarUrl { get; private set; }

        /// <summary>
        /// 30x30 Pixel version of the Members Avatar Image
        /// </summary>
        public string AvatarUrl30 => AvatarUrl != null ? $"{AvatarUrl}/30.png" : null;

        /// <summary>
        /// 50x50 Pixel version of the Members Avatar Image
        /// </summary>
        public string AvatarUrl50 => AvatarUrl != null ? $"{AvatarUrl}/50.png" : null;

        /// <summary>
        /// 170x170 Pixel version of the Members Avatar Image
        /// </summary>
        public string AvatarUrl170 => AvatarUrl != null ? $"{AvatarUrl}/170.png" : null;

        /// <summary>
        /// Original version of the Members Avatar Image
        /// </summary>
        public string AvatarUrlOriginal => AvatarUrl != null ? $"{AvatarUrl}/original.png" : null;

        /// <summary>
        /// Whether the user has confirmed their email address for their account.
        /// </summary>
        [JsonPropertyName("confirmed")]
        [JsonInclude]
        public bool Confirmed { get; private set; }

        /// <summary>
        /// The Email of the member (only populated if field is included in GetMemberOptions)
        /// </summary>
        [JsonPropertyName("email")]
        [JsonInclude]
        public string Email { get; private set; }

        /// <summary>
        /// Last Interaction Datetime (did something in the system)
        /// </summary>
        [JsonPropertyName("dateLastImpression")]
        [JsonInclude]
        public DateTimeOffset? LastActivity { get; private set; }

        /// <summary>
        /// Last Login Datetime (visited Trello.com)
        /// </summary>
        [JsonPropertyName("dateLastActive")]
        [JsonInclude]
        public DateTimeOffset? LastLogin { get; private set; }

        /// <summary>
        /// The Type of the Members (admin, normal, observer) [only populated if field is included in GetMemberOptions]
        /// </summary>
        [JsonPropertyName("memberType")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<MembershipType>))]
        public MembershipType MemberType { get; private set; }

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