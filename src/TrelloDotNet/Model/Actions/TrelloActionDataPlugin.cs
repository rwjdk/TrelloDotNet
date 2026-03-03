using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represents plugin data associated with a Trello action, including the plugin's ID and name.
    /// </summary>
    public class TrelloActionDataPlugin
    {
        /// <summary>
        /// The ID of the plugin related to the action.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The name of the plugin related to the action.
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }
    }
}




