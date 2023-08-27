using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Add a Card
        /// </summary>
        /// <param name="card">The Card to Add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Added Card</returns>
        public async Task<Card> AddCardAsync(Card card, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Post<Card>($"{UrlPaths.Cards}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(card));
        }

        /// <summary>
        /// Archive (Close) a Card
        /// </summary>
        /// <param name="cardId">The id of card that should be archived</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Archived Card</returns>
        public async Task<Card> ArchiveCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, new QueryParameter("closed", true));
        }

        /// <summary>
        /// ReOpen (Send back to board) a Card
        /// </summary>
        /// <param name="cardId">The id of card that should be reopened</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The ReOpened Card</returns>
        public async Task<Card> ReOpenCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, new QueryParameter(@"closed", false));
        }

        /// <summary>
        /// Update a Card
        /// </summary>
        /// <param name="cardWithChanges">The card with the changes</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Updated Card</returns>
        public async Task<Card> UpdateCardAsync(Card cardWithChanges, CancellationToken cancellationToken = default)
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

            return await _apiRequestController.PutWithJsonPayload<Card>($"{UrlPaths.Cards}/{cardWithChanges.Id}", cancellationToken, payload, parameters.ToArray());
        }

        /// <summary>
        /// Archive all cards on in a List
        /// </summary>
        /// <param name="listId">The id of the List that should have its cards archived</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task ArchiveAllCardsInListAsync(string listId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Post<List>($"{UrlPaths.Lists}/{listId}/archiveAllCards", cancellationToken);
        }

        /// <summary>
        /// Move all cards of a list to another list
        /// </summary>
        /// <param name="currentListId">The id of the List that should have its cards moved</param>
        /// <param name="newListId">The id of the new List that should receive the cards</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task MoveAllCardsInListAsync(string currentListId, string newListId, CancellationToken cancellationToken = default)
        {
            var newList = await GetListAsync(newListId, cancellationToken); //Get the new list's BoardId so the user do not need to provide it.
            await _apiRequestController.Post($"{UrlPaths.Lists}/{currentListId}/moveAllCards", cancellationToken,
                0,
                new QueryParameter(@"idBoard", newList.BoardId),
                new QueryParameter(@"idList", newListId)
            );
        }

        /// <summary>
        /// Delete a Card (WARNING: THERE IS NO WAY GOING BACK!!!). Alternative use ArchiveCardAsync() for non-permanency
        /// </summary>
        /// <param name="cardId">The id of the Card to Delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete($"{UrlPaths.Cards}/{cardId}", cancellationToken, 0);
        }

        /// <summary>
        /// Get a Card by its Id
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Card</returns>
        public async Task<Card> GetCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken,
                new QueryParameter(@"customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter(@"attachments", Options.IncludeAttachmentsInCardGetMethods)
            );
        }

        /// <summary>
        /// Get a Card by its Id
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="options">Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Card</returns>
        public async Task<Card> GetCardAsync(string cardId, GetCardOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Get all open cards on un-archived lists on a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/", cancellationToken,
                new QueryParameter("customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter("attachments", Options.IncludeAttachmentsInCardGetMethods)
                );
        }

        /// <summary>
        /// Get all open cards on un-archived lists on a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="options">Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardAsync(string boardId, GetCardOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/", cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Get all open cards on a specific list
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsInListAsync(string listId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Lists}/{listId}/{UrlPaths.Cards}/", cancellationToken,
                new QueryParameter(@"customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter(@"attachments", Options.IncludeAttachmentsInCardGetMethods));
        }

        /// <summary>
        /// Get all open cards on a specific list
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="options">Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsInListAsync(string listId, GetCardOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Lists}/{listId}/{UrlPaths.Cards}/", cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Get the cards on board based on their status regardless if they are on archived lists
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">The Selected Filter</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardFilteredAsync(string boardId, CardsFilter filter, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/{filter.GetJsonPropertyName()}", cancellationToken,
                new QueryParameter(@"customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter(@"attachments", Options.IncludeAttachmentsInCardGetMethods));
        }

        /// <summary>
        /// Get the cards on board based on their status regardless if they are on archived lists
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="filter">The Selected Filter</param>
        /// <param name="options">Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)</param> 
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardFilteredAsync(string boardId, CardsFilter filter, GetCardOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/{filter.GetJsonPropertyName()}", cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Get all Cards a Member is on (across multiple boards)
        /// </summary>
        /// <param name="memberId">Id of Member</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<List<Card>> GetCardsForMemberAsync(string memberId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Members}/{memberId}/{UrlPaths.Cards}", cancellationToken);
        }

        /// <summary>
        /// Get all Cards a Member is on (across multiple boards)
        /// </summary>
        /// <param name="memberId">Id of Member</param>
        /// <param name="options">Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<List<Card>> GetCardsForMemberAsync(string memberId, GetCardOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>($"{UrlPaths.Members}/{memberId}/{UrlPaths.Cards}", cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Set Due Date on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="dueDate">The Due Date (In UTC Time)</param>
        /// <param name="dueComplete">If Due is complete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task<Card> SetDueDateOnCardAsync(string cardId, DateTimeOffset dueDate, bool dueComplete = false, CancellationToken cancellationToken = default)
        {
            var card = await GetCardAsync(cardId, cancellationToken);
            card.Due = dueDate;
            card.DueComplete = dueComplete;
            return await UpdateCardAsync(card, cancellationToken);
        }

        /// <summary>
        /// Set Due Date on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="startDate">The Start Date (In UTC Time)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task<Card> SetStartDateOnCardAsync(string cardId, DateTimeOffset startDate, CancellationToken cancellationToken = default)
        {
            var card = await GetCardAsync(cardId, cancellationToken);
            card.Start = startDate;
            return await UpdateCardAsync(card, cancellationToken);
        }

        /// <summary>
        /// Set Start and Due Date on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="startDate">The Start Date (In UTC Time)</param>
        /// <param name="dueDate">The Due Date (In UTC Time)</param>
        /// <param name="dueComplete">If Due is complete</param>
        /// <param name="cancellationToken">Cancellation Token</param> 
        public async Task<Card> SetStartDateAndDueDateOnCardAsync(string cardId, DateTimeOffset startDate, DateTimeOffset dueDate, bool dueComplete = false, CancellationToken cancellationToken = default)
        {
            var card = await GetCardAsync(cardId, cancellationToken);
            card.Start = startDate;
            card.Due = dueDate;
            card.DueComplete = dueComplete;
            return await UpdateCardAsync(card, cancellationToken);
        }

        /// <summary>
        /// Move a Card to a new list on the same board
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="newListId">Id of the List you wish to move it to</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<Card> MoveCardToListAsync(string cardId, string newListId, CancellationToken cancellationToken = default)
        {
            Card card = await GetCardAsync(cardId, cancellationToken);
            card.ListId = newListId;
            return await UpdateCardAsync(card, cancellationToken);
        }
    }
}