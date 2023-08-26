namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// Filter what specific boards should be included in the search (Tip: Use SearchRequestBoardFilter.Mine to only search your boards)
    /// </summary>
    public class SearchRequestBoardFilter
    {
        /// <summary>
        /// Ids of Boards to search in
        /// </summary>
        public string[] BoardIds { get; }
        /// <summary>
        /// Predefined Filter that include 'Mine' Boards
        /// </summary>
        public static SearchRequestBoardFilter Mine => new SearchRequestBoardFilter("mine");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boardIds">Ids of Boards to search in</param>
        public SearchRequestBoardFilter(params string[] boardIds)
        {
            BoardIds = boardIds;
        }
    }
}