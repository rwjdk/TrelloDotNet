using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Actions;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Get the most recent Actions (Changelog Events) of a board
        /// </summary>
        /// <param name="boardId">The Id of the Board</param>
        /// <param name="filter">A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes)</param>
        /// <param name="limit">How many recent events to get back; Default 50, Max 1000</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsOfBoardAsync(string boardId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Actions}", filter, limit, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) on a Card
        /// </summary>
        /// <remarks>
        /// If no filter is given the default filter of Trello API is 'commentCard, updateCard:idList' (aka 'Move Card To List' and 'Add Comment')
        /// </remarks>
        /// <param name="cardId">The Id of the Card</param>
        /// <param name="filter">A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes). Default: 'commentCard, updateCard:idList' (aka 'Move Card To List' and 'Add Comment')</param>
        /// <param name="limit">How many recent events to get back; Default 50, Max 1000</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsOnCardAsync(string cardId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Actions}", filter, limit, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for a List
        /// </summary>
        /// <param name="listId">The Id of the List</param>
        /// <param name="filter">A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes)</param>
        /// <param name="limit">How many recent events to get back; Default 50, Max 1000</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsForListAsync(string listId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix($"{UrlPaths.Lists}/{listId}/{UrlPaths.Actions}", filter, limit, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for a Member
        /// </summary>
        /// <param name="memberId">The Id of the Member</param>
        /// <param name="filter">A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes)</param>
        /// <param name="limit">How many recent events to get back; Default 50, Max 1000</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsForMemberAsync(string memberId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix($"{UrlPaths.Members}/{memberId}/{UrlPaths.Actions}", filter, limit, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for an Organization
        /// </summary>
        /// <param name="organizationId">The Id of the Organization</param>
        /// <param name="filter">A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes)</param>
        /// <param name="limit">How many recent events to get back; Default 50, Max 1000</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsForOrganizationsAsync(string organizationId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix($"{UrlPaths.Organizations}/{organizationId}/{UrlPaths.Actions}", filter, limit, cancellationToken);
        }

        private async Task<List<TrelloAction>> GetActionsFromSuffix(string suffix, List<string> filter, int limit, CancellationToken cancellationToken = default)
        {
            var parameters = new List<QueryParameter>();
            if (filter != null)
            {
                parameters.Add(new QueryParameter("filter", string.Join(",", filter)));
            }

            if (limit > 0)
            {
                parameters.Add(new QueryParameter("limit", limit));
            }

            return await _apiRequestController.Get<List<TrelloAction>>(suffix, cancellationToken, parameters.ToArray());
        }

    }
}