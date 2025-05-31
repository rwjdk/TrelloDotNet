using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.GetLabelOptions;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Deletes a label from the board and removes it from all cards it was assigned to. This operation is irreversible. If you want to remove a label from a single card, use 'RemoveLabelsFromCardAsync' or 'RemoveAllLabelsFromCardAsync'.
        /// </summary>
        /// <param name="labelId">The ID of the label to delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteLabelAsync(string labelId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete($"{UrlPaths.Labels}/{labelId}", cancellationToken, 0);
        }

        /// <summary>
        /// Retrieves all labels defined for a specific board.
        /// </summary>
        /// <param name="boardId">ID of the board (long or short version)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of labels on the board</returns>
        public async Task<List<Label>> GetLabelsOfBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Label>>(GetUrlBuilder.GetLabelsOfBoard(boardId), cancellationToken);
        }

        /// <summary>
        /// Retrieves all labels defined for a specific board, with additional options for filtering and selection.
        /// </summary>
        /// <param name="boardId">ID of the board (long or short version)</param>
        /// <param name="options">Options for filtering and selecting labels</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of labels on the board</returns>
        public async Task<List<Label>> GetLabelsOfBoardAsync(string boardId, GetLabelOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Label>>(GetUrlBuilder.GetLabelsOfBoard(boardId), cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Adds one or more labels to a card by their IDs.
        /// </summary>
        /// <param name="cardId">ID of the card to add labels to</param>
        /// <param name="labelIdsToAdd">One or more IDs of labels to add</param>
        /// <returns>The updated card with the added labels</returns>
        public async Task<Card> AddLabelsToCardAsync(string cardId, params string[] labelIdsToAdd)
        {
            return await AddLabelsToCardAsync(cardId, CancellationToken.None, labelIdsToAdd.Distinct().ToArray());
        }

        /// <summary>
        /// Adds one or more labels to a card by their IDs.
        /// </summary>
        /// <param name="cardId">ID of the card to add labels to</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="labelIdsToAdd">One or more IDs of labels to add</param>
        /// <returns>The updated card with the added labels</returns>
        public async Task<Card> AddLabelsToCardAsync(string cardId, CancellationToken cancellationToken = default, params string[] labelIdsToAdd)
        {
            var card = await GetCardAsync(cardId, cancellationToken);
            var missing = labelIdsToAdd.Where(x => !card.LabelIds.Contains(x)).ToList();

            if (missing.Count == 0)
            {
                return card; //All already There
            }

            //Need update
            card.LabelIds.AddRange(missing);
            return await UpdateCardAsync(cardId, new List<CardUpdate>
            {
                CardUpdate.Labels(card.LabelIds.Distinct().ToList())
            }, cancellationToken);
        }

        /// <summary>
        /// Removes one or more labels from a card by their IDs.
        /// </summary>
        /// <param name="cardId">ID of the card to remove labels from</param>
        /// <param name="labelIdsToRemove">One or more IDs of labels to remove</param>
        /// <returns>The updated card with the labels removed</returns>
        public async Task<Card> RemoveLabelsFromCardAsync(string cardId, params string[] labelIdsToRemove)
        {
            return await RemoveLabelsFromCardAsync(cardId, CancellationToken.None, labelIdsToRemove);
        }

        /// <summary>
        /// Removes one or more labels from a card by their IDs.
        /// </summary>
        /// <param name="cardId">ID of the card to remove labels from</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="labelIdsToRemove">One or more IDs of labels to remove</param>
        /// <returns>The updated card with the labels removed</returns>
        public async Task<Card> RemoveLabelsFromCardAsync(string cardId, CancellationToken cancellationToken, params string[] labelIdsToRemove)
        {
            var card = await GetCardAsync(cardId, cancellationToken);
            var toRemove = labelIdsToRemove.Where(x => card.LabelIds.Contains(x)).ToList();
            if (toRemove.Count == 0)
            {
                return card; //All not there
            }

            //Need update
            card.LabelIds = card.LabelIds.Except(toRemove).ToList();
            return await UpdateCardAsync(cardId, new List<CardUpdate>
            {
                CardUpdate.Labels(card.LabelIds.Distinct().ToList())
            }, cancellationToken);
        }

        /// <summary>
        /// Removes all labels from a card, leaving it without any labels.
        /// </summary>
        /// <param name="cardId">ID of the card to remove all labels from</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated card with all labels removed</returns>
        public async Task<Card> RemoveAllLabelsFromCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await UpdateCardAsync(cardId, new List<CardUpdate>
            {
                CardUpdate.Labels(new List<string>())
            }, cancellationToken);
        }

        /// <summary>
        /// Updates the definition of a label (Name and Color).
        /// </summary>
        /// <param name="labelWithUpdates">The label object containing the updated name and/or color</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated label</returns>
        public async Task<Label> UpdateLabelAsync(Label labelWithUpdates, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Label>($"{UrlPaths.Labels}/{labelWithUpdates.Id}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(labelWithUpdates));
        }

        /// <summary>
        /// Adds a new label to the board. (Not to be confused with 'AddLabelsToCardAsync', which assigns labels to cards.)
        /// </summary>
        /// <param name="label">The definition of the new label to add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The newly created label</returns>
        public async Task<Label> AddLabelAsync(Label label, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Post<Label>($"{UrlPaths.Labels}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(label));
        }
    }
}