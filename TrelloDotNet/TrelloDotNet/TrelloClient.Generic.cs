using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> PostAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await PostAsync<T>(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> PostAsync<T>(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Post<T>(urlSuffix, cancellationToken, parameters);
        }

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PostAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await PostAsync(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PostAsync(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Post(urlSuffix, cancellationToken, 0, parameters);
        }

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> PutAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await PutAsync<T>(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> PutAsync<T>(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Put<T>(urlSuffix, cancellationToken, parameters);
        }

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PutAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await PutAsync(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PutAsync(string urlSuffix, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Put(urlSuffix, cancellationToken, 0, parameters);
        }

        /// <summary>
        /// Custom Put Method (with JSON Payload) to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="payload">JSON Payload (In the rare cases Trello API need this)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PutAsync(string urlSuffix, string payload, params QueryParameter[] parameters)
        {
            return await PutAsync(urlSuffix, payload, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Custom Put Method (with JSON Payload) to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="payload">JSON Payload (In the rare cases Trello API need this)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PutAsync(string urlSuffix, string payload, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.PutWithJsonPayload(urlSuffix, cancellationToken, payload, 0, parameters);
        }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> GetAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await GetAsync<T>(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> GetAsync<T>(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Get<T>(urlSuffix, cancellationToken, parameters);
        }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> GetAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await GetAsync(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> GetAsync(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Get(urlSuffix, cancellationToken, 0, parameters);
        }

        /// <summary>
        /// Custom Delete Method to be used on unexposed features of the API.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteAsync(string urlSuffix, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete(urlSuffix, cancellationToken, 0);
        }
    }
}