using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// FieldType of a member
    /// </summary>
    public enum MemberFieldsType
    {
        /// <summary>
        /// The full name related to the account, if it is public.
        /// </summary>
        [JsonPropertyName("fullName")] FullName,

        /// <summary>
        /// Username of the Member
        /// </summary>
        [JsonPropertyName("username")] Username,

        /// <summary>
        /// The initials related to the account, if it is public.
        /// </summary>
        [JsonPropertyName("initials")] Initials,

        /// <summary>
        /// The url of this member's avatar
        /// </summary>
        [JsonPropertyName("avatarUrl")] AvatarUrl,

        /// <summary>
        /// Whether the user has confirmed their email address for their account.
        /// </summary>
        [JsonPropertyName("confirmed")] Confirmed,

        /// <summary>
        /// Email of Member
        /// </summary>
        [JsonPropertyName("email")] Email,

        /// <summary>
        /// Type Member (admin, normal, observer)
        /// </summary>
        [JsonPropertyName("memberType")] MemberType,
    }
}