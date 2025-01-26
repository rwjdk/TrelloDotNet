using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    internal class TokenMemberInformationInbox
    {
        /// <summary>
        /// Your Inbox Ids
        /// </summary>
        [JsonPropertyName("inbox")]
        [JsonInclude]
        public TokenMemberInbox Inbox { get; private set; }
    }
}