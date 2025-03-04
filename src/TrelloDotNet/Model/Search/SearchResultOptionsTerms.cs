using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// The Search-terms used in the search
    /// </summary>
    public class SearchResultOptionsTerms
    {
        /// <summary>
        /// The Test searched for
        /// </summary>
        [JsonPropertyName("text")]
        [JsonInclude]
        public string Text { get; private set; }

        /// <summary>
        /// If the search was partial search term matching
        /// </summary>
        [JsonPropertyName("partial")]
        [JsonInclude]
        public bool Partial { get; private set; }
    }
}