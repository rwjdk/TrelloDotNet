using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent a Board on an Action
    /// </summary>
    public class TrelloActionDataBoard
    {
        /// <summary>
        /// Id of the Board
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Board
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }
       
        /// <summary>
        /// The short-link of the Board
        /// </summary>
        [JsonPropertyName("shortLink")]
        [JsonInclude]
        public string ShortLink { get; private set; }
    }
}