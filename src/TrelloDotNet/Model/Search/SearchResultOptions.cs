using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// The Options used to conduct a search
    /// </summary>
    public class SearchResultOptions
    {
        /// <summary>
        /// The Search-terms used in the search
        /// </summary>
        [JsonPropertyName("terms")]
        [JsonInclude]
        public List<SearchResultOptionsTerms> Terms { get; private set; }

        /// <summary>
        /// The different types of things that was searched (Boards, Cards, and/or Organizations)
        /// </summary>
        [JsonPropertyName("modelTypes")]
        [JsonInclude]
        public List<string> ModelTypes { get; private set; }

        /// <summary>
        /// If the search was partial search term matching
        /// </summary>
        [JsonPropertyName("partial")]
        [JsonInclude]
        public bool Partial { get; private set; }
    }
}