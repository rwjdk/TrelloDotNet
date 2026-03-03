using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Checklist of the Webhook Action Data
    /// </summary>
    public class TrelloActionDataChecklist
    {
        /// <summary>
        /// Checklist Id
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Checklist Name
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }
    }
}




