using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
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

        internal async Task<T> Get<T>(string suffix, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            string json = await Get(suffix, cancellationToken, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        internal async Task<string> Get(string suffix, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.GetAsync(uri, cancellationToken);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }

        internal async Task<T> Post<T>(string suffix, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            string json = await Post(suffix, cancellationToken, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        internal async Task<T> PostWithAttachmentFileUpload<T>(string suffix, AttachmentFileUpload attachmentFile, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            string json = await PostWithAttachmentFileUpload(suffix, attachmentFile, cancellationToken, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        internal async Task<string> PostWithAttachmentFileUpload(string suffix, AttachmentFileUpload attachmentFile, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                multipartFormContent.Add(new StreamContent(attachmentFile.Stream), name: @"file", fileName: attachmentFile.Filename);
                var response = await _httpClient.PostAsync(uri, multipartFormContent, cancellationToken);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new TrelloApiException(responseContent, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
                }
                return responseContent; //Content is assumed JSON
            }
        }

        internal async Task<string> Post(string suffix, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.PostAsync(uri, null, cancellationToken);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }

        internal async Task<T> Put<T>(string suffix, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            string json = await Put(suffix, cancellationToken, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        internal async Task<string> Put(string suffix, CancellationToken cancellationToken, params QueryParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.PutAsync(uri, null, cancellationToken);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
            }
            return content; //Content is assumed JSON
        }

        internal async Task<T> PutWithJsonPayload<T>(string suffix, CancellationToken cancellationToken, string payload, params QueryParameter[] parameters)
        {
            string json = await PutWithJsonPayload(suffix, cancellationToken, payload, parameters);
            var @object = JsonSerializer.Deserialize<T>(json);
            return @object;
        }

        internal async Task<string> PutWithJsonPayload(string suffix, CancellationToken cancellationToken, string payload, params QueryParameter[] parameters)
        {
            var uri = BuildUri(suffix, parameters);
            var response = await _httpClient.PutAsync(uri, new StringContent(payload, Encoding.UTF8, "application/json"), cancellationToken);
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
                    // ReSharper disable StringLiteralTypo
                    return fullUrl.Replace($"?key={_apiKey}&token={_token}", "?key=XXXXX&token=XXXXXXXXXX");
                case ApiCallExceptionOption.DoNotIncludeTheUrl:
                    return string.Empty.PadLeft(5, 'X');
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

        internal async Task Delete(string suffix, CancellationToken cancellationToken)
        {
            var uri = BuildUri(suffix);
            var response = await _httpClient.DeleteAsync(uri, cancellationToken);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new TrelloApiException(content, FormatExceptionUrlAccordingToClientOptions(uri.AbsoluteUri)); //Content is assumed Error Message
            }
        }
    }
}