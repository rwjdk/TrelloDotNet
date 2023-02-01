using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
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

        public async Task<T> GetResponse<T>(string suffix, params UriParameter[] parameters)
        {
            string json = await GetResponse(suffix, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            if (@object is IRawJsonObject rawJsonObject) //todo should it be an option (bigger objects)
            {
                rawJsonObject.RawJson = json;
            }
            return @object;
        }
        
        internal async Task<string> GetResponse(string suffix, params UriParameter[] parameters)
        {
            var uri = BuildGetUri(suffix, parameters);
            var response = await _httpClient.GetAsync(uri);
            //todo - check response
            return await response.Content.ReadAsStringAsync();
        }

        private Uri BuildGetUri(string suffix, UriParameter[] parameters)
        {
            suffix = RemoveStartSlashIfGiven(suffix);
            return new Uri($@"{BaseUrl}{suffix}?key={_apiKey}&token={_token}" + GetParametersAsString(parameters));
        }

        public async Task<T> Post<T>(string suffix, params UriParameter[] parameters)
        {
            string json = await Post(suffix, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            if (@object is RawJsonObject baseTrelloObject) //todo should it be an option (bigger objects)
            {
                baseTrelloObject.RawJson = json;
            }
            return @object;
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

            return new Uri($@"{BaseUrl}{suffix}?key={_apiKey}&token={_token}" + GetParametersAsString(parameters));
        }

        private static StringBuilder GetParametersAsString(UriParameter[] parameters)
        {
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

            return parameterString;
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