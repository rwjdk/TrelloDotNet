using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Sends a custom POST request to an unexposed Trello API endpoint and returns the response as a strongly-typed object. Use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON properties.
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The deserialized object from the response</returns>
        public async Task<T> PostAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await PostAsync<T>(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Sends a custom POST request to an unexposed Trello API endpoint and returns the response as a strongly-typed object. Use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON properties.
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The deserialized object from the response</returns>
        public async Task<T> PostAsync<T>(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Post<T>(urlSuffix, cancellationToken, parameters);
        }

        /// <summary>
        /// Sends a custom POST request to an unexposed Trello API endpoint and returns the response as a JSON string.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The JSON response as a string</returns>
        public async Task<string> PostAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await PostAsync(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Sends a custom POST request to an unexposed Trello API endpoint and returns the response as a JSON string.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The JSON response as a string</returns>
        public async Task<string> PostAsync(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Post(urlSuffix, cancellationToken, 0, parameters);
        }

        /// <summary>
        /// Sends a custom PUT request to an unexposed Trello API endpoint and returns the response as a strongly-typed object. Use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON properties.
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The deserialized object from the response</returns>
        public async Task<T> PutAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await PutAsync<T>(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Sends a custom PUT request to an unexposed Trello API endpoint and returns the response as a strongly-typed object. Use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON properties.
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The deserialized object from the response</returns>
        public async Task<T> PutAsync<T>(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Put<T>(urlSuffix, cancellationToken, parameters);
        }

        /// <summary>
        /// Sends a custom PUT request to an unexposed Trello API endpoint and returns the response as a JSON string.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The JSON response as a string</returns>
        public async Task<string> PutAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await PutAsync(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Sends a custom PUT request to an unexposed Trello API endpoint and returns the response as a JSON string.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The JSON response as a string</returns>
        public async Task<string> PutAsync(string urlSuffix, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Put(urlSuffix, cancellationToken, 0, parameters);
        }

        /// <summary>
        /// Sends a custom PUT request with a JSON payload to an unexposed Trello API endpoint and returns the response as a JSON string.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="payload">The JSON payload to send in the request body</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The JSON response as a string</returns>
        public async Task<string> PutAsync(string urlSuffix, string payload, params QueryParameter[] parameters)
        {
            return await PutAsync(urlSuffix, payload, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Sends a custom PUT request with a JSON payload to an unexposed Trello API endpoint and returns the response as a JSON string.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="payload">The JSON payload to send in the request body</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The JSON response as a string</returns>
        public async Task<string> PutAsync(string urlSuffix, string payload, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.PutWithJsonPayload(urlSuffix, cancellationToken, payload, 0, parameters);
        }

        /// <summary>
        /// Sends a custom GET request to an unexposed Trello API endpoint and returns the response as a strongly-typed object. Use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON properties.
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The deserialized object from the response</returns>
        public async Task<T> GetAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await GetAsync<T>(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Sends a custom GET request to an unexposed Trello API endpoint and returns the response as a strongly-typed object. Use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON properties.
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The deserialized object from the response</returns>
        public async Task<T> GetAsync<T>(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Get<T>(urlSuffix, cancellationToken, parameters);
        }

        /// <summary>
        /// Sends a custom GET request to an unexposed Trello API endpoint and returns the response as a JSON string.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The JSON response as a string</returns>
        public async Task<string> GetAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await GetAsync(urlSuffix, CancellationToken.None, parameters);
        }

        /// <summary>
        /// Sends a custom GET request to an unexposed Trello API endpoint and returns the response as a JSON string.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="parameters">Additional query parameters for the request</param>
        /// <returns>The JSON response as a string</returns>
        public async Task<string> GetAsync(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Get(urlSuffix, cancellationToken, 0, parameters);
        }

        /// <summary>
        /// Sends a custom DELETE request to an unexposed Trello API endpoint.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (the part after https://api.trello.com/1/ and before any URI parameters)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteAsync(string urlSuffix, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete(urlSuffix, cancellationToken, 0);
        }
    }
}