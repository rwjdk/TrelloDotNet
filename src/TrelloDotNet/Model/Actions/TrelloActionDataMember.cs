using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represents a member involved in a Trello action, including the member's ID and name.
    /// </summary>
    public class TrelloActionDataMember
    {
        /// <summary>
        /// The ID of the member involved in the action.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The name of the member involved in the action.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }
    }
}