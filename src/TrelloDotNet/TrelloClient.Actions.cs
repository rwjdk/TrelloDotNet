using System;
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
        /// Get the most recent Actions (Changelog Events) of a board
        /// </summary>
        /// <param name="boardId">The Id of the Board</param>
        /// <param name="options">Options on how and what is retrieved</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsOfBoardAsync(string boardId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsOnBoard(boardId), options, cancellationToken);
        }

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
        [Obsolete("Use overload the take in the 'GetActionsOptions' instead [Will be removed in v2.0 of this nuGet Package]")]
        public async Task<List<TrelloAction>> GetActionsOfBoardAsync(string boardId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default, int page = 0, string before = null, string since = null)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsOnBoard(boardId), new GetActionsOptions
            {
                Limit = limit,
                Before = before,
                Filter = filter,
                Page = page,
                Since = since
            }, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) on a Card
        /// </summary>
        /// <param name="cardId">The Id of the Card</param>
        /// <param name="options">Options on how and what is retrieved</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsOnCardAsync(string cardId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsOnCard(cardId), options, cancellationToken);
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
        [Obsolete("Use overload the take in the 'GetActionsOptions' instead [Will be removed in v2.0 of this nuGet Package]")]
        public async Task<List<TrelloAction>> GetActionsOnCardAsync(string cardId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsOnCard(cardId), new GetActionsOptions
            {
                Limit = limit,
                Filter = filter,
            }, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for a List
        /// </summary>
        /// <param name="listId">The Id of the List</param>
        /// <param name="options">Options on how and what is retrieved</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsForListAsync(string listId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForList(listId), options, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for a List
        /// </summary>
        /// <param name="listId">The Id of the List</param>
        /// <param name="filter">A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes)</param>
        /// <param name="limit">How many recent events to get back; Default 50, Max 1000</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        [Obsolete("Use overload the take in the 'GetActionsOptions' instead [Will be removed in v2.0 of this nuGet Package]")]
        public async Task<List<TrelloAction>> GetActionsForListAsync(string listId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForList(listId), new GetActionsOptions
            {
                Limit = limit,
                Filter = filter,
            }, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for a Member
        /// </summary>
        /// <param name="memberId">The Id of the Member</param>
        /// <param name="options">Options on how and what is retrieved</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsForMemberAsync(string memberId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForMember(memberId), options, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for a Member
        /// </summary>
        /// <param name="memberId">The Id of the Member</param>
        /// <param name="filter">A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes)</param>
        /// <param name="limit">How many recent events to get back; Default 50, Max 1000</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        [Obsolete("Use overload the take in the 'GetActionsOptions' instead [Will be removed in v2.0 of this nuGet Package]")]
        public async Task<List<TrelloAction>> GetActionsForMemberAsync(string memberId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForMember(memberId), new GetActionsOptions
            {
                Limit = limit,
                Filter = filter,
            }, cancellationToken);
        }

        /// <summary>
        /// Get the most recent Actions (Changelog Events) for an Organization
        /// </summary>
        /// <remarks>
        /// Only organization-specific actions will be returned. For the actions on the boards, see GetActionsOfBoardAsync
        /// </remarks>
        /// <param name="organizationId">The Id of the Organization</param>
        /// <param name="options">Options on how and what is retrieved</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of most Recent Trello Actions</returns>
        public async Task<List<TrelloAction>> GetActionsForOrganizationsAsync(string organizationId, GetActionsOptions options, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForOrganization(organizationId), options, cancellationToken);
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
        [Obsolete("Use overload the take in the 'GetActionsOptions' instead [Will be removed in v2.0 of this nuGet Package]")]
        public async Task<List<TrelloAction>> GetActionsForOrganizationsAsync(string organizationId, List<string> filter = null, int limit = 50, CancellationToken cancellationToken = default)
        {
            return await GetActionsFromSuffix(GetUrlBuilder.GetActionsForOrganization(organizationId), new GetActionsOptions
            {
                Limit = limit,
                Filter = filter,
            }, cancellationToken);
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