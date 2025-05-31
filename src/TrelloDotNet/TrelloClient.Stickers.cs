using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Adds a sticker to a card.
        /// </summary>
        /// <param name="cardId">ID of the card to add the sticker to</param>
        /// <param name="sticker">The sticker object to add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The newly created sticker</returns>
        public async Task<Sticker> AddStickerToCardAsync(string cardId, Sticker sticker, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Post<Sticker>($"{UrlPaths.Cards}/{cardId}/stickers", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(sticker));
        }

        /// <summary>
        /// Updates the properties of an existing sticker on a card.
        /// </summary>
        /// <param name="cardId">ID of the card containing the sticker</param>
        /// <param name="stickerWithUpdates">The sticker object with updated properties</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated sticker</returns>
        public async Task<Sticker> UpdateStickerAsync(string cardId, Sticker stickerWithUpdates, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Sticker>($"{UrlPaths.Cards}/{cardId}/stickers/{stickerWithUpdates.Id}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(stickerWithUpdates));
        }

        /// <summary>
        /// Retrieves all stickers attached to a specific card.
        /// </summary>
        /// <param name="cardId">ID of the card to retrieve stickers for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of stickers on the card</returns>
        public async Task<List<Sticker>> GetStickersOnCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Sticker>>(GetUrlBuilder.GetStickersOnCard(cardId), cancellationToken);
        }

        /// <summary>
        /// Retrieves a sticker with a specific ID from a card.
        /// </summary>
        /// <param name="cardId">ID of the card containing the sticker</param>
        /// <param name="stickerId">ID of the sticker to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The requested sticker</returns>
        public async Task<Sticker> GetStickerAsync(string cardId, string stickerId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Sticker>(GetUrlBuilder.GetSticker(cardId, stickerId), cancellationToken);
        }

        /// <summary>
        /// Deletes a sticker from a card. This operation is irreversible.
        /// </summary>
        /// <param name="cardId">ID of the card containing the sticker</param>
        /// <param name="stickerId">ID of the sticker to delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteStickerAsync(string cardId, string stickerId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete($"{UrlPaths.Cards}/{cardId}/stickers/{stickerId}", cancellationToken, 0);
        }
    }
}