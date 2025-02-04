using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Extensions;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    /// <summary>
    /// The Main Client to communicate with the Trello API (aka everything is done via this)
    /// </summary>
    public partial class TrelloClient
    {
        /// <summary>
        /// Options for the client
        /// </summary>
        public TrelloClientOptions Options { get; }

        private readonly ApiRequestController _apiRequestController;
        private readonly QueryParametersBuilder _queryParametersBuilder;
        private readonly HttpClient _staticHttpClient = new HttpClient();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">The Trello API Key you get on https://trello.com/power-ups/admin/</param>
        /// <param name="token">Your Authorization Token you generate get on https://trello.com/power-ups/admin/</param>
        /// <param name="options">Various options for the client (if null default options will be used)</param>
        /// <param name="httpClient">Optional HTTP Client if you wish to specify it on your own (else an internal static HttpClient will be used for re-use)</param>
        public TrelloClient(string apiKey, string token, TrelloClientOptions options = null, HttpClient httpClient = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("You need to specify an API Key. Get it on page: https://trello.com/power-ups/admin/");
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("You need to specify a Token. Generate it on page: https://trello.com/power-ups/admin/");
            }

            if (httpClient != null)
            {
                _staticHttpClient = httpClient;
            }

            Options = options ?? new TrelloClientOptions();
            _apiRequestController = new ApiRequestController(_staticHttpClient, apiKey, token, this);
            _queryParametersBuilder = new QueryParametersBuilder();
        }

        /// <summary>
        /// Get information about the token used by this TrelloClient
        /// </summary>
        /// <returns>Information about the Token</returns>
        public async Task<TokenInformation> GetTokenInformationAsync(CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<TokenInformation>($"{UrlPaths.Tokens}/{_apiRequestController.Token}", cancellationToken);
        }

        /// <summary>
        /// Get Ids of the owner of the Token's Inbox
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>The Inbox Info</returns>
        public async Task<TokenMemberInbox> GetTokenMemberInboxAsync(CancellationToken cancellationToken = default)
        {
            return (await _apiRequestController.Get<TokenMemberInformationInbox>($"{UrlPaths.Members}/me?fields=inbox", cancellationToken))?.Inbox;
        }

        private static List<Card> OrderCards(List<Card> cards, CardsOrderBy? orderBy)
        {
            switch (orderBy)
            {
                case CardsOrderBy.CreateDateAsc:
                    return cards.OrderBy(x => x.Created).ToList();
                case CardsOrderBy.CreateDateDesc:
                    return cards.OrderByDescending(x => x.Created).ToList();
                case CardsOrderBy.StartDateAsc:
                    return cards.OrderBy(x => x.Start).ToList();
                case CardsOrderBy.StartDateDesc:
                    return cards.OrderByDescending(x => x.Start).ToList();
                case CardsOrderBy.DueDateAsc:
                    return cards.OrderBy(x => x.Due).ToList();
                case CardsOrderBy.DueDateDesc:
                    return cards.OrderByDescending(x => x.Due).ToList();
                case CardsOrderBy.NameAsc:
                    return cards.OrderBy(x => x.Name).ToList();
                case CardsOrderBy.NameDesc:
                    return cards.OrderByDescending(x => x.Name).ToList();
            }

            return cards;
        }

        private async Task<List<Card>> FilterCards(string boardId, List<Card> cards, List<CardsFilterCondition> filterConditions)
        {
            if (filterConditions == null || filterConditions.Count == 0)
            {
                return cards;
            }

            var customFields = new List<CustomField>();
            if (filterConditions.Any(x => x.Field == CardsConditionField.CustomField))
            {
                customFields = await GetCustomFieldsOnBoardAsync(boardId);
            }

            return cards.Filter(filterConditions, customFields);
        }
    }
}