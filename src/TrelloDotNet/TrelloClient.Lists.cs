﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Add a List to a Board
        /// </summary>
        /// <remarks>
        /// The Provided BoardId the list should be added to need to be the long version of the BoardId as API does not support the short version
        /// </remarks>
        /// <param name="list">List to add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Create list</returns>
        public async Task<List> AddListAsync(List list, CancellationToken cancellationToken = default)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(list);
            _queryParametersBuilder.AdjustForNamedPosition(parameters, list.NamedPosition);
            return await _apiRequestController.Post<List>($"{UrlPaths.Lists}", cancellationToken, parameters);
        }

        /// <summary>
        /// Archive a List
        /// </summary>
        /// <param name="listId">The id of list that should be Archived</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Archived List</returns>
        public async Task<List> ArchiveListAsync(string listId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}", cancellationToken, new QueryParameter("closed", true));
        }

        /// <summary>
        /// Reopen a List (Send back to the board)
        /// </summary>
        /// <param name="listId">The id of list that should be Reopened</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Archived List</returns>
        public async Task<List> ReOpenListAsync(string listId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}", cancellationToken, new QueryParameter("closed", false));
        }

        /// <summary>
        /// Update a List
        /// </summary>
        /// <param name="listWithChanges">The List with the changes</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Updated List</returns>
        public async Task<List> UpdateListAsync(List listWithChanges, CancellationToken cancellationToken = default)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(listWithChanges);
            _queryParametersBuilder.AdjustForNamedPosition(parameters, listWithChanges.NamedPosition);
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listWithChanges.Id}", cancellationToken, parameters);
        }

        /// <summary>
        /// Move an entire list to another board
        /// </summary>
        /// <param name="listId">The id of the List to move</param>
        /// <param name="newBoardId">The id of the board the list should be moved to [It needs to be the long version of the boardId]</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Updated List</returns>
        public async Task<List> MoveListToBoardAsync(string listId, string newBoardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}/idBoard", cancellationToken, new QueryParameter("value", newBoardId));
        }

        /// <summary>
        /// Get Lists on board based on their status
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">The Selected Filter</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Cards</returns>
        public async Task<List<List>> GetListsOnBoardFilteredAsync(string boardId, ListFilter filter, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<List>>($"{GetUrlBuilder.GetListsOnBoard(boardId)}/{filter.GetJsonPropertyName()}", cancellationToken);
        }

        /// <summary>
        /// Get a specific List (Column) based on its Id
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<List> GetListAsync(string listId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List>(GetUrlBuilder.GetList(listId), cancellationToken);
        }

        /// <summary>
        /// Get Lists (Columns) on a Board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Lists (Columns)</returns>
        public async Task<List<List>> GetListsOnBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<List>>(GetUrlBuilder.GetListsOnBoard(boardId), cancellationToken);
        }
    }
}