using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Actions;
using TrelloDotNet.Model.Options.GetActionsOptions;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Retrieves the most recent actions (Activities) performed on the specified board, such as card movements, updates, and comments. Allows filtering and pagination through options.
        /// </summary>
        /// <param name="boardId">The ID of the board to retrieve actions for</param>
        /// <param name="options">Options to control filtering, limits, and additional parameters for the actions to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list containing the most recent Trello actions for the specified board</returns>
        public async Task<List<TrelloAction>> GetActionsOfBoardAsync(string boardId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsOnBoard(boardId), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves the most recent actions (Activities) performed on the specified card, such as comments, updates, and movements. Supports filtering and pagination.
        /// </summary>
        /// <param name="cardId">The ID of the card to retrieve actions for</param>
        /// <param name="options">Options to control filtering, limits, and additional parameters for the actions to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list containing the most recent Trello actions for the specified card</returns>
        public async Task<List<TrelloAction>> GetActionsOnCardAsync(string cardId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsOnCard(cardId), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves the most recent actions (Activities) performed on the specified list, such as card additions, removals, and updates. Allows filtering and pagination.
        /// </summary>
        /// <param name="listId">The ID of the list to retrieve actions for</param>
        /// <param name="options">Options to control filtering, limits, and additional parameters for the actions to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list containing the most recent Trello actions for the specified list</returns>
        public async Task<List<TrelloAction>> GetActionsForListAsync(string listId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForList(listId), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves the most recent actions (Activities) performed by the specified member, such as card assignments, comments, and updates. Supports filtering and pagination.
        /// </summary>
        /// <param name="memberId">The ID of the member to retrieve actions for</param>
        /// <param name="options">Options to control filtering, limits, and additional parameters for the actions to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list containing the most recent Trello actions performed by the specified member</returns>
        public async Task<List<TrelloAction>> GetActionsForMemberAsync(string memberId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForMember(memberId), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves the most recent actions (Activities) related to the specified organization, such as membership changes and organization updates. Only organization-specific actions are returned; board actions require GetActionsOfBoardAsync.
        /// </summary>
        /// <remarks>
        /// Only organization-specific actions will be returned. For actions on boards, use GetActionsOfBoardAsync.
        /// </remarks>
        /// <param name="organizationId">The ID of the organization to retrieve actions for</param>
        /// <param name="options">Options to control filtering, limits, and additional parameters for the actions to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list containing the most recent Trello actions for the specified organization</returns>
        public async Task<List<TrelloAction>> GetActionsForOrganizationsAsync(string organizationId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForOrganization(organizationId), options, cancellationToken);
        }

        private async Task<List<TrelloAction>> GetActionsFromSuffix(string suffix, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            var parameters = new List<QueryParameter>();
            if (options.Filter != null)
            {
                parameters.Add(new QueryParameter("filter", string.Join(",", options.Filter)));
            }

            if (options.Limit > 0)
            {
                parameters.Add(new QueryParameter("limit", options.Limit));
            }

            if (options.Page > 0)
            {
                parameters.Add(new QueryParameter("page", options.Page));
            }

            if (options.Before != null)
            {
                parameters.Add(new QueryParameter("before", options.Before));
            }

            if (options.Since != null)
            {
                parameters.Add(new QueryParameter("since", options.Since));
            }

            parameters.AddRange(options.AdditionalParameters);

            return await _apiRequestController.Get<List<TrelloAction>>(suffix, cancellationToken, parameters.ToArray());
        }
    }
}