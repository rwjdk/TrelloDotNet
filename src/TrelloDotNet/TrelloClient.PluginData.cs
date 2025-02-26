using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        /// Get PluginData for a specific Plugin on a card (Note: If no data is stored yet, this will return null)
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="pluginId">Id of the Plugin (Use TrelloClient.getPlugins to get the ID)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The PluginData</returns>
        public async Task<PluginData> GetPluginDataOnCardAsync(string cardId, string pluginId, CancellationToken cancellationToken = default)
        {
            return (await _apiRequestController.Get<List<PluginData>>(GetUrlBuilder.GetPluginDataOnCard(cardId), cancellationToken)).FirstOrDefault(x => x.PluginId == pluginId);
        }

        /// <summary>
        /// Get PluginData for a specific Plugin on a card (Note: If no data is stored yet, this will return null)
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="pluginId">Id of the Plugin (Use TrelloClient.getPlugins to get the ID)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The PluginData</returns>
        public async Task<T> GetPluginDataOnCardAsync<T>(string cardId, string pluginId, CancellationToken cancellationToken = default)
        {
            PluginData pluginData = await GetPluginDataOnCardAsync(cardId, pluginId, cancellationToken);
            if (pluginData == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(pluginData.Value);
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

        /// <summary>
        /// Get PluginData for a specific Plugin on a Board (Note: If no data is stored yet, this will return null)
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="pluginId">Id of the Plugin</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The PluginData</returns>
        public async Task<PluginData> GetPluginDataOfBoardAsync(string boardId, string pluginId, CancellationToken cancellationToken = default)
        {
            return (await _apiRequestController.Get<List<PluginData>>(GetUrlBuilder.GetPluginDataOfBoard(boardId), cancellationToken)).FirstOrDefault(x => x.PluginId == pluginId);
        }

        /// <summary>
        /// Get PluginData for a specific Plugin on a Board (Note: If no data is stored yet, this will return null)
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="pluginId">Id of the Plugin</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The PluginData</returns>
        public async Task<T> GetPluginDataOfBoardAsync<T>(string boardId, string pluginId, CancellationToken cancellationToken = default)
        {
            PluginData pluginData = await GetPluginDataOfBoardAsync(boardId, pluginId, cancellationToken);
            if (pluginData == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(pluginData.Value);
        }
    }
}