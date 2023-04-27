using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Membership on a Board
    /// </summary>
    public class Membership
    {
        /// <summary>
        /// Id of the Membership
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Id of the Member this Membership-entry represent
        /// </summary>
        [JsonPropertyName("idMember")]
        [JsonInclude]
        public string MemberId { get; private set; }

        /// <summary>
        /// The Type of the Members (admin, normal, observer)
        /// </summary>
        [JsonPropertyName("memberType")]
        [JsonInclude]
        [JsonConverter(typeof(EnumViaJsonPropertyConverter<MembershipType>))]
        public MembershipType MemberType { get; private set; }

        /// <summary>
        /// If the member is unconfirmed
        /// </summary>
        [JsonPropertyName("unconfirmed")]
        [JsonInclude]
        public bool Unconfirmed { get; private set; }

        /// <summary>
        /// If the member is deactivated
        /// </summary>
        [JsonPropertyName("deactivated")]
        [JsonInclude]
        public bool Deactivated { get; private set; }
    }
}