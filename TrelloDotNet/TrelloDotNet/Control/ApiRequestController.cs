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
        private readonly TrelloClient _client;

        internal ApiRequestController(HttpClient httpClient, string apiKey, string token, TrelloClient client)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiKey = apiKey;
            _token = token;
            _client = client;
        }

        public string Token => _token;

        internal async Task<T> Get<T>(string suffix, params QueryParameter[] parameters)
        {
            string json = await Get(suffix, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }
        
        internal async Task<string> Get(string suffix, params QueryParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }
        
        internal async Task<T> Post<T>(string suffix, params QueryParameter[] parameters)
        {
            string json = await Post(suffix, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        internal async Task<string> Post(string suffix, params QueryParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.PostAsync(uri, null);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }
        
        public async Task<T> Put<T>(string suffix, params QueryParameter[] parameters)
        {
            string json = await Put(suffix, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        public async Task<string> Put(string suffix, params QueryParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.PutAsync(uri, null);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }

        private string FormatExceptionUrlAccordingToClientOptions(string fullUrl)
        {
            switch (_client.Options.ApiCallExceptionOption)
            {
                case ApiCallExceptionOption.IncludeUrlAndCredentials:
                    return fullUrl;
                case ApiCallExceptionOption.IncludeUrlButMaskCredentials:
                    return fullUrl.Replace($"?key={_apiKey}&token={_token}", $"?key=XXXXX&token=XXXXXXXXXX");
                case ApiCallExceptionOption.DoNotIncludeTheUrl:
                    return string.Empty.PadLeft(5,'X');
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static StringBuilder GetParametersAsString(QueryParameter[] parameters)
        {
            StringBuilder parameterString = new StringBuilder();
            foreach (var parameter in parameters)
            {
                parameterString.Append($"&{parameter.Name}={parameter.GetValueAsApiFormattedString()}");
            }

            return parameterString;
        }

        private Uri BuildUri(string suffix, params QueryParameter[] parameters)
        {
            return new Uri($@"{BaseUrl}{suffix}?key={_apiKey}&token={_token}" + GetParametersAsString(parameters));
        }

        internal async Task Delete(string suffix)
        {
            var uri = BuildUri(suffix);
            var response = await _httpClient.DeleteAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
            }
        }
    }
}