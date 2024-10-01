using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Get PluginData on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The PluginData</returns>
        public async Task<List<PluginData>> GetPluginDataOnCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<PluginData>>(GetUrlBuilder.GetPluginDataOnCard(cardId), cancellationToken);
        }

        /// <summary>
        /// Get PluginData on a Board
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The PluginData</returns>
        public async Task<List<PluginData>> GetPluginDataOfBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<PluginData>>(GetUrlBuilder.GetPluginDataOfBoard(boardId), cancellationToken);
        }
    }
}