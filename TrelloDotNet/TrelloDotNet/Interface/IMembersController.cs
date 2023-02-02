using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    /// <summary>
    /// Operations on Members (Users)
    /// </summary>
    public interface IMembersController
    {
        //todo - Need to Have
        //- Get member (https://developer.atlassian.com/cloud/trello/rest/api-group-members/#api-members-id-get)

        //todo - Nice to Have
        //- Get members of a Card
        //- Member Actions

        /// <summary>
        /// Get the Members (users) of a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Members</returns>
        Task<List<Member>> GetMembersOfBoardAsync(string boardId);
    }
}