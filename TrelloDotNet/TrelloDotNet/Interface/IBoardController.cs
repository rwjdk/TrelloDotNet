using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    public interface IBoardController
    {
        Task<Board> GetAsync(string longOrShortBoardId);
        /// <summary>
        /// Get Lists on a Board
        /// </summary>
        /// <param name="longOrShortBoardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">Filter Lists based on status</param>
        /// <returns>List of Trello Lists</returns>
        Task<ListWithRawJsonIncluded<List>> GetListsAsync(string longOrShortBoardId, ListFilter filter = ListFilter.Open);
        Task<ListWithRawJsonIncluded<Label>> GetLabelsAsync(string longOrShortBoardId);
        Task<ListWithRawJsonIncluded<Card>> GetCardsAsync(string longOrShortBoardId);
        Task<ListWithRawJsonIncluded<Card>> GetCardsFilteredAsync(string longOrShortBoardId, CardsFilter filter = CardsFilter.Open);
        Task<ListWithRawJsonIncluded<Member>> GetMembersAsync(string longOrShortBoardId);
    }
}