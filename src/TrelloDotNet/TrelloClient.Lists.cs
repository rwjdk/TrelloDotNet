using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.GetListOptions;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Adds a new list (column) to a board.
        /// </summary>
        /// <param name="list">The list object to add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The created list</returns>
        public async Task<List> AddListAsync(List list, CancellationToken cancellationToken = default)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(list);
            _queryParametersBuilder.AdjustForNamedPosition(parameters, list.NamedPosition);
            return await _apiRequestController.Post<List>($"{UrlPaths.Lists}", cancellationToken, parameters);
        }

        /// <summary>
        /// Archives a list.
        /// </summary>
        /// <param name="listId">The ID of the list to archive</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The archived list</returns>
        public async Task<List> ArchiveListAsync(string listId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}", cancellationToken, new QueryParameter("closed", true));
        }

        /// <summary>
        /// Permanently deletes a list and any cards it contains. This operation is irreversible. Use ArchiveListAsync for a non-permanent alternative.
        /// </summary>
        /// <param name="listId">The ID of the list to delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteListAsync(string listId, CancellationToken cancellationToken = default)
        {
            await ArchiveListAsync(listId, cancellationToken);
            await _apiRequestController.Delete($"{UrlPaths.Lists}/{listId}", cancellationToken, 0);
        }

        /// <summary>
        /// Reopens an archived list, making it active on the board again.
        /// </summary>
        /// <param name="listId">The ID of the list to reopen</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The reopened list</returns>
        public async Task<List> ReOpenListAsync(string listId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}", cancellationToken, new QueryParameter("closed", false));
        }

        /// <summary>
        /// Updates the properties of an existing list.
        /// </summary>
        /// <param name="listWithChanges">The list object containing the updated properties</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated list</returns>
        public async Task<List> UpdateListAsync(List listWithChanges, CancellationToken cancellationToken = default)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(listWithChanges);
            _queryParametersBuilder.AdjustForNamedPosition(parameters, listWithChanges.NamedPosition);
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listWithChanges.Id}", cancellationToken, parameters);
        }

        /// <summary>
        /// Moves an entire list to another board.
        /// </summary>
        /// <param name="listId">The ID of the list to move</param>
        /// <param name="newBoardId">The ID of the board to move the list to</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated list after moving</returns>
        public async Task<List> MoveListToBoardAsync(string listId, string newBoardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}/idBoard", cancellationToken, new QueryParameter("value", newBoardId));
        }

        /// <summary>
        /// Retrieves a specific list (column) by its ID.
        /// </summary>
        /// <param name="listId">ID of the list to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The requested list</returns>
        public async Task<List> GetListAsync(string listId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List>(GetUrlBuilder.GetList(listId), cancellationToken);
        }

        /// <summary>
        /// Retrieves a specific list (column) by its ID, with additional options for selection.
        /// </summary>
        /// <param name="listId">ID of the list to retrieve</param>
        /// <param name="options">Options for retrieving the list</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The requested list</returns>
        public async Task<List> GetListAsync(string listId, GetListOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List>(GetUrlBuilder.GetList(listId), cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Retrieves all lists (columns) on a specific board.
        /// </summary>
        /// <param name="boardId">ID of the board (long or short version) to retrieve lists from</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of lists (columns) on the board</returns>
        public async Task<List<List>> GetListsOnBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<List>>(GetUrlBuilder.GetListsOnBoard(boardId), cancellationToken);
        }

        /// <summary>
        /// Retrieves all lists (columns) on a specific board, with additional options for filtering and selection.
        /// </summary>
        /// <param name="boardId">ID of the board (long or short version) to retrieve lists from</param>
        /// <param name="options">Options for retrieving the lists</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of lists (columns) on the board</returns>
        public async Task<List<List>> GetListsOnBoardAsync(string boardId, GetListOptions options, CancellationToken cancellationToken = default)
        {
            List<List> lists;
            options.AdjustFieldsBasedOnSelectedOptions();

            if (options.Filter.HasValue)
            {
                lists = await _apiRequestController.Get<List<List>>($"{GetUrlBuilder.GetListsOnBoard(boardId)}/{options.Filter.GetJsonPropertyName()}", cancellationToken, options.GetParameters());
            }
            else
            {
                lists = await _apiRequestController.Get<List<List>>(GetUrlBuilder.GetListsOnBoard(boardId), cancellationToken, options.GetParameters());
            }

            // ReSharper disable once InvertIf
            if (options.IncludeBoard)
            {
                var board = await GetBoardAsync(boardId, new GetBoardOptions
                {
                    BoardFields = options.BoardFields
                }, cancellationToken);
                foreach (List list in lists)
                {
                    list.Board = board;
                }
            }

            if (options.IncludeCards != GetListOptionsIncludeCards.None)
            {
                foreach (List list in lists)
                {
                    list.Cards = FilterCards(list.Cards, options.CardsFilterConditions);
                    list.Cards = OrderCards(list.Cards, options.CardsOrderBy);
                }
            }

            return lists;
        }
    }
}