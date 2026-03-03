using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Represent Webhook Organization Data
    /// </summary>
    public class WebhookActionDataOrganization
    {
        /// <summary>
        /// Id of the Organization 
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Display Name of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.DisplayName)]
        [JsonInclude]
        public string DisplayName { get; private set; }

        /// <summary>
        /// Website of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Website)]
        [JsonInclude]
        public string Website { get; private set; }
    }
}





