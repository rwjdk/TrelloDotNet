using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    /// <inheritdoc />
    public class MembersController : IMembersController
    {
        private readonly ApiRequestController _apiRequestController;
        // ReSharper disable once NotAccessedField.Local
        private readonly ITrelloClient _parent;

        internal MembersController(ApiRequestController apiRequestController, ITrelloClient parent)
        {
            _apiRequestController = apiRequestController;
            _parent = parent;
        }

        /// <inheritdoc />
        public async Task<List<Member>> GetMembersOfBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Member>>($"{Constants.UrlSuffixGroup.Boards}/{boardId}/members/");
        }
    }
}