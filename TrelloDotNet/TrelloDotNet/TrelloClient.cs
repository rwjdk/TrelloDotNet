using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using Action = TrelloDotNet.Model.Action;

namespace TrelloDotNet
{
    /// <summary>
    /// The Main Client to communicate with the Trello API (aka everything is done via this)
    /// </summary>
    public class TrelloClient
    {
        //todo - Other
        //- Create unit-test suite
        //- Common Scenario/Actions List (aka things that is not a one to one API call... Example: "Move Card to List with name" so user do not need to set everything up themselves)

        //todo: Management
        //- Manage Custom Fields on board (CRUD)
        //- Manage Labels
        //- Batch-system (why???)
        //- Web-hooks (+ reaction to it)
        //- Workspace management
        //- Organizations (how to gain access?)

        //todo: Boards
        //- Get Board Membership (Aka what roles the Token user have on the board) [https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-memberships-get]
        //- Invite members by mail or userId to board [https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-put + https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-put]
        //- Remove Members from board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-delete)
        //- Update Membership on board (make admin as an example)
        //- WIP: Create Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-post)

        //todo: Lists
        //- Delete List
        //- Archive All Cards on list (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-archiveallcards-post)
        //- Move all Cards on list (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-moveallcards-post)
        //- Update/Move List (https://developer.atlassian.com/cloud/trello/rest/api-group-lists/#api-lists-id-idboard-put)

        //todo: Cards
        //- Copy Card
        //- Get members of a Card
        //- Card: Delete a card
        //- Card: Attachments CRUD
        //- Card: Support Stickers
        //- Card: Comments CRUD
        //- Card: Custom Fields CRUD

        //todo: Actions
        //- Members
        //- Cards
        //- Lists
        //- Boards (WIP)

        /// <summary>
        /// Options for the client
        /// </summary>
        public TrelloClientOptions Options { get; }

        private readonly ApiRequestController _apiRequestController;
        private readonly QueryParametersBuilder _queryParametersBuilder;
        private readonly HttpClient _staticHttpClient = new HttpClient();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">The Trello API Key you get on https://trello.com/power-ups/admin/</param>
        /// <param name="token">Your Authorization Token you generate get on https://trello.com/power-ups/admin/</param>
        /// <param name="options">Various option for the client (if null default options will be used)</param>
        /// <param name="httpClient">Optional HTTP Client if you wish to specify it on your own (else an internal static HttpClient will be used for re-use)</param>
        public TrelloClient(string apiKey, string token, TrelloClientOptions options = null, HttpClient httpClient = null)
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

            Options = options ?? new TrelloClientOptions();
            _apiRequestController = new ApiRequestController(_staticHttpClient, apiKey, token, this);
            _queryParametersBuilder = new QueryParametersBuilder();
        }

        #region Add

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
                new QueryParameter("idChecklistSource", existingChecklistIdToCopyFrom)
            };
            return await _apiRequestController.Post<Checklist>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Checklists}", parameters);
        }

        /// <summary>
        /// Add a Card
        /// </summary>
        /// <param name="card">The Card to Add</param>
        /// <returns>The Added Card</returns>
        public async Task<Card> AddCardAsync(Card card)
        {
            return await _apiRequestController.Post<Card>($"{UrlPaths.Cards}", _queryParametersBuilder.GetViaQueryParameterAttributes(card));
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
        /// Add a new Board
        /// </summary>
        /// <param name="board">The Board to Add</param>
        /// <returns>The New Board</returns>
        public async Task<Board> AddBoardAsync(Board board)
        {
            //todo - add creation options: https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-post
            return await _apiRequestController.Post<Board>($"{UrlPaths.Boards}", _queryParametersBuilder.GetViaQueryParameterAttributes(board));
        }

        #endregion

        #region Update

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
        /// Update a Card
        /// </summary>
        /// <param name="card">The card with the changes</param>
        /// <returns>The Updated Card</returns>
        public async Task<Card> UpdateCardAsync(Card card)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{card.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(card));
        }

        /// <summary>
        /// Update a Board
        /// </summary>
        /// <param name="board">The board with the changes</param>
        /// <returns>The Updated Card</returns>
        public async Task<Board> UpdateBoardAsync(Board board)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{board.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(board));
        }

        /// <summary>
        /// Update a List
        /// </summary>
        /// <param name="list">The List with the changes</param>
        /// <returns>The Updated List</returns>
        public async Task<List> UpdateListAsync(List list)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{list.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(list));
        }

        #endregion

        #region Delete
        
        /// <summary>
        /// Delete a entire board (WARNING: THERE IS NO WAY GOING BACK!!!)
        /// </summary>
        /// <param name="boardId">The id of the Board to Delete</param>
        public async Task DeleteBoard(string boardId)
        {
            if (Options.AllowDeleteOfBoards)
            {
                await _apiRequestController.Delete($"{UrlPaths.Boards}/{boardId}");
            }
            else
            {
                throw new SecurityException("Deletion of Boards are disabled via TrelloClient.Options.AllowDeleteOfBoards (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back: https://support.atlassian.com/trello/docs/deleting-a-board/)");
            }
        }

        #endregion

        #region Get

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
        /// Get all open cards on a specific list
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnListAsync(string listId)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Lists}/{listId}/{UrlPaths.Cards}/");
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
        /// The Lists on board based on their status
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">The Selected Filter</param>
        /// <returns>List of Cards</returns>
        public async Task<List<List>> GetListsOnBoardFilteredAsync(string boardId, ListFilter filter)
        {
            return await _apiRequestController.Get<List<List>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Lists}/{filter.GetJsonPropertyName()}");
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
        /// Get Lists (Columns) on a Board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Lists (Columns)</returns>
        public async Task<List<List>> GetListsOnBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<List>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Lists}");
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

        /// <summary>
        /// Get a Member with a specific Id
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <returns>The Member</returns>
        public async Task<Member> GetMember(string memberId)
        {
            return await _apiRequestController.Get<Member>($"{UrlPaths.Members}/{memberId}");
        }

        #endregion

        #region WIP

        internal async Task<List<Action>> GetActionsOnBoard(string boardId) //todo - turn public once ready
        {
            return await _apiRequestController.Get<List<Action>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Actions}");
        }

        internal static TrelloClientSingleBoard<TList, TLabel> CreateSingleBoard<TList, TLabel>(string apiKey, string token, BoardInfo<TList, TLabel> boardInfo) where TList : Enum where TLabel : Enum //todo - turn public once ready
        {
            return new TrelloClientSingleBoard<TList, TLabel>(apiKey, token, boardInfo);
        }

        #endregion
    }
}