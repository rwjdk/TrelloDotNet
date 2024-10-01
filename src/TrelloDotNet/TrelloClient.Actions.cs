using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
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
        /// <param name="page">The page of results for actions</param>
        /// <param name="before">An Action ID</param>
        /// <param name="since">An Action ID</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsOfBoardAsync(string boardId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default, int page = 0, string before = null, string since = null)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsOnBoard(boardId), filter, limit, cancellationToken, page, before, since);
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
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsOnCard(cardId), filter, limit, cancellationToken);
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
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForList(listId), filter, limit, cancellationToken);
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
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForMember(memberId), filter, limit, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for an Organization
        /// </summary>
        /// <remarks>
        /// Only organization-specific actions will be returned. For the actions on the boards, see GetActionsOfBoardAsync
        /// </remarks>
        /// <param name="organizationId">The Id of the Organization</param>
        /// <param name="filter">A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes)</param>
        /// <param name="limit">How many recent events to get back; Default 50, Max 1000</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsForOrganizationsAsync(string organizationId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForOrganization(organizationId), filter, limit, cancellationToken);
        }

        private async Task<List<TrelloAction>> GetActionsFromSuffix(string suffix, List<string> filter, int limit, CancellationToken cancellationToken = default, int page = 0, string before = null, string since = null)
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
            
            if (page > 0)
            {
                parameters.Add(new QueryParameter("page", page));
            }
            
            if (before != null)
            {
                parameters.Add(new QueryParameter("before", before));
            }
            
            if (since != null)
            {
                parameters.Add(new QueryParameter("since", since));
            }

            return await _apiRequestController.Get<List<TrelloAction>>(suffix, cancellationToken, parameters.ToArray());
        }
    }
}