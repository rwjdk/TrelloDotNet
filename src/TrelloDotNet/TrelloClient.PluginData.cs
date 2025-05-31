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
        /// Retrieves all plugin data objects attached to a specific card.
        /// </summary>
        /// <param name="cardId">ID of the card to retrieve plugin data for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of plugin data objects on the card</returns>
        public async Task<List<PluginData>> GetPluginDataOnCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<PluginData>>(GetUrlBuilder.GetPluginDataOnCard(cardId), cancellationToken);
        }

        /// <summary>
        /// Retrieves the plugin data for a specific plugin on a card. Returns null if no data is stored for the plugin.
        /// </summary>
        /// <param name="cardId">ID of the card to retrieve plugin data for</param>
        /// <param name="pluginId">ID of the plugin (use TrelloClient.getPlugins to get the ID)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The plugin data object for the specified plugin, or null if not found</returns>
        public async Task<PluginData> GetPluginDataOnCardAsync(string cardId, string pluginId, CancellationToken cancellationToken = default)
        {
            return (await _apiRequestController.Get<List<PluginData>>(GetUrlBuilder.GetPluginDataOnCard(cardId), cancellationToken)).FirstOrDefault(x => x.PluginId == pluginId);
        }

        /// <summary>
        /// Retrieves the plugin data for a specific plugin on a card and deserializes it to the specified type. Returns default if no data is stored for the plugin.
        /// </summary>
        /// <param name="cardId">ID of the card to retrieve plugin data for</param>
        /// <param name="pluginId">ID of the plugin (use TrelloClient.getPlugins to get the ID)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <typeparam name="T">The type to deserialize the plugin data value to</typeparam>
        /// <returns>The deserialized plugin data object, or default if not found</returns>
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
        /// Retrieves all plugin data objects attached to a specific board.
        /// </summary>
        /// <param name="boardId">ID of the board to retrieve plugin data for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of plugin data objects on the board</returns>
        public async Task<List<PluginData>> GetPluginDataOfBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<PluginData>>(GetUrlBuilder.GetPluginDataOfBoard(boardId), cancellationToken);
        }

        /// <summary>
        /// Retrieves the plugin data for a specific plugin on a board. Returns null if no data is stored for the plugin.
        /// </summary>
        /// <param name="boardId">ID of the board to retrieve plugin data for</param>
        /// <param name="pluginId">ID of the plugin</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The plugin data object for the specified plugin, or null if not found</returns>
        public async Task<PluginData> GetPluginDataOfBoardAsync(string boardId, string pluginId, CancellationToken cancellationToken = default)
        {
            return (await _apiRequestController.Get<List<PluginData>>(GetUrlBuilder.GetPluginDataOfBoard(boardId), cancellationToken)).FirstOrDefault(x => x.PluginId == pluginId);
        }

        /// <summary>
        /// Retrieves the plugin data for a specific plugin on a board and deserializes it to the specified type. Returns default if no data is stored for the plugin.
        /// </summary>
        /// <param name="boardId">ID of the board to retrieve plugin data for</param>
        /// <param name="pluginId">ID of the plugin</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <typeparam name="T">The type to deserialize the plugin data value to</typeparam>
        /// <returns>The deserialized plugin data object, or default if not found</returns>
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