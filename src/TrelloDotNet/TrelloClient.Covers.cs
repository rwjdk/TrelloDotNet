using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Update a Cover to a card (this is equivalent to AddCoverToCardAsync, but here for discover-ability. Tip: It is also possible to update the cover via UpdateCardAsync)
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="newCover">The new Cover</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task<Card> UpdateCoverOnCardAsync(string cardId, CardCover newCover, CancellationToken cancellationToken = default)
        {
            return await AddCoverToCardAsync(cardId, newCover, cancellationToken);
        }

        /// <summary>
        /// Add a Cover to a card. Tip: It is also possible to add the cover via UpdateCardAsync
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="coverToAdd">The Cover to Add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
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
        /// Remove a cover from a Card
        /// </summary>
        /// <param name="cardId">Id of Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Card with the removed Cover</returns>
        public async Task<Card> RemoveCoverFromCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Card>($"{UrlPaths.Cards}/{cardId}", cancellationToken, new QueryParameter("cover", string.Empty));
        }
    }
}