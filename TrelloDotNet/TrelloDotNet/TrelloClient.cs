using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    /// <summary>
    /// The Main Client to communicate with the Trello API (aka everything is done via this)
    /// </summary>
    public class TrelloClient : ITrelloClient
    {
        private static HttpClient _staticHttpClient = new HttpClient();
        private readonly ApiRequestController _apiRequestController;
        private readonly QueryParametersBuilder _queryParametersBuilder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">The Trello API Key you get on https://trello.com/power-ups/admin/</param>
        /// <param name="token">Your Authorization Token you generate get on https://trello.com/power-ups/admin/</param>
        /// <param name="httpClient">Optional HTTP Client if you wish to specify it on your own (else an internal static HttpClient will be used for re-use)</param>
        public TrelloClient(string apiKey, string token, HttpClient httpClient = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException(@"You need to specify an API Key. Get it on page: https://trello.com/power-ups/admin/");
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(@"You need to specify a Token. Generate it on page: https://trello.com/power-ups/admin/");
            }

            if (httpClient != null)
            {
                _staticHttpClient = httpClient;
            }

            _apiRequestController = new ApiRequestController(_staticHttpClient, apiKey, token);
            _queryParametersBuilder = new QueryParametersBuilder();
        }


        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> GetAsync<T>(string suffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Get<T>(suffix, parameters);
        }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> GetAsync(string suffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Get(suffix, parameters);
        }

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> PostAsync<T>(string suffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Post<T>(suffix, parameters);
        }

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PostAsync(string suffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Post(suffix, parameters);
        }

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> PutAsync<T>(string suffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Put<T>(suffix, parameters);
        }

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PutAsync(string suffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Put(suffix, parameters);
        }

        /// <summary>
        /// Get a Board by its Id
        /// </summary>
        /// <param name="id">Id of the Board (in its long or short version)</param>
        /// <returns>The Board</returns>
        public async Task<Board> GetBoardAsync(string id)
        {
            return await _apiRequestController.Get<Board>($"{UrlPaths.Boards}/{id}");
        }

        /// <summary>
        /// Get Card by its Id
        /// </summary>
        /// <param name="id">Id of the Card</param>
        /// <returns>The Card</returns>
        public async Task<Card> GetCardAsync(string id)
        {
            return await _apiRequestController.Get<Card>($"{UrlPaths.Cards}/{id}");
        }

        /// <summary>
        /// Get all open cards on un-archived lists
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/");
        }

        /// <summary>
        /// The cards on board based on their status regardless if they are on archived lists
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">The Selected Filter</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardFilteredAsync(string boardId, CardsFilter filter)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/{filter.GetJsonPropertyName()}");
        }

        /// <summary>
        /// Update a Card
        /// </summary>
        /// <remarks>
        /// Check description on each Card-property if it can be updated or not
        /// </remarks>
        /// <param name="card">The card with the changes</param>
        /// <returns>The Updated Card</returns>
        public async Task<Card> UpdateCardAsync(Card card)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{card.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(card));
        }

        /// <summary>
        /// Get a Checklist with a specific Id
        /// </summary>
        /// <param name="id">Id of the Checklist</param>
        /// <returns>The Checklist</returns>
        public Task<Checklist> GetChecklistAsync(string id)
        {
            return _apiRequestController.Get<Checklist>($"{UrlPaths.Checklists}/{id}");
        }

        /// <summary>
        /// Get list of Checklists that are used on cards on a specific Board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Checklists</returns>
        public async Task<List<Checklist>> GetChecklistsOnBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Checklist>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Checklists}");
        }

        /// <summary>
        /// Get list of Checklists that are used on a specific card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The Checklists</returns>
        public async Task<List<Checklist>> GetChecklistsOnCardAsync(string cardId)
        {
            return await _apiRequestController.Get<List<Checklist>>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Checklists}");
        }

        /// <summary>
        /// Add a Checklist to the card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="checklist">The Checklist to add</param>
        /// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true the card will be checked if a checklist with same name (case sensitive) exist and if so return that instead of creating a new</param>
        /// <returns>New or Existing Checklist with same name</returns>
        public async Task<Checklist> AddChecklistAsync(string cardId, Checklist checklist, bool ignoreIfAChecklistWithThisNameAlreadyExist = false)
        {
            var template = checklist;
            if (ignoreIfAChecklistWithThisNameAlreadyExist)
            {
                //Check if card already have a list with same name
                var existingOnCard = await GetChecklistsOnCardAsync(cardId);
                var existing = existingOnCard.FirstOrDefault(x => x.Name == template.Name);
                if (existing != null)
                {
                    return existing;
                }
            }

            var checklistParameters = _queryParametersBuilder.GetViaQueryParameterAttributes(template);
            var newChecklist = await _apiRequestController.Post<Checklist>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Checklists}", checklistParameters);
            foreach (var item in template.Items)
            {
                var checkItemParameters = _queryParametersBuilder.GetViaQueryParameterAttributes(item);
                newChecklist.Items.Add(await _apiRequestController.Post<ChecklistItem>($"{UrlPaths.Checklists}/{newChecklist.Id}/{UrlPaths.CheckItems}", checkItemParameters));
            }

            return newChecklist;
        }

        /// <summary>
        /// Add a Checklist to the card based on an existing checklist (as a copy)
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="existingChecklistIdToCopyFrom">Id of an existing Checklist that should be added to the card as a new copy</param>
        /// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true the card will be checked if a checklist with same name (case sensitive) exist and if so return that instead of creating a new</param>
        /// <returns>New Checklist</returns>
        public async Task<Checklist> AddChecklistAsync(string cardId, string existingChecklistIdToCopyFrom, bool ignoreIfAChecklistWithThisNameAlreadyExist = false)
        {
            if (ignoreIfAChecklistWithThisNameAlreadyExist)
            {
                //Find the name of the template (as we are only provided Id)
                var existingChecklist = await GetChecklistAsync(existingChecklistIdToCopyFrom);
                //Check if card already have a list with same name
                var existingOnCard = await GetChecklistsOnCardAsync(cardId);
                var existing = existingOnCard.FirstOrDefault(x => x.Name == existingChecklist.Name);
                if (existing != null)
                {
                    return existing;
                }
            }

            QueryParameter[] parameters =
            {
                new QueryParameter(QueryParameterNames.IdCheckListSource, existingChecklistIdToCopyFrom)
            };
            return await _apiRequestController.Post<Checklist>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Checklists}", parameters);
        }

        /// <summary>
        /// Get the Members (users) of a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Members</returns>
        public async Task<List<Member>> GetMembersOfBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Member>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Members}/");
        }

        /// <summary>
        /// Get a specific List (Column) based on it's Id
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <returns></returns>
        public async Task<List> GetListAsync(string listId)
        {
            return await _apiRequestController.Get<List>($"{UrlPaths.Lists}/{listId}");
        }

        /// <summary>
        /// Add a List to a Board
        /// </summary>
        /// <remarks>
        /// The Provided BoardId the list should be added to need to be the long version of the BoardId as API does not support the short version
        /// </remarks>
        /// <param name="list">List to add</param>
        /// <returns>The Create list</returns>
        public async Task<List> AddListAsync(List list)
        {
            return await _apiRequestController.Post<List>($"{UrlPaths.Lists}", _queryParametersBuilder.GetViaQueryParameterAttributes(list));
        }

        /// <summary>
        /// Get Lists (Columns) on a Board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">Filter Lists based on status</param>
        /// <returns>List of Lists (Columns)</returns>
        public async Task<List<List>> GetListsOnBoardAsync(string boardId, ListFilter filter = ListFilter.Open)
        {
            return await _apiRequestController.Get<List<List>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Lists}", new QueryParameter(QueryParameterNames.Filter, filter.GetJsonPropertyName()));
        }

        /// <summary>
        /// Get List of Labels defined for a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Labels</returns>
        public async Task<List<Label>> GetLabelsOfBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Label>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Labels}");
        }
    }
}