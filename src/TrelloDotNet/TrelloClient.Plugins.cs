using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Retrieves all plugins registered on a specific board.
        /// </summary>
        /// <param name="boardId">ID of the board to retrieve plugins for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of plugins registered on the board</returns>
        public async Task<List<Plugin>> GetPluginsOnBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Plugin>>($"{UrlPaths.Boards}/{boardId}/plugins", cancellationToken);
        }
    }
}