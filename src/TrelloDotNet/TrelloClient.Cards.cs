using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.MoveCardToBoardOptions;
using TrelloDotNet.Model.Options.MoveCardToListOptions;

// ReSharper disable UnusedMember.Global

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
            QueryParameter[] parameters = _queryParametersBuilder.GetViaQueryParameterAttributes(card);
            _queryParametersBuilder.AdjustForNamedPosition(parameters, card.NamedPosition);
            var result = await _apiRequestController.Post<Card>($"{UrlPaths.Cards}", cancellationToken, parameters);
            if (card.Cover != null)
            {
                return await AddCoverToCardAsync(result.Id, card.Cover, cancellationToken);
            }

            return result;
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
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, new QueryParameter("closed", false));
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
            CardCover cardCover = cardWithChanges.Cover;
            _queryParametersBuilder.AdjustForNamedPosition(parameters, cardWithChanges.NamedPosition);
            var payload = GeneratePayloadForCoverUpdate(cardCover, parameters);
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
                new QueryParameter("idBoard", newList.BoardId),
                new QueryParameter("idList", newListId)
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
            return await _apiRequestController.Get<Card>(GetUrlBuilder.GetCard(cardId), cancellationToken,
#pragma warning disable CS0618 // Type or member is obsolete
                new QueryParameter("customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter("attachments", Options.IncludeAttachmentsInCardGetMethods)
#pragma warning restore CS0618 // Type or member is obsolete
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
            return await _apiRequestController.Get<Card>(GetUrlBuilder.GetCard(cardId), cancellationToken, options.GetParameters(false));
        }

        /// <summary>
        /// Get all open cards on un-archived lists on a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsOnBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>(GetUrlBuilder.GetCardsOnBoard(boardId), cancellationToken,
#pragma warning disable CS0618 // Type or member is obsolete
                new QueryParameter("customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter("attachments", Options.IncludeAttachmentsInCardGetMethods)
#pragma warning restore CS0618 // Type or member is obsolete
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
            if (options.IncludeList)
            {
                if (options.CardFields != null && !options.CardFields.Fields.Contains("idList"))
                {
                    options.CardFields.Fields = options.CardFields.Fields.Union(new List<string>
                    {
                        "idList"
                    }).ToArray();
                }
            }

            if (options.IncludeBoard)
            {
                if (options.CardFields != null && !options.CardFields.Fields.Contains("idBoard"))
                {
                    options.CardFields.Fields = options.CardFields.Fields.Union(new List<string>
                    {
                        "idBoard"
                    }).ToArray();
                }
            }

            var cards = await _apiRequestController.Get<List<Card>>(GetUrlBuilder.GetCardsOnBoard(boardId), cancellationToken, options.GetParameters(true));
            if (options.IncludeList)
            {
                var lists = await GetListsOnBoardAsync(boardId, cancellationToken);
                foreach (Card card in cards)
                {
                    card.List = lists.FirstOrDefault(x => x.Id == card.ListId);
                }
            }

            if (options.IncludeBoard)
            {
                var board = await GetBoardAsync(boardId, cancellationToken);
                foreach (Card card in cards)
                {
                    card.Board = board;
                }
            }

            return cards;
        }

        /// <summary>
        /// Get all open cards on a specific list
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Cards</returns>
        public async Task<List<Card>> GetCardsInListAsync(string listId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>(GetUrlBuilder.GetCardsInList(listId), cancellationToken,
#pragma warning disable CS0618 // Type or member is obsolete
                new QueryParameter("customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter("attachments", Options.IncludeAttachmentsInCardGetMethods));
#pragma warning restore CS0618 // Type or member is obsolete
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
            if (options.IncludeBoard)
            {
                if (options.CardFields != null && !options.CardFields.Fields.Contains("idBoard"))
                {
                    options.CardFields.Fields = options.CardFields.Fields.Union(new List<string>
                    {
                        "idBoard"
                    }).ToArray();
                }
            }

            var cards = await _apiRequestController.Get<List<Card>>(GetUrlBuilder.GetCardsInList(listId), cancellationToken, options.GetParameters(true));
            if (options.IncludeList)
            {
                var list = await GetListAsync(listId, cancellationToken);
                foreach (Card card in cards)
                {
                    card.ListId = listId;
                    card.List = list;
                }
            }

            if (options.IncludeBoard && cards.Count > 0)
            {
                var board = await GetBoardAsync(cards[0].BoardId, cancellationToken);
                foreach (Card card in cards)
                {
                    card.Board = board;
                }
            }


            return cards;
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
            return await _apiRequestController.Get<List<Card>>($"{GetUrlBuilder.GetCardsOnBoard(boardId)}/{filter.GetJsonPropertyName()}", cancellationToken,
#pragma warning disable CS0618 // Type or member is obsolete
                new QueryParameter("customFieldItems", Options.IncludeCustomFieldsInCardGetMethods),
                new QueryParameter("attachments", Options.IncludeAttachmentsInCardGetMethods));
#pragma warning restore CS0618 // Type or member is obsolete
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
            if (options.IncludeList)
            {
                if (options.CardFields != null && !options.CardFields.Fields.Contains("idList"))
                {
                    options.CardFields.Fields = options.CardFields.Fields.Union(new List<string>
                    {
                        "idList"
                    }).ToArray();
                }
            }

            var cards = await _apiRequestController.Get<List<Card>>($"{GetUrlBuilder.GetCardsOnBoard(boardId)}/{filter.GetJsonPropertyName()}", cancellationToken, options.GetParameters(true));
            if (options.IncludeList)
            {
                List<List> lists;
                switch (filter)
                {
                    case CardsFilter.All:
                    case CardsFilter.Closed:
                    case CardsFilter.Visible:
                        lists = await GetListsOnBoardFilteredAsync(boardId, ListFilter.All, cancellationToken);
                        break;
                    case CardsFilter.Open:
                        lists = await GetListsOnBoardAsync(boardId, cancellationToken);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(filter), filter, null);
                }

                foreach (Card card in cards)
                {
                    card.List = lists.FirstOrDefault(x => x.Id == card.ListId);
                }
            }

            return cards;
        }

        /// <summary>
        /// Get all Cards a Member is on (across multiple boards)
        /// </summary>
        /// <param name="memberId">Id of Member</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<List<Card>> GetCardsForMemberAsync(string memberId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Card>>(GetUrlBuilder.GetCardsForMember(memberId), cancellationToken);
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
            if (options.IncludeList)
            {
                if (options.CardFields != null && !options.CardFields.Fields.Contains("idList"))
                {
                    options.CardFields.Fields = options.CardFields.Fields.Union(new List<string>
                    {
                        "idList"
                    }).ToArray();
                }

                if (options.CardFields != null && !options.CardFields.Fields.Contains("idBoard"))
                {
                    options.CardFields.Fields = options.CardFields.Fields.Union(new List<string>
                    {
                        "idBoard"
                    }).ToArray();
                }
            }

            if (options.IncludeBoard)
            {
                if (options.CardFields != null && !options.CardFields.Fields.Contains("idBoard"))
                {
                    options.CardFields.Fields = options.CardFields.Fields.Union(new List<string>
                    {
                        "idBoard"
                    }).ToArray();
                }
            }

            var cards = await _apiRequestController.Get<List<Card>>(GetUrlBuilder.GetCardsForMember(memberId), cancellationToken, options.GetParameters(true));
            if (options.IncludeList)
            {
                var boardsToGetListsFor = cards.Select(x => x.BoardId).Distinct().ToArray();
                List<List> lists = new List<List>();
                foreach (var boardId in boardsToGetListsFor)
                {
                    lists.AddRange(await GetListsOnBoardAsync(boardId, cancellationToken));
                }

                foreach (Card card in cards)
                {
                    card.List = lists.FirstOrDefault(x => x.Id == card.ListId);
                }
            }

            if (options.IncludeBoard)
            {
                var boardIds = cards.Select(x => x.BoardId).Distinct().ToList();
                var boards = await GetBoardsAsync(boardIds, cancellationToken);
                foreach (Card card in cards)
                {
                    card.Board = boards.FirstOrDefault(x => x.Id == card.BoardId);
                }
            }

            return cards;
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
            return await UpdateCardAsync(cardId, new List<QueryParameter>
            {
                new QueryParameter(CardFieldsType.Due.GetJsonPropertyName(), dueDate),
                new QueryParameter(CardFieldsType.DueComplete.GetJsonPropertyName(), dueComplete)
            }, cancellationToken);
        }

        /// <summary>
        /// Set Due Date on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="startDate">The Start Date (In UTC Time)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task<Card> SetStartDateOnCardAsync(string cardId, DateTimeOffset startDate, CancellationToken cancellationToken = default)
        {
            return await UpdateCardAsync(cardId, new List<QueryParameter>
            {
                new QueryParameter(CardFieldsType.Start.GetJsonPropertyName(), startDate)
            }, cancellationToken);
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
            return await UpdateCardAsync(cardId, new List<QueryParameter>
            {
                new QueryParameter(CardFieldsType.Start.GetJsonPropertyName(), startDate),
                new QueryParameter(CardFieldsType.Due.GetJsonPropertyName(), dueDate),
                new QueryParameter(CardFieldsType.DueComplete.GetJsonPropertyName(), dueComplete),
            }, cancellationToken);
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
            return await UpdateCardAsync(cardId, new List<QueryParameter>
            {
                new QueryParameter(CardFieldsType.ListId.GetJsonPropertyName(), newListId)
            }, cancellationToken);
        }

        /// <summary>
        /// Move a Card to a new list on the same board
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="newListId">Id of the List you wish to move it to</param>
        /// <param name="options">Additional optional Options for the Move</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<Card> MoveCardToListAsync(string cardId, string newListId, MoveCardToListOptions options, CancellationToken cancellationToken = default)
        {
            var parameters = new List<QueryParameter> { new QueryParameter(CardFieldsType.ListId.GetJsonPropertyName(), newListId) };
            if (options.NamedPositionOnNewList.HasValue)
            {
                switch (options.NamedPositionOnNewList.Value)
                {
                    case NamedPosition.Top:
                        parameters.Add(new QueryParameter("pos", "top"));
                        break;
                    case NamedPosition.Bottom:
                        parameters.Add(new QueryParameter("pos", "bottom"));
                        break;
                }
            }
            else if (options.PositionOnNewList.HasValue)
            {
                parameters.Add(new QueryParameter(CardFieldsType.Position.GetJsonPropertyName(), options.PositionOnNewList.Value));
            }

            return await UpdateCardAsync(cardId, parameters, cancellationToken);
        }

        /// <summary>
        /// Update one or more specific fields on a card (compared to a full update of all fields with UpdateCard)
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="parameters">The Specific Parameters to set</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task<Card> UpdateCardAsync(string cardId, List<QueryParameter> parameters, CancellationToken cancellationToken = default)
        {
            QueryParameter coverParameter = parameters.FirstOrDefault(x => x.Name == "cover");
            if (coverParameter != null && !string.IsNullOrWhiteSpace(coverParameter.GetRawStringValue()))
            {
                parameters.Remove(coverParameter);
                CardCover cover = JsonSerializer.Deserialize<CardCover>(coverParameter.GetRawStringValue());
                var payload = GeneratePayloadForCoverUpdate(cover, parameters);
                return await _apiRequestController.PutWithJsonPayload<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, payload, parameters.ToArray());
            }

            //Special Cover Card
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, parameters.ToArray());
        }

        /// <summary>
        /// Update one or more specific fields on a card (compared to a full update of all fields with UpdateCard)
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="valuesToUpdate">The Specific values to set</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task<Card> UpdateCardAsync(string cardId, List<CardUpdate> valuesToUpdate, CancellationToken cancellationToken = default)
        {
            return await UpdateCardAsync(cardId, valuesToUpdate.Select(x => x.ToQueryParameter()).ToList(), cancellationToken);
        }

        /// <summary>
        /// Move the Card to the top of its current list
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Card</returns>
        public async Task<Card> MoveCardToTopOfCurrentListAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await UpdateCardAsync(cardId, new List<QueryParameter>
            {
                new QueryParameter(CardFieldsType.Position.GetJsonPropertyName(), "top")
            }, cancellationToken);
        }

        /// <summary>
        /// Move the Card to the bottom of its current list
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Card</returns>
        public async Task<Card> MoveCardToBottomOfCurrentListAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await UpdateCardAsync(cardId, new List<QueryParameter>
            {
                new QueryParameter(CardFieldsType.Position.GetJsonPropertyName(), "bottom")
            }, cancellationToken);
        }

        /// <summary>
        /// Move a Card to another board
        /// </summary>
        /// <param name="cardId">The Id of the Card to Move</param>
        /// <param name="newBoardId">The ID of the New Board that the card should be moved to</param>
        /// <param name="options">Additional Options for the move like what list the card should end up on the new board and what happens to labels and members</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<Card> MoveCardToBoard(string cardId, string newBoardId, MoveCardToBoardOptions options, CancellationToken cancellationToken = default)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "You need to pass an options object to confirm the various options that are invovled with moving a card between boards");
            }

            List<QueryParameter> parameters = new List<QueryParameter> { new QueryParameter(CardFieldsType.BoardId.GetJsonPropertyName(), newBoardId) };
            var newListId = options.NewListId;
            if (string.IsNullOrWhiteSpace(newListId))
            {
                //No list specified, so we need to find the first list on the board
                newListId = (await GetListsOnBoardAsync(newBoardId, cancellationToken)).OrderBy(x => x.Position).FirstOrDefault()?.Id;
            }

            parameters.Add(new QueryParameter(CardFieldsType.ListId.GetJsonPropertyName(), newListId));

            if (options.NamedPositionOnNewList.HasValue)
            {
                switch (options.NamedPositionOnNewList.Value)
                {
                    case NamedPosition.Top:
                        parameters.Add(new QueryParameter("pos", "top"));
                        break;
                    case NamedPosition.Bottom:
                        parameters.Add(new QueryParameter("pos", "bottom"));
                        break;
                }
            }
            else if (options.PositionOnNewList.HasValue)
            {
                parameters.Add(new QueryParameter(CardFieldsType.Position.GetJsonPropertyName(), options.PositionOnNewList.Value));
            }

            Card card = await GetCardAsync(cardId, new GetCardOptions
            {
                CardFields = new CardFields(CardFieldsType.MemberIds, CardFieldsType.LabelIds, CardFieldsType.Labels)
            }, cancellationToken);

            switch (options.MemberOptions)
            {
                case MoveCardToBoardOptionsMemberOptions.KeepMembersAlsoOnNewBoardAndRemoveRest:
                    var existingMemberIdsOnNewBoard = (await GetMembersOfBoardAsync(newBoardId, cancellationToken)).Select(x => x.Id);
                    card.MemberIds = card.MemberIds.Intersect(existingMemberIdsOnNewBoard).ToList();
                    break;
                case MoveCardToBoardOptionsMemberOptions.RemoveAllMembersOnCard:
                    card.MemberIds.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (card.LabelIds.Any())
            {
                card.LabelIds.Clear();
                switch (options.LabelOptions)
                {
                    case MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndColorAndCreateMissing:
                    {
                        var existingLabels = await GetLabelsOfBoardAsync(newBoardId, cancellationToken);
                        foreach (Label cardLabel in card.Labels)
                        {
                            Label existingLabel = existingLabels.FirstOrDefault(x => x.Name == cardLabel.Name && x.Color == cardLabel.Color);
                            if (existingLabel != null)
                            {
                                card.LabelIds.Add(existingLabel.Id);
                            }
                            else
                            {
                                //Label need to be added
                                Label newLabel = await AddLabelAsync(new Label(newBoardId, cardLabel.Name, cardLabel.Color), cancellationToken);
                                card.LabelIds.Add(newLabel.Id);
                            }
                        }

                        break;
                    }
                    case MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndColorAndRemoveMissing:
                    {
                        var existingLabels = await GetLabelsOfBoardAsync(newBoardId, cancellationToken);
                        foreach (Label cardLabel in card.Labels)
                        {
                            Label existingLabel = existingLabels.FirstOrDefault(x => x.Name == cardLabel.Name && x.Color == cardLabel.Color);
                            if (existingLabel != null)
                            {
                                card.LabelIds.Add(existingLabel.Id);
                            }
                        }

                        break;
                    }
                    case MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndCreateMissing:
                    {
                        var existingLabels = await GetLabelsOfBoardAsync(newBoardId, cancellationToken);
                        foreach (Label cardLabel in card.Labels)
                        {
                            Label existingLabel = existingLabels.FirstOrDefault(x => x.Name == cardLabel.Name);
                            if (existingLabel != null)
                            {
                                card.LabelIds.Add(existingLabel.Id);
                            }
                            else
                            {
                                //Label need to be added
                                Label newLabel = await AddLabelAsync(new Label(newBoardId, cardLabel.Name, cardLabel.Color), cancellationToken);
                                card.LabelIds.Add(newLabel.Id);
                            }
                        }

                        break;
                    }
                    case MoveCardToBoardOptionsLabelOptions.MigrateToLabelsOfSameNameAndRemoveMissing:
                    {
                        var existingLabels = await GetLabelsOfBoardAsync(newBoardId, cancellationToken);
                        foreach (Label cardLabel in card.Labels)
                        {
                            Label existingLabel = existingLabels.FirstOrDefault(x => x.Name == cardLabel.Name);
                            if (existingLabel != null)
                            {
                                card.LabelIds.Add(existingLabel.Id);
                            }
                        }

                        break;
                    }
                    case MoveCardToBoardOptionsLabelOptions.RemoveAllLabelsOnCard:
                        //No more Work needed
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            parameters.Add(new QueryParameter(CardFieldsType.LabelIds.GetJsonPropertyName(), card.LabelIds));
            parameters.Add(new QueryParameter(CardFieldsType.MemberIds.GetJsonPropertyName(), card.MemberIds));

            if (options.RemoveDueDate)
            {
                parameters.Add(new QueryParameter(CardFieldsType.Due.GetJsonPropertyName(), (DateTimeOffset?)null));
            }

            if (options.RemoveStartDate)
            {
                parameters.Add(new QueryParameter(CardFieldsType.Start.GetJsonPropertyName(), (DateTimeOffset?)null));
            }

            return await UpdateCardAsync(cardId, parameters, cancellationToken);
        }

        private static string GeneratePayloadForCoverUpdate(CardCover cardCover, List<QueryParameter> parameters)
        {
            //Special code for Cover
            string payload = string.Empty;
            if (cardCover == null)
            {
                //Remove cover
                parameters.Add(new QueryParameter("cover", ""));
            }
            else
            {
                cardCover.PrepareForAddUpdate();
                if (cardCover.Color != null || cardCover.BackgroundImageId != null)
                {
                    QueryParameter queryParameter = parameters.FirstOrDefault(x => x.Name == "idAttachmentCover");
                    if (queryParameter != null)
                    {
                        parameters.Remove(queryParameter); //This parameter can't be there while a cover is added
                    }
                }

                payload = $"{{\"cover\":{JsonSerializer.Serialize(cardCover)}}}";
            }

            return payload;
        }
    }
}