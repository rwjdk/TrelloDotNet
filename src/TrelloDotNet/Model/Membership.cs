using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Membership on a Board
    /// </summary>
    [DebuggerDisplay("Id = {Id}, MemberId = {MemberId}, MemberType = {MemberType}")]
    public class Membership
    {
        /// <summary>
        /// Id of the Membership
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Id of the Member this Membership-entry represent
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.IdMember)]
        [JsonInclude]
        public string MemberId { get; private set; }

        /// <summary>
        /// The Type of the Members (admin, normal, observer)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.MemberType)]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<MembershipType>))]
        public MembershipType MemberType { get; private set; }

        /// <summary>
        /// If the member is unconfirmed
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Unconfirmed)]
        [JsonInclude]
        public bool Unconfirmed { get; private set; }

        /// <summary>
        /// If the member is deactivated
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Deactivated)]
        [JsonInclude]
        public bool Deactivated { get; private set; }
    }
}





