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

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Get up to 10 lists as a batch
        /// </summary>
        /// <param name="ids">List Ids</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of Lists</returns>
        public async Task<List<List>> GetListsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await GetBatchedDataAsync<List>(ids.Select(id => $"/{GetUrlBuilder.GetList(id)}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Get up to 10 cards as a batch
        /// </summary>
        /// <param name="ids">Card Ids</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of Cards</returns>
        public async Task<List<Card>> GetCardsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await GetBatchedDataAsync<Card>(ids.Select(id => $"/{GetUrlBuilder.GetCard(id)}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Get up to 10 cards as a batch
        /// </summary>
        /// <param name="ids">Card Ids</param>
        /// <param name="options">Options</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of Cards</returns>
        public async Task<List<Card>> GetCardsAsync(List<string> ids, GetCardOptions options, CancellationToken cancellationToken = default)
        {
            StringBuilder parametersAsString = ApiRequestController.GetParametersAsString(options.GetParameters()).Replace("&", "?", 0, 1);
            return await GetBatchedDataAsync<Card>(ids.Select(id => $"/{UrlPaths.Cards}/{id}{parametersAsString}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Get up to 10 boards as a batch
        /// </summary>
        /// <param name="ids">Board Ids</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of Boards</returns>
        public async Task<List<Board>> GetBoardsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await GetBatchedDataAsync<Board>(ids.Select(id => $"/{UrlPaths.Boards}/{id}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Get up to 10 boards as a batch
        /// </summary>
        /// <param name="ids">Board Ids</param>
        /// <param name="options">Options</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of Boards</returns>
        public async Task<List<Board>> GetBoardsAsync(List<string> ids, GetBoardOptions options, CancellationToken cancellationToken = default)
        {
            StringBuilder parametersAsString = ApiRequestController.GetParametersAsString(options.GetParameters()).Replace("&", "?", 0, 1);
            return await GetBatchedDataAsync<Board>(ids.Select(id => $"/{UrlPaths.Boards}/{id}{parametersAsString}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Get up to 10 members as a batch
        /// </summary>
        /// <param name="ids">Member Ids</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of Members</returns>
        public async Task<List<Member>> GetMembersAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await GetBatchedDataAsync<Member>(ids.Select(id => $"/{UrlPaths.Members}/{id}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Get up to 10 organizations as a batch
        /// </summary>
        /// <param name="ids">Organization Ids</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of Organizations</returns>
        public async Task<List<Organization>> GetOrganizationsAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            return await GetBatchedDataAsync<Organization>(ids.Select(id => $"/{UrlPaths.Organizations}/{id}").ToList(), cancellationToken);
        }

        /// <summary>
        /// Get batched data of the same type (T) as a list
        /// </summary>
        /// <typeparam name="T">The type to return (example Card or Board)</typeparam>
        /// <param name="urls">The REST API Suffix Urls (example: /boards/{boardId}/lists)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of T</returns>
        /// <exception cref="TrelloApiException"></exception>
        public async Task<List<T>> GetBatchedDataAsync<T>(List<string> urls, CancellationToken cancellationToken = default)
        {
            var batchResult = await GetBatchedDataAsync(urls, cancellationToken);
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
                throw new TrelloApiException($"{batchResult.ErrorCount} of {batchResult.Results.Count} batched results failed: {string.Join(" | ",errors)}", string.Empty);
            }
            return batchResult.Results.Select(x => x.StatusCode==200 ? x.GetData<T>() : default).ToList();
        }

        /// <summary>
        /// Make a generic Batch request with up to 10 URLs of same or different type of GET data
        /// </summary>
        /// <param name="urls">The REST API Suffix Urls (example: /boards/{boardId}/lists)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>a Batch-result</returns>
        /// <exception cref="ArgumentException">If you ask for more than 10 URLs</exception>
        public async Task<BatchResult> GetBatchedDataAsync(List<string> urls, CancellationToken cancellationToken = default)
        {
            if (urls.Count > 10)
            {
                throw new ArgumentException("Batch features in the API only allow up to 10 batched URLs", nameof(urls));
            }
            var preparedUrls = urls.Select(url => url.StartsWith("/") ? url : $"/{url}").ToList();
            string json = await _apiRequestController.Get("batch", cancellationToken, 0, new QueryParameter("urls", string.Join(",", preparedUrls)));
            var batchResultForUrls = JsonSerializer.Deserialize<List<BatchResultForUrl>>(json);

            //Attach URL to each result
            for (int i = 0; i < preparedUrls.Count; i++)
            {
                batchResultForUrls[i].Url = preparedUrls[i];
            }

            return new BatchResult(batchResultForUrls);
        }
    }
}