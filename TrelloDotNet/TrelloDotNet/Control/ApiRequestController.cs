using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrelloDotNet.Model;
using static System.Net.WebRequestMethods;

namespace TrelloDotNet.Control
{
    internal class ApiRequestController
    {
        private const string BaseUrl = "https://api.trello.com/1/";
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _token;

        public ApiRequestController(HttpClient httpClient, string apiKey, string token)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiKey = apiKey;
            _token = token;
        }

        public async Task<T> GetResponse<T>(string suffix)
        {
            string jsonResponse = await GetResponse(suffix);
            return JsonSerializer.Deserialize<T>(jsonResponse);
        }

        internal async Task<string> GetResponse(string suffix)
        {
            var response = await _httpClient.GetAsync(BuildGetUri(suffix));
            //todo - check response
            return await response.Content.ReadAsStringAsync();
        }

        private Uri BuildGetUri(string suffix)
        {
            suffix = RemoveStartSlashIfGiven(suffix);
            return new Uri($@"{BaseUrl}{suffix}?key={_apiKey}&token={_token}");
        }

        public async Task<T> Post<T>(string suffix, params UriParameter[] parameters)
        {
            string jsonResponse = await Post(suffix, parameters);
            return JsonSerializer.Deserialize<T>(jsonResponse);
        }

        public async Task<string> Post(string suffix, params UriParameter[] parameters)
        {
            var uri = BuildPostUri(suffix, parameters);
            var response = await _httpClient.PostAsync(uri, null);
            //todo - check response
            return await response.Content.ReadAsStringAsync();
        }

        private Uri BuildPostUri(string suffix, UriParameter[] parameters)
        {
            suffix = RemoveStartSlashIfGiven(suffix);
            StringBuilder parameterString = new StringBuilder();
            foreach (var parameter in parameters)
            {
                switch (parameter.Type)
                {
                    case RequestParameterType.String:
                        parameterString.Append($"&{parameter.Name}={parameter.GetValueAsString()}");
                        break;
                }
            }

            return new Uri($@"{BaseUrl}{suffix}?key={_apiKey}&token={_token}" + parameterString);
        }

        private static string RemoveStartSlashIfGiven(string suffix)
        {
            if (suffix.StartsWith("/"))
            {
                suffix = suffix.Substring(1);
            }

            return suffix;
        }
    }
}