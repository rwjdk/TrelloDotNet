using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent an Organization
    /// </summary>
    public class TrelloActionDataOrganization
    {
        /// <summary>
        /// Id of the Organization 
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Display Name of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.DisplayName)]
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




