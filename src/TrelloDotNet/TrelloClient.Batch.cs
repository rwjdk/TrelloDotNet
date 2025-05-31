using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Batch;
using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.GetListOptions;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Retrieves multiple Trello lists in a single batch request, given a collection of list IDs.
        /// </summary>
        /// <param name="ids">A list of List IDs to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of List objects corresponding to the provided IDs</returns>
        public async Task<List<List>> GetListsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await ExecuteBatchedRequestAsync<List>(ids.Select(id => $"/{GetUrlBuilder.GetList(id)}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Retrieves multiple Trello lists in a single batch request, with additional options for customizing the returned data.
        /// </summary>
        /// <param name="ids">A list of List IDs to retrieve</param>
        /// <param name="options">Options for customizing which fields and nested data are included for each list</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of List objects corresponding to the provided IDs</returns>
        public async Task<List<List>> GetListsAsync(List<string> ids, GetListOptions options, CancellationToken cancellationToken = default)
        {
            return await ExecuteBatchedRequestAsync<List>(ids.Select(id => $"/{GetUrlBuilder.GetList(id, options)}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Retrieves multiple Trello cards in a single batch request, given a collection of card IDs.
        /// </summary>
        /// <param name="ids">A list of Card IDs to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Card objects corresponding to the provided IDs</returns>
        public async Task<List<Card>> GetCardsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await ExecuteBatchedRequestAsync<Card>(ids.Select(id => $"/{GetUrlBuilder.GetCard(id)}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Retrieves multiple Trello cards in a single batch request, with additional options for customizing the returned data.
        /// </summary>
        /// <param name="ids">A list of Card IDs to retrieve</param>
        /// <param name="options">Options for customizing which fields and nested data are included for each card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Card objects corresponding to the provided IDs</returns>
        public async Task<List<Card>> GetCardsAsync(List<string> ids, GetCardOptions options, CancellationToken cancellationToken = default)
        {
            options.AdjustFieldsBasedOnSelectedOptions();
            StringBuilder parametersAsString = ApiRequestController.GetParametersAsString(options.GetParameters(false)).Replace("&", "?", 0, 1);
            List<Card> cards = await ExecuteBatchedRequestAsync<Card>(ids.Select(id => $"/{UrlPaths.Cards}/{id}{parametersAsString}").ToList(), cancellationToken);
            cards = FilterCards(cards, options.FilterConditions);
            return OrderCards(cards, options.OrderBy);
        }

        /// <summary>
        /// Retrieves multiple Trello boards in a single batch request, given a collection of board IDs.
        /// </summary>
        /// <param name="ids">A list of Board IDs to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Board objects corresponding to the provided IDs</returns>
        public async Task<List<Board>> GetBoardsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await ExecuteBatchedRequestAsync<Board>(ids.Select(id => $"/{UrlPaths.Boards}/{id}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Retrieves multiple Trello boards in a single batch request, with additional options for customizing the returned data.
        /// </summary>
        /// <param name="ids">A list of Board IDs to retrieve</param>
        /// <param name="options">Options for customizing which fields and nested data are included for each board</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Board objects corresponding to the provided IDs</returns>
        public async Task<List<Board>> GetBoardsAsync(List<string> ids, GetBoardOptions options, CancellationToken cancellationToken = default)
        {
            StringBuilder parametersAsString = ApiRequestController.GetParametersAsString(options.GetParameters()).Replace("&", "?", 0, 1);
            return await ExecuteBatchedRequestAsync<Board>(ids.Select(id => $"/{UrlPaths.Boards}/{id}{parametersAsString}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Retrieves multiple Trello members in a single batch request, given a collection of member IDs.
        /// </summary>
        /// <param name="ids">A list of Member IDs to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Member objects corresponding to the provided IDs</returns>
        public async Task<List<Member>> GetMembersAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await ExecuteBatchedRequestAsync<Member>(ids.Select(id => $"/{UrlPaths.Members}/{id}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Retrieves multiple Trello organizations in a single batch request, given a collection of organization IDs.
        /// </summary>
        /// <param name="ids">A list of Organization IDs to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of Organization objects corresponding to the provided IDs</returns>
        public async Task<List<Organization>> GetOrganizationsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await ExecuteBatchedRequestAsync<Organization>(ids.Select(id => $"/{UrlPaths.Organizations}/{id}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Executes a batch request for a set of Trello API URLs and returns the results as a list of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to return for each result (e.g., Card, Board)</typeparam>
        /// <param name="urls">A list of REST API URL suffixes (e.g., /boards/{boardId}/lists) to request in the batch</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of objects of type T, one for each requested URL</returns>
        /// <exception cref="TrelloApiException">Thrown if any of the batch requests fail</exception>
        private async Task<List<T>> ExecuteBatchedRequestAsync<T>(List<string> urls, CancellationToken cancellationToken = default)
        {
            var batchResult = await GetBatchedRequestDataAsync(urls, cancellationToken);
            if (batchResult.ErrorCount == 0)
            {
                return batchResult.Results.Select(x => x.StatusCode == 200 ? x.GetData<T>() : default).ToList();
            }

            //Error scenario
            var errors = new List<string>();
            foreach (var result in batchResult.Results)
            {
                if (result.StatusCode != 200)
                {
                    errors.Add($"'{result.Url}' resulted in error: {result.StatusCode}{result.Name} ({result.Message})");
                }
            }

            if (batchResult.ErrorCount > 0)
            {
                throw new TrelloApiException($"{batchResult.ErrorCount} of {batchResult.Results.Count} batched results failed: {string.Join(" | ", errors)}", string.Empty);
            }

            return batchResult.Results.Select(x => x.StatusCode == 200 ? x.GetData<T>() : default).ToList();
        }

        /// <summary>
        /// Executes a generic batch GET request for a set of Trello API URLs and returns a summary of the results.
        /// </summary>
        /// <param name="urls">A list of REST API URL suffixes (e.g., /boards/{boardId}/lists) to request in the batch</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A BatchResultSummary containing the results and any errors</returns>
        private async Task<BatchResultSummary> GetBatchedRequestDataAsync(List<string> urls, CancellationToken cancellationToken = default)
        {
            if (urls == null)
            {
                throw new ArgumentNullException(nameof(urls));
            }

            List<BatchResultForUrl> batchResultForUrls = new List<BatchResultForUrl>();
            while (batchResultForUrls.Count < urls.Count)
            {
                var urlsToProcessThisBatch = urls.Skip(batchResultForUrls.Count).Take(10).ToList();
                var preparedUrls = urlsToProcessThisBatch.Select(url => url.StartsWith("/") ? url : $"/{url}").ToList();
                string json = await _apiRequestController.Get("batch", cancellationToken, 0, new QueryParameter("urls", string.Join(",", preparedUrls)));
                var results = JsonSerializer.Deserialize<List<BatchResultForUrl>>(json);
                //Attach URL to each result
                for (int i = 0; i < results.Count; i++)
                {
                    results[i].Url = urls[i + batchResultForUrls.Count];
                }

                batchResultForUrls.AddRange(results);
            }

            return new BatchResultSummary(batchResultForUrls);
        }

        /// <summary>
        /// Executes a set of batch GET requests, each with a URL and an associated action to process the result data.
        /// </summary>
        /// <param name="requests">A list of BatchRequest objects, each containing a URL and an action to handle the result</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task ExecuteBatchedRequestAsync(List<BatchRequest> requests, CancellationToken cancellationToken = default)
        {
            if (requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }

            List<BatchResultForUrl> batchResultForUrls = new List<BatchResultForUrl>();
            while (batchResultForUrls.Count < requests.Count)
            {
                var urlsToProcessThisBatch = requests.Skip(batchResultForUrls.Count).Take(10).ToList();
                var preparedUrls = urlsToProcessThisBatch.Select(x => x.Url.StartsWith("/") ? x.Url : $"/{x.Url}").ToList();
                string json = await _apiRequestController.Get("batch", cancellationToken, 0, new QueryParameter("urls", string.Join(",", preparedUrls)));
                List<BatchResultForUrl> results = JsonSerializer.Deserialize<List<BatchResultForUrl>>(json);

                //Attach URL to each result
                for (int i = 0; i < results.Count; i++)
                {
                    results[i].Url = requests[i + batchResultForUrls.Count].Url;
                }

                var batchResult = new BatchResultSummary(results);
                if (batchResult.ErrorCount > 0)
                {
                    throw new Exception($"Error getting Trello data: {string.Join(" | ", batchResult.Errors)}");
                }

                for (int i = 0; i < results.Count; i++)
                {
                    requests[i + batchResultForUrls.Count].Action.Invoke(results[i]);
                }

                batchResultForUrls.AddRange(results);
            }
        }
    }
}