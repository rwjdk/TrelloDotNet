using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public class TrelloClient : ITrelloClient
    {
        private readonly ApiRequestController _controller;

        /// <summary>
        /// The Trello API Key you generate on https://trello.com/power-ups/admin/
        /// </summary>
        public string ApiKey { get; }
        
        /// <summary>
        /// Your Authorization Token
        /// </summary>
        public string Token { get; }

        public TrelloClient(string apiKey, string token)
        {
            ApiKey = apiKey;
            Token = token;
            _controller = new ApiRequestController(new HttpClient(), apiKey, token);
            Boards = new BoardController(_controller);
            Lists = new ListController(_controller);
            Cards = new CardController(_controller);
        }

        /// <summary>
        /// Board-related API Methods
        /// </summary>
        public IBoardController Boards { get; }
        /// <summary>
        /// List-related API Methods
        /// </summary>
        public IListController Lists { get; }
        /// <summary>
        /// Cards-related API Methods
        /// </summary>
        public ICardController Cards { get; }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> GetAsync<T>(string suffix)
        {
            return await _controller.GetResponse<T>(suffix);
        }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> GetAsync(string suffix)
        {
            return await _controller.GetResponse(suffix);
        }

        public async Task<T> Post<T>(string suffix, params UriParameter[] parameters)
        {
            return await _controller.Post<T>(suffix, parameters);
        }

        public async Task<string> Post(string suffix, params UriParameter[] parameters)
        {
            return await _controller.Post(suffix, parameters);
        }

        //todo -should everything have the Async Suffix (and should there be non-async versions?)
    }
}
