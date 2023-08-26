using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// The result of a Search
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Boards of the search
        /// </summary>
        [JsonPropertyName("boards")]
        [JsonInclude]
        public List<Board> Boards { get; private set; }

        /// <summary>
        /// Cards of the search
        /// </summary>
        [JsonPropertyName("cards")]
        [JsonInclude]
        public List<Card> Cards { get; private set; }

        /// <summary>
        /// Organizations of the search
        /// </summary>
        [JsonPropertyName("organizations")]
        [JsonInclude]
        public List<Organization> Organizations { get; private set; }

        /// <summary>
        /// Options used to conduct the search
        /// </summary>
        [JsonPropertyName("options")]
        [JsonInclude]
        public SearchResultOptions Options { get; private set; }
    }
}