namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// Object representing a Search Request for Organizations, Boards and Cards
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// The search query with a length of 1 to 16384 characters
        /// </summary>
        public string SearchTerm { get; set; }
        /// <summary>
        /// By default, Trello searches for each word in your query against exactly matching words within Member content. Specifying partial to be true means that we will look for content that starts with any of the words in your query. If you are looking for a Card titled "My Development Status Report", by default you would need to search for "Development". If you have partial enabled, you will be able to search for "dev" but not "velopment".
        /// <remarks>
        /// Default: false
        /// </remarks>
        /// </summary>
        public bool PartialSearch { get; set; }

        /// <summary>
        /// If Organizations should be included in the Search-result
        /// </summary>
        public bool SearchOrganizations { get; set; } = true;

        /// <summary>
        /// If Boards should be included in the Search-result
        /// </summary>
        public bool SearchBoards { get; set; } = true;

        /// <summary>
        /// If Cards be included in the Search-result
        /// </summary>
        public bool SearchCards { get; set; } = true;

        /// <summary>
        /// Filter what specific boards should be included in the search (Tip: Use SearchRequestBoardFilter.Mine to only search your boards)
        /// </summary>
        public SearchRequestBoardFilter BoardFilter { get; set; }

        /// <summary>
        /// What Board-fields should be included in the Search Result: Default: name,idOrganization
        /// </summary>
        public SearchRequestBoardFields BoardFields { get; set; }

        /// <summary>
        /// The maximum number of boards returned. Maximum: 1000, Default: 10
        /// </summary>
        public int? BoardLimit { get; set; }

        /// <summary>
        /// Filter what specific organizations should be included in the search
        /// </summary>
        public SearchRequestOrganizationFilter OrganizationFilter { get; set; }

        /// <summary>
        /// What Board-fields should be included in the Search Result: Default: name,idOrganization
        /// </summary>
        public SearchRequestOrganizationFields OrganizationFields { get; set; }

        /// <summary>
        /// The maximum number of Workspaces to return. Maximum 1000, Default: 10
        /// </summary>
        public int? OrganizationLimit { get; set; }

        /// <summary>
        /// Filter what specific cards should be included in the search
        /// </summary>
        public SearchRequestCardFilter CardFilter { get; set; }

        /// <summary>
        /// What Board-fields should be included in the Search Result: Default: name,idOrganization
        /// </summary>
        public SearchRequestCardFields CardFields { get; set; }

        /// <summary>
        /// The maximum number of cards returned. Maximum: 1000, Default: 10
        /// </summary>
        public int? CardLimit { get; set; }

        /// <summary>
        /// The page of results for cards (Crude Pagination). Maximum: 100, Default: 0
        /// </summary>
        public int? CardPage { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="searchTerm">The search query with a length of 1 to 16384 characters</param>
        /// <param name="partialSearch">By default, Trello searches for each word in your query against exactly matching words within Member content. Specifying partial to be true means that we will look for content that starts with any of the words in your query</param>
        public SearchRequest(string searchTerm, bool partialSearch = false)
        {
            SearchTerm = searchTerm;
            PartialSearch = partialSearch;
        }
    }
}