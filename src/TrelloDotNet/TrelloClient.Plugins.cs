using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Get the plugins registered on the Board
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>The Plugins</returns>
        public async Task<List<Plugin>> GetPluginsOnBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Plugin>>($"{UrlPaths.Boards}/{boardId}/plugins", cancellationToken);
        }
    }
}