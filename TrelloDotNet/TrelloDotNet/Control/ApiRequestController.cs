using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    internal class ApiRequestController
    {
        private const string BaseUrl = "https://api.trello.com/1/";
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _token;

        internal ApiRequestController(HttpClient httpClient, string apiKey, string token)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiKey = apiKey;
            _token = token;
        }

        internal async Task<T> Get<T>(string suffix, params UriParameter[] parameters)
        {
            string json = await Get(suffix, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }
        
        internal async Task<string> Get(string suffix, params UriParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, uri.AbsoluteUri); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }
        
        internal async Task<T> Post<T>(string suffix, params UriParameter[] parameters)
        {
            string json = await Post(suffix, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        internal async Task<string> Post(string suffix, params UriParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.PostAsync(uri, null);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, uri.AbsoluteUri); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }
        
        public async Task<T> Put<T>(string suffix, params UriParameter[] parameters)
        {
            string json = await Put(suffix, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        public async Task<string> Put(string suffix, params UriParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.PutAsync(uri, null);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, uri.AbsoluteUri); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }

        private static StringBuilder GetParametersAsString(UriParameter[] parameters)
        {
            StringBuilder parameterString = new StringBuilder();
            foreach (var parameter in parameters)
            {
                parameterString.Append($"&{parameter.Name}={parameter.GetValueAsApiFormattedString()}");
            }

            return parameterString;
        }

        private Uri BuildUri(string suffix, UriParameter[] parameters)
        {
            return new Uri($@"{BaseUrl}{suffix}?key={_apiKey}&token={_token}" + GetParametersAsString(parameters));
        }
    }
}