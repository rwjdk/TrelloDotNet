using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    /// <summary>
    /// Controller of Board-specific Methods
    /// </summary>
    public interface IBoardController
    {
        //todo - Need to Have
        //- Get Actions on Board (perhaps its own controller)

        //todo - Nice to Have
        //- Create Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-post)
        //- Update Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-put)
        //- Manage Custom Fields on board (CRUD)
        //- Get Board Membership (Aka what roles the Token user have on the board) [https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-memberships-get]
        //- Invite members by mail or userId [https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-put + https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-put]
        //- Remove Members (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-delete)
        //- Update Membership (make admin as an example)

        /// <summary>
        /// Get a Board by its Id
        /// </summary>
        /// <param name="id">Id of the Board (in its long or short version)</param>
        /// <returns>The Board</returns>
        Task<Board> GetBoardAsync(string id);
    }
}