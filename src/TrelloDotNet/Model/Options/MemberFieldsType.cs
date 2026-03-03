using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [JsonPropertyName(Constants.TrelloIds.MemberFields.FullName)]
        FullName,

        /// <summary>
        /// Username of the Member
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Username)]
        Username,

        /// <summary>
        /// The initials related to the account, if it is public.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Initials)]
        Initials,

        /// <summary>
        /// The url of this member's avatar
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.AvatarUrl)]
        AvatarUrl,

        /// <summary>
        /// Whether the user has confirmed their email address for their account.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Confirmed)]
        Confirmed,

        /// <summary>
        /// Email of Member
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Email)]
        Email,

        /// <summary>
        /// Type Member (admin, normal, observer)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.MemberType)]
        MemberType,

        /// <summary>
        /// Last time of activity (did something in Trello)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.DateLastImpression)]
        LastActivityDate,

        /// <summary>
        /// Last time of user logged in
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.DateLastActive)]
        LastLoginDate,
    }
}



