using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    /// <summary>
    /// List-related operations
    /// </summary>
    public interface IListController
    {
        //todo - Need to have
        //- Update/Move List (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-idboard-put)
        //- Get Filtered Lists

        //todo - Nice to have
        //- Update List (+archive) (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-field-put)
        //- Position of list (research)
        //- Delete List
        //- Archive All Cards on list (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-archiveallcards-post)
        //- Move all Cards on list (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-moveallcards-post)
        //- Get actions on list (perhaps own controller)

        /// <summary>
        /// Get a specific List (Column) based on it's Id
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <returns></returns>
        Task<List> GetListAsync(string listId);
        
        /// <summary>
        /// Add a List to a Board
        /// </summary>
        /// <remarks>
        /// The Provided BoardId the list should be added to need to be the long version of the BoardId as API does not support the short version
        /// </remarks>
        /// <param name="list">List to add</param>
        /// <returns>The Create list</returns>
        Task<List> AddListAsync(List list);

        /// <summary>
        /// Get Lists (Columns) on a Board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">Filter Lists based on status</param>
        /// <returns>List of Lists (Columns)</returns>
        Task<List<List>> GetListsOnBoardAsync(string boardId, ListFilter filter = ListFilter.Open);
    }
}