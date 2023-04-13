using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Actions;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet
{
    /// <summary>
    /// The Main Client to communicate with the Trello API (aka everything is done via this)
    /// </summary>
    public class TrelloClient
    {
        //todo: Management
        //- Manage Custom Fields on board (CRUD)
        //- Manage Labels (Delete/Add)
        //- Batch-system (why???)
        //- Workspace management
        //- Organizations (how to gain access?)

        //todo: Boards
        //- Get Board Membership (Aka what roles the Token user have on the board)
        //- Invite members by mail or userId to board
        //- Remove Members from board
        //- Update Membership on board (make admin as an example)

        //todo: Members
        //- Get Cards for Member

        //todo: Actions
        //- Members
        //- Cards
        //- Lists
        //- Boards

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

            if (template.Items == null)
            {
                return newChecklist;
            }

            if (template.Items.TrueForAll(x => x.Position == 0)) //Give positions to have the system have same order as in list
            {
                int position = 1;
                foreach (var item in template.Items)
                {
                    item.Position = position;
                    position++;
                }
            }

            foreach (var checkItemParameters in template.Items.Select(item => _queryParametersBuilder.GetViaQueryParameterAttributes(item)))
            {
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
                new QueryParameter(@"idChecklistSource", existingChecklistIdToCopyFrom)
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
        /// <returns>The Comment Action</returns>
        public async Task<TrelloAction> AddCommentAsync(string cardId, Comment comment)
        {
            return await _apiRequestController.Post<TrelloAction>($"{UrlPaths.Cards}/{cardId}/actions/comments", _queryParametersBuilder.GetViaQueryParameterAttributes(comment));
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

        /// <summary>
        /// Update a Cover to a card (this is equivalent to AddCoverToCardAsync, but here for discover-ability. Tip: It is also possible to update the cover via UpdateCardAsync)
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="newCover">The new Cover</param>
        public async Task<Card> UpdateCoverOnCardAsync(string cardId, CardCover newCover)
        {
            return await AddCoverToCardAsync(cardId, newCover);
        }

        /// <summary>
        /// Add a Cover to a card. Tip: It is also possible to update the cover via UpdateCardAsync
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="coverToAdd">The Cover to Add</param>
        public async Task<Card> AddCoverToCardAsync(string cardId, CardCover coverToAdd)
        {
            if (coverToAdd == null)
            {
                throw new TrelloApiException(@"Cover can't be null (If you trying to remove a cover see 'RemoveCoverFromCardAsync')", string.Empty);
            }

            coverToAdd.PrepareForAddUpdate();
            string payload = $"{{\"cover\":{JsonSerializer.Serialize(coverToAdd)}}}";
            return await _apiRequestController.PutWithJsonPayload<Card>($"{UrlPaths.Cards}/{cardId}", payload);
        }

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
        /// Custom Put Method (with JSON Payload) to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="payload">JSON Payload (In the rare cases Trello API need this)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        public async Task<string> PutAsync(string urlSuffix, string payload, params QueryParameter[] parameters)
        {
            return await _apiRequestController.PutWithJsonPayload(urlSuffix, payload, parameters);
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
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", new QueryParameter(@"closed", false));
        }

        /// <summary>
        /// Close (Archive) a Board
        /// </summary>
        /// <param name="boardId">The id of board that should be closed</param>
        /// <returns>The Closed Board</returns>
        public async Task<Board> CloseBoardAsync(string boardId)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{boardId}", new QueryParameter(@"closed", true));
        }

        /// <summary>
        /// ReOpen a Board
        /// </summary>
        /// <param name="boardId">The id of board that should be reopened</param>
        /// <returns>The ReOpened Board</returns>
        public async Task<Board> ReOpenBoardAsync(string boardId)
        {
            return await _apiRequestController.Put<Board>($"{UrlPaths.Boards}/{boardId}", new QueryParameter(@"closed", false));
        }

        /// <summary>
        /// Update a Card
        /// </summary>
        /// <param name="cardWithChanges">The card with the changes</param>
        /// <returns>The Updated Card</returns>
        public async Task<Card> UpdateCardAsync(Card cardWithChanges)
        {
            var parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(cardWithChanges).ToList();
            //Special code for Cover
            string payload = string.Empty;
            if (cardWithChanges.Cover == null)
            {
                //Remove cover
                parameters.Add(new QueryParameter(@"cover", ""));
            }
            else
            {
                cardWithChanges.Cover.PrepareForAddUpdate();
                if (cardWithChanges.Cover.Color != null || cardWithChanges.Cover.BackgroundImageId != null)
                {
                    parameters.Remove(parameters.First(x => x.Name == "idAttachmentCover")); //This parameter can't be there while a cover is added
                }
                payload = $"{{\"cover\":{JsonSerializer.Serialize(cardWithChanges.Cover)}}}";
            }
            return await _apiRequestController.PutWithJsonPayload<Card>($"{UrlPaths.Cards}/{cardWithChanges.Id}", payload, parameters.ToArray());
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
        /// Update a Check-item on a Card
        /// </summary>
        /// <param name="cardId">The Id of the Card the ChecklistItem is on</param>
        /// <param name="item">The updated Check-item</param>
        /// <returns>The Updated Checklist Item</returns>
        public async Task<ChecklistItem> UpdateChecklistItemAsync(string cardId, ChecklistItem item)
        {
            return await _apiRequestController.Put<ChecklistItem>($"{UrlPaths.Cards}/{cardId}/checkItem/{item.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(item));
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
        /// Update a Custom field on a Card
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="customField">The custom Field to update</param>
        /// <param name="newValue">The new value</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, bool newValue)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Checkbox:
                    var valueAsString = newValue ? "true" : "false";
                    payload = $"{{\"value\": {{ \"checked\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}}}";
                    break;
                case CustomFieldType.Date:
                case CustomFieldType.List:
                case CustomFieldType.Number:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), "Only a custom field of type 'Checkbox' can be set with a bool value");
            }
            await SendCustomFieldChangeRequestAsync(cardId, customField, payload);
        }

        /// <summary>
        /// Update a Custom field on a Card
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="customField">The custom Field to update</param>
        /// <param name="newValue">The new value</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, DateTimeOffset newValue)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Date:
                    string valueAsString = newValue.UtcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
                    payload = $"{{\"value\": {{ \"date\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}}}";
                    break;
                case CustomFieldType.Checkbox:
                case CustomFieldType.List:
                case CustomFieldType.Number:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), @"Only a custom field of type 'Date' can be set with a DateTimeOffset value");
            }
            await SendCustomFieldChangeRequestAsync(cardId, customField, payload);
        }

        /// <summary>
        /// Update a Custom field on a Card
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="customField">The custom Field to update</param>
        /// <param name="newValue">The new value</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, int newValue)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Number:
                    var valueAsString = newValue.ToString(CultureInfo.InvariantCulture);
                    payload = $"{{\"value\": {{ \"number\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}}}";
                    break;
                case CustomFieldType.Checkbox:
                case CustomFieldType.Date:
                case CustomFieldType.List:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), @"Only a custom field of type 'Number' can be set with a integer value");
            }
            await SendCustomFieldChangeRequestAsync(cardId, customField, payload);

        }

        /// <summary>
        /// Update a Custom field on a Card
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="customField">The custom Field to update</param>
        /// <param name="newValue">The new value</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, decimal newValue)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Number:
                    var valueAsString = newValue.ToString(CultureInfo.InvariantCulture);
                    payload = $"{{\"value\": {{ \"number\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}}}";
                    break;
                case CustomFieldType.Checkbox:
                case CustomFieldType.Date:
                case CustomFieldType.List:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), @"Only a custom field of type 'Number' can be set with a decimal value");
            }
            await SendCustomFieldChangeRequestAsync(cardId, customField, payload);
        }

        /// <summary>
        /// Update a Custom field on a Card
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="customField">The custom Field to update</param>
        /// <param name="newValue">The new value</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, CustomFieldOption newValue)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.List:
                    string valueAsString = string.Empty;
                    if (newValue != null)
                    {
                        valueAsString = newValue.Id;
                    }
                    payload = $"{{\"idValue\": \"{HttpUtility.JavaScriptStringEncode(valueAsString)}\" }}";
                    break;
                case CustomFieldType.Checkbox:
                case CustomFieldType.Date:
                case CustomFieldType.Number:
                case CustomFieldType.Text:
                default:
                    throw new ArgumentOutOfRangeException(nameof(customField), @"Only a custom field of type 'List' can be set with a CustomFieldOption value");
            }
            await SendCustomFieldChangeRequestAsync(cardId, customField, payload);
        }

        /// <summary>
        /// Update a Custom field on a Card
        /// </summary>
        /// <remarks>
        /// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
        /// </remarks>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="customField">The custom Field to update</param>
        /// <param name="newValue">The new value</param>
        public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, string newValue)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Checkbox:
                    payload = $"{{\"value\": {{ \"checked\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}}}";
                    break;
                case CustomFieldType.Date:
                    payload = $"{{\"value\": {{ \"date\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}}}";
                    break;
                case CustomFieldType.List:
                    payload = $"{{\"idValue\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}";
                    break;
                case CustomFieldType.Number:
                    payload = $"{{\"value\": {{ \"number\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}}}";
                    break;
                case CustomFieldType.Text:
                    payload = $"{{\"value\": {{ \"text\": \"{HttpUtility.JavaScriptStringEncode(newValue)}\" }}}}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            await SendCustomFieldChangeRequestAsync(cardId, customField, payload);
        }

        /// <summary>
        /// Clear a Custom field on a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="customField">The custom Field to clear</param>
        public async Task ClearCustomFieldValueOnCardAsync(string cardId, CustomField customField)
        {
            string payload;
            switch (customField.Type)
            {
                case CustomFieldType.Checkbox:
                case CustomFieldType.Date:
                case CustomFieldType.Number:
                case CustomFieldType.Text:
                    payload = "{\"value\": \"\" }";
                    break;
                case CustomFieldType.List:
                    payload = "{\"idValue\": \"\" }";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            await SendCustomFieldChangeRequestAsync(cardId, customField, payload);
        }

        private async Task SendCustomFieldChangeRequestAsync(string cardId, CustomField customField, string payload)
        {
            await _apiRequestController.PutWithJsonPayload($"{UrlPaths.Cards}/{cardId}/customField/{customField.Id}/item", payload);
        }

        /// <summary>
        /// Update a comment Action (aka only way to update comments as they are not seen as their own objects)
        /// </summary>
        /// <param name="commentAction">The comment Action with the updated text</param>
        public async Task<TrelloAction> UpdateCommentActionAsync(TrelloAction commentAction)
        {
            return await _apiRequestController.Put<TrelloAction>($"{UrlPaths.Actions}/{commentAction.Id}", new QueryParameter(@"text", commentAction.Data.Text));
        }

        /// <summary>
        /// Move an entire list to another board
        /// </summary>
        /// <param name="listId">The id of the List to move</param>
        /// <param name="newBoardId">The id of the board the list should be moved to [It need to be the long version of the boardId]</param>
        /// <returns>The Updated List</returns>
        public async Task<List> MoveListToBoardAsync(string listId, string newBoardId)
        {
            return await _apiRequestController.Put<List>($"{UrlPaths.Lists}/{listId}/idBoard", new QueryParameter(@"value", newBoardId));
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
                new QueryParameter(@"idBoard", newList.BoardId),
                new QueryParameter(@"idList", newListId)
                );
        }

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
                throw new SecurityException(@"Deletion of Boards are disabled via Options.AllowDeleteOfBoards (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back: https://support.atlassian.com/trello/docs/deleting-a-board/)");
            }
        }

        /// <summary>
        /// Delete a Card (WARNING: THERE IS NO WAY GOING BACK!!!). Alternative use CloseCard() for non-permanency
        /// </summary>
        /// <param name="cardId">The id of the Card to Delete</param>
        public async Task DeleteCardAsync(string cardId)
        {
            await _apiRequestController.Delete($"{UrlPaths.Cards}/{cardId}");
        }

        /// <summary>
        /// Delete a Label from the board and remove it from all cards it was added to (WARNING: THERE IS NO WAY GOING BACK!!!). If you are looking to remove a label from a Card then see 'RemoveLabelsFromCardAsync' and 'RemoveAllLabelsFromCardAsync'
        /// </summary>
        /// <param name="labelId">The id of the Label to Delete</param>
        public async Task DeleteLabelAsync(string labelId)
        {
            await _apiRequestController.Delete($"{UrlPaths.Labels}/{labelId}");
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
        /// Delete Webhooks using indicated Callback URL (WARNING: THERE IS NO WAY GOING BACK!!!).
        /// </summary>
        /// <param name="callbackUrl">The URL of the callback URL</param>
        public async Task DeleteWebhooksByCallbackUrlAsync(string callbackUrl)
        {
            var currentWebhooks = await GetWebhooksForCurrentTokenAsync();
            foreach (var webhook in currentWebhooks.Where(x => x.CallbackUrl == callbackUrl))
            {
                await DeleteWebhookAsync(webhook.Id);
            }
        }

        /// <summary>
        /// Delete Webhooks using indicated target ModelId (WARNING: THERE IS NO WAY GOING BACK!!!).
        /// </summary>
        /// <param name="targetIdModel">The Target Model Id (example an ID of a Board)</param>
        public async Task DeleteWebhooksByTargetModelIdAsync(string targetIdModel)
        {
            var currentWebhooks = await GetWebhooksForCurrentTokenAsync();
            foreach (var webhook in currentWebhooks.Where(x => x.IdOfTypeYouWishToMonitor == targetIdModel))
            {
                await DeleteWebhookAsync(webhook.Id);
            }
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
        /// Delete a Comment (WARNING: THERE IS NO WAY GOING BACK!!!).
        /// </summary>
        /// <param name="commentActionId">Id of Comment Action Id</param>
        public async Task DeleteCommentActionAsync(string commentActionId)
        {
            await _apiRequestController.Delete($"{UrlPaths.Actions}/{commentActionId}");
        }

        /// <summary>
        /// Custom Delete Method to be used on unexposed features of the API.
        /// </summary>
        /// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        public async Task DeleteAsync(string urlSuffix)
        {
            await _apiRequestController.Delete(urlSuffix);
        }

        /// <summary>
        /// Delete a Checklist (WARNING: THERE IS NO WAY GOING BACK!!!).
        /// </summary>
        /// <param name="checklistId">The id of the Checklist to Delete</param>
        public async Task DeleteChecklistAsync(string checklistId)
        {
            await _apiRequestController.Delete($"{UrlPaths.Checklists}/{checklistId}");
        }

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
            return await _apiRequestController.Get<Card>($"{UrlPaths.Cards}/{cardId}", 
                new QueryParameter(@"customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter(@"attachments", Options.IncludeAttachmentsInCardGetMethods)
                );
        }

        /// <summary>
        /// Get Custom Fields for a Card
        /// </summary>
        /// <remarks>Tip: Use Extension methods GetCustomFieldValueAsXYZ for a handy way to get values</remarks>
        /// <param name="cardId">Id of the Card</param> 
        /// <returns>The Custom Fields</returns>
        public async Task<List<CustomFieldItem>> GetCustomFieldItemsForCardAsync(string cardId)
        {
            return await _apiRequestController.Get<List<CustomFieldItem>>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.CustomFieldItems}");
        }

        /// <summary>
        /// Get all open cards on un-archived lists
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/", 
                new QueryParameter(@"customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter(@"attachments", Options.IncludeAttachmentsInCardGetMethods));
        }

        /// <summary>
        /// Get Custom Fields of a Board
        /// </summary>
        /// <param name="boardId">Id of the Board (long version)</param>
        /// <returns>List of CustomFields</returns>
        public async Task<List<CustomField>> GetCustomFieldsOnBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<CustomField>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.CustomFields}");
        }

        /// <summary>
        /// Get all open cards on a specific list
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsInListAsync(string listId)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Lists}/{listId}/{UrlPaths.Cards}/", 
                new QueryParameter(@"customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter(@"attachments", Options.IncludeAttachmentsInCardGetMethods));
        }

        /// <summary>
        /// The cards on board based on their status regardless if they are on archived lists
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">The Selected Filter</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardFilteredAsync(string boardId, CardsFilter filter)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/{filter.GetJsonPropertyName()}", 
                new QueryParameter(@"customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter(@"attachments", Options.IncludeAttachmentsInCardGetMethods));
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

        /// <summary>
        /// Get All Comments on a Card
        /// </summary>
        /// <param name="cardId">Id of Card</param>
        /// <returns>List of Comments</returns>
        public async Task<List<TrelloAction>> GetAllCommentsOnCardAsync(string cardId)
        {
            var result = new List<TrelloAction>();
            int page = 0;
            bool moreComments = true;
            do
            {
                var comments = await GetPagedCommentsOnCardAsync(cardId, page);
                page++;
                if (comments.Any())
                {
                    result.AddRange(comments);
                }
                else
                {
                    moreComments = false;
                }
            } while (moreComments);
            return result;
        }

        /// <summary>
        /// Get Paged Comments on a Card (Note: this method can max return up to 50 comments. For more use the page parameter [note: the API can't give you back how many there are in total so you need to try until non is returned])
        /// </summary>
        /// <param name="cardId">Id of Card</param>
        /// <param name="page">The page of results for actions. Each page of results has 50 actions. (Default: 0, Maximum: 19)</param>
        /// <returns>List of Comments</returns>
        public async Task<List<TrelloAction>> GetPagedCommentsOnCardAsync(string cardId, int page = 0)
        {
            return await _apiRequestController.Get<List<TrelloAction>>($"{UrlPaths.Cards}/{cardId}/actions",
                new QueryParameter(@"filter", @"commentCard"),
                new QueryParameter(@"page", page));
        }

        /// <summary>
        /// Get information about the token used by this TrelloClient
        /// </summary>
        /// <returns>Information about the Token</returns>
        public async Task<TokenInformation> GetTokenInformationAsync()
        {
            return await _apiRequestController.Get<TokenInformation>($"{UrlPaths.Tokens}/{_apiRequestController.Token}");
        }

        /// <summary>
        /// Get information about the Member that own the token used by this TrelloClient
        /// </summary>
        /// <returns>The Member</returns>
        public async Task<Member> GetTokenMemberAsync()
        {
            return await _apiRequestController.Get<Member>($"{UrlPaths.Tokens}/{_apiRequestController.Token}/member");
        }

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

        /// <summary>
        /// Remove a cover from a Card
        /// </summary>
        /// <param name="cardId">Id of Card</param>
        /// <returns>The Card with the removed Cover</returns>
        public async Task<Card> RemoveCoverFromCardAsync(string cardId)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", new QueryParameter(@"cover", string.Empty));
        }

        /// <summary>
        /// Replace callback URL for one or more Webhooks
        /// </summary>
        /// <param name="oldUrl">The old callback URL to find</param>
        /// <param name="newUrl">The new callback URL to replace it with</param>
        public async Task UpdateWebhookByCallbackUrlAsync(string oldUrl, string newUrl)
        {
            var currentWebhooks = await GetWebhooksForCurrentTokenAsync();
            foreach (var webhook in currentWebhooks.Where(x => x.CallbackUrl == oldUrl))
            {
                webhook.CallbackUrl = newUrl;
                await UpdateWebhookAsync(webhook);
            }
        }

        /// <summary>
        /// Update the definition of a label (Name and Color)
        /// </summary>
        /// <param name="labelWithUpdates">The label with updates</param>
        public async Task<Label> UpdateLabelAsync(Label labelWithUpdates)
        {
            return await _apiRequestController.Put<Label>($"{UrlPaths.Labels}/{labelWithUpdates.Id}", _queryParametersBuilder.GetViaQueryParameterAttributes(labelWithUpdates));
        }

        /// <summary>
        /// Get Attachments on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The Attachments</returns>
        public async Task<List<Attachment>> GetAttachmentsOnCardAsync(string cardId)
        {
            return await _apiRequestController.Get<List<Attachment>>($"{UrlPaths.Cards}/{cardId}/attachments");
        }

        /// <summary>
        /// Delete an Attachments on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="attachmentId">Id of Attachment</param>
        public async Task DeleteAttachmentOnCardAsync(string cardId, string attachmentId)
        {
            await _apiRequestController.Delete($"{UrlPaths.Cards}/{cardId}/attachments/{attachmentId}");
        }

        /// <summary>
        /// Add an Attachment to a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="attachmentUrlLink">A Link Attachment</param>
        /// <returns>The Created Attachment</returns>
        public async Task<Attachment> AddAttachmentToCardAsync(string cardId, AttachmentUrlLink attachmentUrlLink)
        {
            var parameters = new List<QueryParameter> { new QueryParameter("url", attachmentUrlLink.Url) };
            if (!string.IsNullOrWhiteSpace(attachmentUrlLink.Name))
            {
                parameters.Add(new QueryParameter("name", attachmentUrlLink.Name));
            }
            return await _apiRequestController.Post<Attachment>($"{UrlPaths.Cards}/{cardId}/attachments", parameters.ToArray());
        }

        /// <summary>
        /// Add an Attachment to a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="attachmentFileUpload">A Link Attachment</param>
        /// <param name="setAsCover">Make this attachment the cover of the Card</param>
        /// <returns>The Created Attachment</returns>
        public async Task<Attachment> AddAttachmentToCardAsync(string cardId, AttachmentFileUpload attachmentFileUpload, bool setAsCover = false)
        {
            var parameters = new List<QueryParameter>();
            if (!string.IsNullOrWhiteSpace(attachmentFileUpload.Name))
            {
                parameters.Add(new QueryParameter("name", attachmentFileUpload.Name));
            }
            if (setAsCover)
            {
                parameters.Add(new QueryParameter("setCover", "true"));
            }
            return await _apiRequestController.PostWithAttachmentFileUpload<Attachment>($"{UrlPaths.Cards}/{cardId}/attachments", attachmentFileUpload, parameters.ToArray());
        }

        /// <summary>
        /// Add a new label to the Board (Not to be confused with 'AddLabelsToCardAsync' that assign labels to cards)
        /// </summary>
        /// <param name="label">Definition of the new label</param>
        /// <returns>The newly created label</returns>
        public async Task<Label> AddLabelAsync(Label label)
        {
            return await _apiRequestController.Post<Label>($"{UrlPaths.Labels}", _queryParametersBuilder.GetViaQueryParameterAttributes(label));
        }

        /// <summary>
        /// Get the Boards that the specified member have access to
        /// </summary>
        /// <param name="memberId">Id of the Member to find boards for</param>
        /// <returns>The Active Boards there is access to</returns>
        public async Task<List<Board>> GetBoardsForMemberAsync(string memberId)
        {
            return await _apiRequestController.Get<List<Board>>($"{UrlPaths.Members}/{memberId}/boards");
        }

        /// <summary>
        /// Get the Boards that the token provided to the TrelloClient can Access
        /// </summary>
        /// <returns>The Active Boards there is access to</returns>
        public async Task<List<Board>> GetBoardsCurrentTokenCanAccessAsync()
        {
            var tokenMember = await GetTokenMemberAsync();
            return await GetBoardsForMemberAsync(tokenMember.Id);
        }
    }
}