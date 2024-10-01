namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// Filter what specific cards should be included in the search
    /// </summary>
    public class SearchRequestCardFilter
    {
        /// <summary>
        /// Ids of cards to include in search
        /// </summary>
        public string[] CardIds { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cardIds">Ids of cards to include in search</param>
        public SearchRequestCardFilter(params string[] cardIds)
        {
            CardIds = cardIds;
        }
    }
}