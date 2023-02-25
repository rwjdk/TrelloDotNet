using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent comment Action Data
    /// </summary>
    public class TrelloActionData
    {
        /// <summary>
        /// Text of the comment
        /// </summary>
        [JsonPropertyName("text")]
        [QueryParameter]
        public string Text { get; set; }

        /// <summary>
        /// Simplified Card of the Action
        /// </summary>
        [JsonPropertyName("card")]
        [JsonInclude]
        public TrelloActionDataCard Card { get; private set; }
        
        /// <summary>
        /// Simplified Board of the Action
        /// </summary>
        [JsonPropertyName("board")]
        [JsonInclude]
        public TrelloActionDataBoard Board { get; private set; }
        
        /// <summary>
        /// Simplified List of the Action
        /// </summary>
        [JsonPropertyName("list")]
        [JsonInclude]
        public TrelloActionDataList List { get; private set; }
    }
}