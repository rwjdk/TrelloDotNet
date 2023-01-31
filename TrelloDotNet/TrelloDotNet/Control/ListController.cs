using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    public class ListController : IListController
    {
        private readonly ApiRequestController _requestController;

        internal ListController(ApiRequestController requestController)
        {
            _requestController = requestController;
        }

        public async Task<List> GetAsync(string listId)
        {
            return await _requestController.GetResponse<List>($"{Constants.UrlSuffixGroup.Lists}/{listId}");
        }

        public async Task<List> AddAsync(Board board, string name)
        {
            return await AddAsync(board.Id, name);
        }

        public async Task<List> AddAsync(string longBoardId, string name)
        {
            var parameters = new []
            {
                new UriParameter("name", name),
                new UriParameter("idBoard", longBoardId)
            };
            return await _requestController.Post<List>($"{Constants.UrlSuffixGroup.Lists}", parameters);
        }
    }
}