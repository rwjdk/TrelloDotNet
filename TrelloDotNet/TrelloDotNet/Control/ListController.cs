using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    /// <inheritdoc />
    public class ListController : IListController
    {
        private readonly ApiRequestController _apiRequestController;
        // ReSharper disable once NotAccessedField.Local
        private TrelloClient _parent;
        private readonly UriParameterBuilder _uriParameterBuilder;

        internal ListController(ApiRequestController apiRequestController, TrelloClient parent)
        {
            _parent = parent;
            _apiRequestController = apiRequestController;
            _uriParameterBuilder = new UriParameterBuilder();
        }

        /// <inheritdoc />
        public async Task<List> GetListAsync(string listId)
        {
            return await _apiRequestController.Get<List>($"{Constants.UrlSuffixGroup.Lists}/{listId}");
        }

        /// <inheritdoc />
        public async Task<List> AddListAsync(List list)
        {
            var parameters = _uriParameterBuilder.GetViaUriParameterAttribute(list);
            return await _apiRequestController.Post<List>($"{Constants.UrlSuffixGroup.Lists}", parameters.ToArray());
        }

        /// <inheritdoc />
        public async Task<List<List>> GetListsOnBoardAsync(string boardId, ListFilter filter = ListFilter.Open)
        {
            var parameters = new[]
            {
                new UriParameter("filter", filter.GetJsonPropertyName())
            };
            return await _apiRequestController.Get<List<List>>($"{Constants.UrlSuffixGroup.Boards}/{boardId}/lists", parameters);
        }
    }
}