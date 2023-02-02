using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    /// <inheritdoc />
    public class BoardController : IBoardController
    {
        private readonly ApiRequestController _apiRequestController;
        // ReSharper disable once NotAccessedField.Local
        private readonly TrelloClient _parent;

        internal BoardController(ApiRequestController apiRequestController, TrelloClient parent)
        {
            _parent = parent;
            _apiRequestController = apiRequestController;
        }

        /// <inheritdoc />
        public async Task<Board> GetBoardAsync(string id)
        {
            return await _apiRequestController.Get<Board>($"{Constants.UrlSuffixGroup.Boards}/{id}");
        }
    }
}