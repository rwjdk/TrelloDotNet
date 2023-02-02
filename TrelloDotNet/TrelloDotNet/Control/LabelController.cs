using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    /// <inheritdoc />
    public class LabelController : ILabelController
    {
        private readonly ApiRequestController _apiRequestController;
        // ReSharper disable once NotAccessedField.Local
        private readonly TrelloClient _parent;

        internal LabelController(ApiRequestController apiRequestController, TrelloClient parent)
        {
            _apiRequestController = apiRequestController;
            _parent = parent;
        }

        /// <inheritdoc />
        public async Task<List<Label>> GetLabelsOfBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Label>>($"{Constants.UrlSuffixGroup.Boards}/{boardId}/labels");
        }
    }
}