using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.UpdateBoardPreferencesOptions;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Creates a new Trello board with the specified properties and optional configuration options.
        /// </summary>
        /// <param name="board">The Board object containing the properties for the new board</param>
        /// <param name="options">Optional settings for the new board, such as default labels or lists</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The newly created Board object</returns>
        public async Task<Board> AddBoardAsync(Board board, AddBoardOptions options = null, CancellationToken cancellationToken = default)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(board).ToList();
            if (options != null)
            {
                parameters.AddRange(_queryParametersBuilder.GetViaQueryParameterAttributes(options));
            }

            return await _apiRequestController.Post<Board>($"{UrlPaths.Boards}", cancellationToken, parameters.ToArray());
        }

        /// <summary>
        /// Closes (archives) a board, making it inactive but not permanently deleted. The board can be reopened later.
        /// </summary>
        /// <param name="boardId">ID of the board to close</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The closed Board object</returns>
        public async Task<Board> CloseBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{boardId}", cancellationToken, new QueryParameter("closed", true));
        }

        /// <summary>
        /// Reopens a board that was previously closed (archived), making it active again.
        /// </summary>
        /// <param name="boardId">ID of the board to reopen</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The reopened Board object</returns>
        public async Task<Board> ReOpenBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{boardId}", cancellationToken, new QueryParameter("closed", false));
        }

        /// <summary>
        /// Updates the properties of an existing board with the specified changes.
        /// </summary>
        /// <param name="boardWithChanges">A Board object containing the updated properties (must include the board's ID)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated Board object</returns>
        public async Task<Board> UpdateBoardAsync(Board boardWithChanges, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{boardWithChanges.Id}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(boardWithChanges));
        }

        /// <summary>
        /// Permanently deletes a board by its ID. This action cannot be undone. For a reversible option, use CloseBoardAsync instead.
        /// </summary>
        /// <remarks>
        /// As this is a major action, deletion is only allowed if TrelloClient.Options.AllowDeleteOfBoards is set to true as a secondary confirmation.
        /// </remarks>
        /// <param name="boardId">ID of the board to delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            if (Options.AllowDeleteOfBoards)
            {
                await _apiRequestController.Delete($"{UrlPaths.Boards}/{boardId}", cancellationToken, 0);
            }
            else
            {
                throw new SecurityException("Deletion of Boards are disabled via Options.AllowDeleteOfBoards (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back: https://support.atlassian.com/trello/docs/deleting-a-board/)");
            }
        }

        /// <summary>
        /// Retrieves a board by its ID.
        /// </summary>
        /// <param name="boardId">ID of the board</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Board object with the specified ID</returns>
        public async Task<Board> GetBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Board>(GetUrlBuilder.GetBoard(boardId), cancellationToken);
        }

        /// <summary>
        /// Retrieves plan information for a specific board, including its subscription level (Free, Standard, Premium, Enterprise) and supported features.
        /// </summary>
        /// <param name="boardId">ID of the board to get plan information for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A TrelloPlanInformation object describing the board's plan and features</returns>
        public async Task<TrelloPlanInformation> GetTrelloPlanInformationForBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<TrelloPlanInformation>(GetUrlBuilder.GetBoard(boardId, new GetBoardOptions
            {
                BoardFields = new BoardFields("name", "premiumFeatures")
            }), cancellationToken);
        }

        /// <summary>
        /// Retrieves a board by its ID, with options to include additional nested data such as cards, lists, or organization details.
        /// </summary>
        /// <param name="boardId">ID of the board.</param>
        /// <param name="options">Options specifying which fields and nested data to include</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Board object with the specified ID and requested details</returns>
        public async Task<Board> GetBoardAsync(string boardId, GetBoardOptions options, CancellationToken cancellationToken = default)
        {
            options.AdjustFieldsBasedOnSelectedOptions();
            var board = await _apiRequestController.Get<Board>(GetUrlBuilder.GetBoard(boardId), cancellationToken, options.GetParameters());
            if (options.IncludeCards != GetBoardOptionsIncludeCards.None)
            {
                board.Cards = FilterCards(board.Cards, options.CardsFilterConditions);
                board.Cards = OrderCards(board.Cards, options.CardsOrderBy);
            }

            return board;
        }

        /// <summary>
        /// Retrieves all boards that a specified member has access to.
        /// </summary>
        /// <param name="memberId">ID of the member to find boards for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Board objects the member can access</returns>
        public async Task<List<Board>> GetBoardsForMemberAsync(string memberId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Board>>(GetUrlBuilder.GetBoardsForMember(memberId), cancellationToken);
        }

        /// <summary>
        /// Retrieves all boards that a specified member has access to, with options to include additional nested data.
        /// </summary>
        /// <param name="memberId">ID of the member to find boards for</param>
        /// <param name="options">Options specifying which fields and nested data to include</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Board objects the member can access</returns>
        public async Task<List<Board>> GetBoardsForMemberAsync(string memberId, GetBoardOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Board>>(GetUrlBuilder.GetBoardsForMember(memberId), cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Retrieves all boards that the current Trello API token has access to.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Board objects accessible to the current token</returns>
        public async Task<List<Board>> GetBoardsCurrentTokenCanAccessAsync(CancellationToken cancellationToken = default)
        {
            var tokenMember = await GetTokenMemberAsync(cancellationToken);
            return await GetBoardsForMemberAsync(tokenMember.Id, cancellationToken);
        }

        /// <summary>
        /// Retrieves all boards that the current Trello API token has access to, with options to include additional nested data.
        /// </summary>
        /// <param name="options">Options specifying which fields and nested data to include</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Board objects accessible to the current token</returns>
        public async Task<List<Board>> GetBoardsCurrentTokenCanAccessAsync(GetBoardOptions options, CancellationToken cancellationToken = default)
        {
            var tokenMember = await GetTokenMemberAsync(cancellationToken);
            return await GetBoardsForMemberAsync(tokenMember.Id, options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all boards in a specified organization, given the organization's ID.
        /// </summary>
        /// <param name="organizationId">ID of the organization to get boards for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Board objects in the organization</returns>
        public async Task<List<Board>> GetBoardsInOrganizationAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Board>>(GetUrlBuilder.GetBoardsInOrganization(organizationId), cancellationToken);
        }

        /// <summary>
        /// Retrieves all boards in a specified organization, with options to include additional nested data.
        /// </summary>
        /// <param name="organizationId">ID of the organization to get boards for</param>
        /// <param name="options">Options specifying which fields and nested data to include</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Board objects in the organization</returns>
        public async Task<List<Board>> GetBoardsInOrganizationAsync(string organizationId, GetBoardOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Board>>(GetUrlBuilder.GetBoardsInOrganization(organizationId), cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Updates the preferences of a board, such as visibility, voting, comments, and other settings. Only the specified options are changed; others remain unchanged.
        /// </summary>
        /// <param name="boardId">ID of the board to update preferences for</param>
        /// <param name="options">The preference options to set (leave properties null to keep existing values)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateBoardPreferencesAsync(string boardId, UpdateBoardPreferencesOptions options, CancellationToken cancellationToken = default)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            SetOption(options.CardCovers, "prefs/cardCovers");
            SetOption(options.Visibility, "prefs/permissionLevel");
            SetOption(options.ShowCompletedStatusOnCardFront, "prefs/showCompleteStatus");
            SetOption(options.HideVotes, "prefs/hideVotes");
            SetOption(options.WhoCanVote, "prefs/voting");
            SetOption(options.WhoCanComment, "prefs/comments");
            SetOption(options.WhoCanAddAndRemoveMembers, "prefs/invitations");
            SetOption(options.SelfJoin, "prefs/selfJoin");
            SetOption(options.CardAging, "prefs/cardAging");

            if (parameters.Count > 0)
            {
                await _apiRequestController.Put($"{UrlPaths.Boards}/{boardId}", cancellationToken, 0, parameters.ToArray());
            }

            void SetOption(Enum @enum, string parameterKey)
            {
                if (@enum != null)
                {
                    parameters.Add(new QueryParameter(parameterKey, @enum.GetJsonPropertyName()));
                }
            }
        }
    }
}