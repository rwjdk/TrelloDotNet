using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Object representing your Trello Inbox
    /// </summary>
    public class TokenMemberInbox
    {
        /// <summary>
        /// "Board" Id of your Inbox
        /// </summary>
        [JsonPropertyName("idBoard")]
        [JsonInclude]
        public string BoardId { get; set; }

        /// <summary>
        /// "List" Id of your Inbox
        /// </summary>
        [JsonPropertyName("idList")]
        [JsonInclude]
        public string ListId { get; set; }

        /// <summary>
        /// "Organization" Id of your Inbox
        /// </summary>
        [JsonPropertyName("idOrganizations")]
        [JsonInclude]
        public string OrganizationId { get; private set; }
    }
}