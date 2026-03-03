using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Data Attachment
    /// </summary>
    public class TrelloActionDataAttachment
    {
        /// <summary>
        /// Id of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// URL of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Url)]
        [JsonInclude]
        public string Url { get; private set; }
    }
}




