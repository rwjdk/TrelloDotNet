using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Add a sticker to a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="sticker">The Sticker to add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The new sticker</returns>
        public async Task<Sticker> AddStickerToCardAsync(string cardId, Sticker sticker, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Post<Sticker>($"{UrlPaths.Cards}/{cardId}/stickers", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(sticker));
        }

        /// <summary>
        /// Update a sticker
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="stickerWithUpdates">The Sticker to update</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The Updated Sticker</returns>
        public async Task<Sticker> UpdateStickerAsync(string cardId, Sticker stickerWithUpdates, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Sticker>($"{UrlPaths.Cards}/{cardId}/stickers/{stickerWithUpdates.Id}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(stickerWithUpdates));
        }

        /// <summary>
        /// Get the List of Stickers on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The List of Stickers</returns>
        public async Task<List<Sticker>> GetStickersOnCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Sticker>>($"{UrlPaths.Cards}/{cardId}/stickers", cancellationToken);
        }

        /// <summary>
        /// Get a Stickers with a specific Id
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="stickerId">Id of the Sticker</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Sticker</returns>
        public async Task<Sticker> GetStickerAsync(string cardId, string stickerId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Sticker>($"{UrlPaths.Cards}/{cardId}/stickers/{stickerId}", cancellationToken);
        }

        /// <summary>
        /// Delete a sticker (WARNING: THERE IS NO WAY GOING BACK!!!).
        /// </summary>
        /// <param name="cardId">Id of Card that have the sticker</param>
        /// <param name="stickerId">Id of the sticker</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteStickerAsync(string cardId, string stickerId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete($"{UrlPaths.Cards}/{cardId}/stickers/{stickerId}", cancellationToken);
        }
    }
}