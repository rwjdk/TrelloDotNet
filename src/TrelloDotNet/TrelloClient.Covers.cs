using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Updates the cover of a card. This method is functionally equivalent to AddCoverToCardAsync and is provided for discoverability. You can also update the cover using UpdateCardAsync.
        /// </summary>
        /// <param name="cardId">ID of the card to update the cover on</param>
        /// <param name="newCover">The new cover to set on the card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated card with the new cover</returns>
        public async Task<Card> UpdateCoverOnCardAsync(string cardId, CardCover newCover, CancellationToken cancellationToken = default)
        {
            return await AddCoverToCardAsync(cardId, newCover, cancellationToken);
        }

        /// <summary>
        /// Adds a cover to a card. You can also add a cover using UpdateCardAsync.
        /// </summary>
        /// <param name="cardId">ID of the card to add the cover to</param>
        /// <param name="coverToAdd">The cover object to add to the card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated card with the added cover</returns>
        public async Task<Card> AddCoverToCardAsync(string cardId, CardCover coverToAdd, CancellationToken cancellationToken = default)
        {
            if (coverToAdd == null)
            {
                throw new TrelloApiException("Cover can't be null (If you trying to remove a cover see 'RemoveCoverFromCardAsync')", string.Empty);
            }

            coverToAdd.PrepareForAddUpdate();
            string payload = $"{{\"cover\":{JsonSerializer.Serialize(coverToAdd)}}}";
            return await _apiRequestController.PutWithJsonPayload<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, payload);
        }

        /// <summary>
        /// Removes the cover from a card.
        /// </summary>
        /// <param name="cardId">ID of the card to remove the cover from</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated card with the cover removed</returns>
        public async Task<Card> RemoveCoverFromCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, new QueryParameter("cover", string.Empty));
        }
    }
}