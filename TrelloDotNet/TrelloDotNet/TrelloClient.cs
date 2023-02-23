using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet
{
    /// <summary>
    /// The Main Client to communicate with the Trello API (aka everything is done via this)
    /// </summary>
    public class TrelloClient
    {
        //todo - Other
        //- Common Scenario/Actions List (aka things that is not a one to one API call... Example: "Move Card to List with name" so user do not need to set everything up themselves)

        //todo: Management
        //- Manage Custom Fields on board (CRUD)
        //- Manage Labels
        //- Batch-system (why???)
        //- Workspace management
        //- Organizations (how to gain access?)

        //todo: Boards
        //- Get Board Membership (Aka what roles the Token user have on the board) [https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-memberships-get]
        //- Invite members by mail or userId to board [https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-put + https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-put]
        //- Remove Members from board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-delete)
        //- Update Membership on board (make admin as an example)

        //todo: Cards
        //- Copy Card
        //- Card: Attachments CRUD
        //- Card: Support Stickers
        //- Card: Comments CRUD
        //- Card: Custom Fields CRUD

        //todo: Actions
        //- Members
        //- Cards
        //- Lists
        //- WIP: Boards

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
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> PostAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Post<T>(urlSuffix, parameters);
        }

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PostAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Post(urlSuffix, parameters);
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
        /// <param name="options">Options for the new board</param>
        /// <returns>The New Board</returns>
        public async Task<Board> AddBoardAsync(Board board, AddBoardOptions options = null)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(board).ToList();
            if (options != null)
            {
                parameters.AddRange(_queryParametersBuilder.GetViaQueryParameterAttributes(options));
            }
            return await _apiRequestController.Post<Board>($"{UrlPaths.Boards}", parameters.ToArray());
        }

        /// <summary>
        /// Add a new Webhook
        /// </summary>
        /// <param name="webhook">The Webhook to add</param>
        /// <returns>The Webhook</returns>
        public async Task<Webhook> AddWebhookAsync(Webhook webhook)
        {
            return await _apiRequestController.Post<Webhook>($"{UrlPaths.Webhooks}", _queryParametersBuilder.GetViaQueryParameterAttributes(webhook));
        }

        /// <summary>
        /// Add a new Comment on a Card
        /// </summary>
        /// <paramref name="cardId">Id of the Card</paramref>
        /// <paramref name="comment">The Comment</paramref>
        /// <returns>The Comment</returns>
        public async Task<Comment> AddCommentAsync(string cardId, Comment comment)
        {
            return await _apiRequestController.Post<Comment>($"{UrlPaths.Cards}/{cardId}/actions/comments", _queryParametersBuilder.GetViaQueryParameterAttributes(comment));
        }

        /// <summary>
        /// Add a sticker to a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="sticker">The Sticker to add</param>
        /// <returns>The new sticker</returns>
        public async Task<Sticker> AddStickerToCardAsync(string cardId, Sticker sticker)
        {
            return await _apiRequestController.Post<Sticker>($"{UrlPaths.Cards}/{cardId}/stickers", _queryParametersBuilder.GetViaQueryParameterAttributes(sticker));
        }

        #endregion

        #region Update

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> PutAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Put<T>(urlSuffix, parameters);
        }

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PutAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Put(urlSuffix, parameters);
        }

        /// <summary>
        /// Archive a List
        /// </summary>
        /// <param name="listId">The id of list that should be Archived</param>
        /// <returns>The Archived List</returns>
        public async Task<List> ArchiveListAsync(string listId)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}", new QueryParameter("closed", true));
        }

        /// <summary>
        /// Reopen a List (Send back to the board)
        /// </summary>
        /// <param name="listId">The id of list that should be Reopened</param>
        /// <returns>The Archived List</returns>
        public async Task<List> ReOpenListAsync(string listId)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}", new QueryParameter("closed", false));
        }

        /// <summary>
        /// Archive (Close) a Card
        /// </summary>
        /// <param name="cardId">The id of card that should be archived</param>
        /// <returns>The Archived Card</returns>
        public async Task<Card> ArchiveCardAsync(string cardId)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", new QueryParameter("closed", true));
        }

        /// <summary>
        /// ReOpen (Send back to board) a Card
        /// </summary>
        /// <param name="cardId">The id of card that should be reopened</param>
        /// <returns>The ReOpened Card</returns>
        public async Task<Card> ReOpenCardAsync(string cardId)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", new QueryParameter("closed", false));
        }

        /// <summary>
        /// Close (Archive) a Board
        /// </summary>
        /// <param name="boardId">The id of board that should be closed</param>
        /// <returns>The Closed Board</returns>
        public async Task<Board> CloseBoardAsync(string boardId)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{boardId}", new QueryParameter("closed", true));
        }

        /// <summary>
        /// ReOpen a Board
        /// </summary>
        /// <param name="boardId">The id of board that should be reopened</param>
        /// <returns>The ReOpened Board</returns>
        public async Task<Board> ReOpenBoardAsync(string boardId)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{boardId}", new QueryParameter("closed", false));
        }

        /// <summary>
        /// Update a Card
        /// </summary>
        /// <param name="cardWithChanges">The card with the changes</param>
        /// <returns>The Updated Card</returns>
        public async Task<Card> UpdateCardAsync(Card cardWithChanges)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardWithChanges.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(cardWithChanges));
        }

        /// <summary>
        /// Update a Board
        /// </summary>
        /// <param name="boardWithChanges">The board with the changes</param>
        /// <returns>The Updated Card</returns>
        public async Task<Board> UpdateBoardAsync(Board boardWithChanges)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{boardWithChanges.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(boardWithChanges));
        }

        /// <summary>
        /// Update a List
        /// </summary>
        /// <param name="listWithChanges">The List with the changes</param>
        /// <returns>The Updated List</returns>
        public async Task<List> UpdateListAsync(List listWithChanges)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listWithChanges.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(listWithChanges));
        }

        /// <summary>
        /// Update a webhook
        /// </summary>
        /// <param name="webhookWithChanges">The Webhook with changes</param>
        /// <returns>The Updated Webhook</returns>
        public async Task<Webhook> UpdateWebhookAsync(Webhook webhookWithChanges)
        {
            return await _apiRequestController.Put<Webhook>($"{UrlPaths.Webhooks}/{webhookWithChanges.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(webhookWithChanges));
        }

        /// <summary>
        /// Update a sticker
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="stickerWithUpdates">The Sticker to update</param>
        /// <returns>The Updated Sticker</returns>
        public async Task<Sticker> UpdateStickerAsync(string cardId, Sticker stickerWithUpdates)
        {
            return await _apiRequestController.Put<Sticker>($"{UrlPaths.Cards}/{cardId}/stickers/{stickerWithUpdates.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(stickerWithUpdates));
        }

        /// <summary>
        /// Move an entire list to another board
        /// </summary>
        /// <param name="listId">The id of the List to move</param>
        /// <param name="newBoardId">The id of the board the list should be moved to [It need to be the long version of the boardId]</param>
        /// <returns>The Updated List</returns>
        public async Task<List> MoveListToBoardAsync(string listId, string newBoardId)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}/idBoard", new QueryParameter("value", newBoardId));
        }

        /// <summary>
        /// Archive all cards on in a List
        /// </summary>
        /// <param name="listId">The id of the List that should have its cards archived</param>
        public async Task ArchiveAllCardsInListAsync(string listId)
        {
            await _apiRequestController.Post<List>($"{UrlPaths.Lists}/{listId}/archiveAllCards");
        }

        /// <summary>
        /// Move all cards of a list to another list
        /// </summary>
        /// <param name="currentListId">The id of the List that should have its cards moved</param>
        /// <param name="newListId">The id of the new List that should receive the cards</param>
        public async Task MoveAllCardsInListAsync(string currentListId, string newListId)
        {
            var newList = await GetListAsync(newListId); //Get the new list's BoardId so the user do not need to provide it.
            await _apiRequestController.Post($"{UrlPaths.Lists}/{currentListId}/moveAllCards",
                new QueryParameter("idBoard", newList.BoardId),
                new QueryParameter("idList", newListId)
                );
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete a entire board (WARNING: THERE IS NO WAY GOING BACK!!!). Alternative use CloseBoard() for non-permanency
        /// </summary>
        /// <remarks>
        /// As this is a major thing, there is a secondary confirm needed by setting: Options.AllowDeleteOfBoards = true
        /// </remarks>
        /// <param name="boardId">The id of the Board to Delete</param>
        public async Task DeleteBoardAsync(string boardId)
        {
            if (Options.AllowDeleteOfBoards)
            {
                await _apiRequestController.Delete($"{UrlPaths.Boards}/{boardId}");
            }
            else
            {
                throw new SecurityException("Deletion of Boards are disabled via Options.AllowDeleteOfBoards (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back: https://support.atlassian.com/trello/docs/deleting-a-board/)");
            }
        }

        /// <summary>
        /// Delete a Card (WARNING: THERE IS NO WAY GOING BACK!!!). Alternative use CloseCard() for non-permanency
        /// </summary>
        /// <param name="webhookId">The id of the Board to Delete</param>
        public async Task DeleteCardAsync(string webhookId)
        {
            await _apiRequestController.Delete($"{UrlPaths.Cards}/{webhookId}");
        }

        /// <summary>
        /// Delete a Webhook (WARNING: THERE IS NO WAY GOING BACK!!!).
        /// </summary>
        /// <param name="webhookId">The id of the Webhook to Delete</param>
        public async Task DeleteWebhookAsync(string webhookId)
        {
            await _apiRequestController.Delete($"{UrlPaths.Webhooks}/{webhookId}");
        }

        /// <summary>
        /// Delete a sticker (WARNING: THERE IS NO WAY GOING BACK!!!).
        /// </summary>
        /// <param name="cardId">Id of Card that have the sticker</param>
        /// <param name="stickerId">Id of the sticker</param>
        public async Task DeleteStickerAsync(string cardId, string stickerId)
        {
            await _apiRequestController.Delete($"{UrlPaths.Cards}/{cardId}/stickers/{stickerId}");
        }

        /// <summary>
        /// Custom Delete Method to be used on unexposed features of the API.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        public async Task DeleteAsync(string urlSuffix)
        {
            await _apiRequestController.Delete(urlSuffix);
        }

        #endregion

        #region Get

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        public async Task<T> GetAsync<T>(string urlSuffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Get<T>(urlSuffix, parameters);
        }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> GetAsync(string urlSuffix, params QueryParameter[] parameters)
        {
            return await _apiRequestController.Get(urlSuffix, parameters);
        }

        /// <summary>
        /// Get a Board by its Id
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>The Board</returns>
        public async Task<Board> GetBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<Board>($"{UrlPaths.Boards}/{boardId}");
        }

        /// <summary>
        /// Get Card by its Id
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The Card</returns>
        public async Task<Card> GetCardAsync(string cardId)
        {
            return await _apiRequestController.Get<Card>($"{UrlPaths.Cards}/{cardId}");
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
        public async Task<List<Card>> GetCardsInListAsync(string listId)
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
        /// Get the Members (users) of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>List of Members</returns>
        public async Task<List<Member>> GetMembersOfCardAsync(string cardId)
        {
            return await _apiRequestController.Get<List<Member>>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Members}/");
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
        public async Task<Member> GetMemberAsync(string memberId)
        {
            return await _apiRequestController.Get<Member>($"{UrlPaths.Members}/{memberId}");
        }

        /// <summary>
        /// Get Webhooks linked with the current Token used to authenticate with the API
        /// </summary>
        /// <returns>List of Webhooks</returns>
        public async Task<List<Webhook>> GetWebhooksForCurrentTokenAsync()
        {
            return await _apiRequestController.Get<List<Webhook>>($"{UrlPaths.Tokens}/{_apiRequestController.Token}/webhooks");
        }

        /// <summary>
        /// Get a Webhook from its Id
        /// </summary>
        /// <param name="webhookId">Id of the Webhook</param>
        /// <returns>The Webhook</returns>
        public async Task<Webhook> GetWebhookAsync(string webhookId)
        {
            return await _apiRequestController.Get<Webhook>($"{UrlPaths.Webhooks}/{webhookId}");
        }

        /// <summary>
        /// Get List of Stickers on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The List of Stickers</returns>
        public async Task<List<Sticker>> GetStickersOnCardAsync(string cardId)
        {
            return await _apiRequestController.Get<List<Sticker>>($"{UrlPaths.Cards}/{cardId}/stickers");
        }
        
        /// <summary>
        /// Get a Stickers with a specific Id
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="stickerId">Id of the Sticker</param>
        /// <returns>The Sticker</returns>
        public async Task<Sticker> GetStickerAsync(string cardId, string stickerId)
        {
            return await _apiRequestController.Get<Sticker>($"{UrlPaths.Cards}/{cardId}/stickers/{stickerId}");
        }

        #endregion

        #region Ease of Use Methods

        /// <summary>
        /// Add a Member to a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="memberIdsToAdd">One or more Ids of Members to add</param>
        public async Task<Card> AddMembersToCardAsync(string cardId, params string[] memberIdsToAdd)
        {
            var card = await GetCardAsync(cardId);
            var missing = memberIdsToAdd.Where(x => !card.MemberIds.Contains(x)).ToList();

            if (missing.Count == 0)
            {
                return card; //Everyone already There
            }

            //Need update
            card.MemberIds.AddRange(missing);
            return await UpdateCardAsync(card);
        }

        /// <summary>
        /// Remove a Member of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="memberIdsToRemove">One or more Ids of Members to remove</param>
        public async Task<Card> RemoveMembersFromCardAsync(string cardId, params string[] memberIdsToRemove)
        {
            var card = await GetCardAsync(cardId);
            var toRemove = memberIdsToRemove.Where(x => card.MemberIds.Contains(x)).ToList();
            if (toRemove.Count == 0)
            {
                return card; //Everyone not there
            }

            //Need update
            card.MemberIds = card.MemberIds.Except(toRemove).ToList();
            return await UpdateCardAsync(card);
        }
        
        /// <summary>
        /// Remove all Members of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        public async Task<Card> RemoveAllMembersFromCardAsync(string cardId)
        {
            var card = await GetCardAsync(cardId);
            if (card.MemberIds.Any())
            {
                //Need update
                card.MemberIds = new List<string>();
                return await UpdateCardAsync(card);
            }
            return card;
        }

        /// <summary>
        /// Add a Label to a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="labelIdsToAdd">One or more Ids of Labels to add</param>
        public async Task<Card> AddLabelsToCardAsync(string cardId, params string[] labelIdsToAdd)
        {
            var card = await GetCardAsync(cardId);
            var missing = labelIdsToAdd.Where(x => !card.LabelIds.Contains(x)).ToList();

            if (missing.Count == 0)
            {
                return card; //All already There
            }

            //Need update
            card.LabelIds.AddRange(missing);
            return await UpdateCardAsync(card);
        }

        /// <summary>
        /// Remove a Label of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="labelIdsToRemove">One or more Ids of Labels to remove</param>
        public async Task<Card> RemoveLabelsFromCardAsync(string cardId, params string[] labelIdsToRemove)
        {
            var card = await GetCardAsync(cardId);
            var toRemove = labelIdsToRemove.Where(x => card.LabelIds.Contains(x)).ToList();
            if (toRemove.Count == 0)
            {
                return card; //All not there
            }

            //Need update
            card.LabelIds = card.LabelIds.Except(toRemove).ToList();
            return await UpdateCardAsync(card);
        }

        /// <summary>
        /// Remove all Labels of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        public async Task<Card> RemoveAllLabelsFromCardAsync(string cardId)
        {
            var card = await GetCardAsync(cardId);
            if (card.LabelIds.Any())
            {
                //Need update
                card.LabelIds = new List<string>();
                return await UpdateCardAsync(card);
            }
            return card;
        }

        /// <summary>
        /// Set Due Date on a card a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="dueDate">The Due Date (In UTC Time)</param>
        /// <param name="dueComplete">If Due is complete</param>
        public async Task<Card> SetDueDateOnCardAsync(string cardId, DateTimeOffset dueDate, bool dueComplete = false)
        {
            var card = await GetCardAsync(cardId);
            card.Due = dueDate;
            card.DueComplete = dueComplete;
            return await UpdateCardAsync(card);
        }
        
        /// <summary>
        /// Set Due Date on a card a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="startDate">The Start Date (In UTC Time)</param>
        public async Task<Card> SetStartDateOnCardAsync(string cardId, DateTimeOffset startDate)
        {
            var card = await GetCardAsync(cardId);
            card.Start = startDate;
            return await UpdateCardAsync(card);
        }

        /// <summary>
        /// Set Start and Due Date on a card a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="startDate">The Start Date (In UTC Time)</param>
        /// <param name="dueDate">The Due Date (In UTC Time)</param>
        /// <param name="dueComplete">If Due is complete</param> 
        public async Task<Card> SetStartDateAndDueDateOnCardAsync(string cardId, DateTimeOffset startDate, DateTimeOffset dueDate, bool dueComplete = false)
        {
            var card = await GetCardAsync(cardId);
            card.Start = startDate;
            card.Due = dueDate;
            card.DueComplete = dueComplete;
            return await UpdateCardAsync(card);
        }

        #endregion


    }
}