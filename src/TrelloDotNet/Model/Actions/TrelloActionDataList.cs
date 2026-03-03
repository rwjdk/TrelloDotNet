using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent a List on an Action
    /// </summary>
    public class TrelloActionDataList
    {
        /// <summary>
        /// Id of the List
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Board
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }
    }
}




