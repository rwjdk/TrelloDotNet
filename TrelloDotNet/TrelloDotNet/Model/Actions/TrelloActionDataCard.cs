using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent a Card on an Action
    /// </summary>
    public class TrelloActionDataCard
    {
        /// <summary>
        /// Id of the card
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the card
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Id of the card in short form (only unique to the specific board)
        /// </summary>
        [JsonPropertyName("idShort")]
        [JsonInclude]
        public int IdShort { get; private set; }
        
        /// <summary>
        /// The short-link of the card
        /// </summary>
        [JsonPropertyName("shortLink")]
        [JsonInclude]
        public string ShortLink { get; private set; }
    }
}