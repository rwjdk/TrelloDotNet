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
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Boards)]
        [JsonInclude]
        public List<Board> Boards { get; private set; }

        /// <summary>
        /// Cards of the search
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Cards)]
        [JsonInclude]
        public List<Card> Cards { get; private set; }

        /// <summary>
        /// Organizations of the search
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Organizations)]
        [JsonInclude]
        public List<Organization> Organizations { get; private set; }

        /// <summary>
        /// Options used to conduct the search
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.SearchFields.Options)]
        [JsonInclude]
        public SearchResultOptions Options { get; private set; }
    }
}



