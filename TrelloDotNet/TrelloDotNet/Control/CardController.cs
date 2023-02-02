using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    /// <inheritdoc />
    public class CardController : ICardController
    {
        private readonly ApiRequestController _apiRequestController;
        // ReSharper disable once NotAccessedField.Local
        private readonly ITrelloClient _parent;
        private readonly UriParameterBuilder _uriParameterBuilder;

        internal CardController(ApiRequestController apiRequestController, ITrelloClient parent)
        {
            _apiRequestController = apiRequestController;
            _parent = parent;
            _uriParameterBuilder = new UriParameterBuilder();
        }

        /// <inheritdoc />
        public async Task<Card> GetCardAsync(string id)
        {
            return await _apiRequestController.Get<Card>($"{Constants.UrlSuffixGroup.Cards}/{id}");
        }
        
        /// <inheritdoc />
        public async Task<List<Card>> GetCardsOnBoardAsync(string boardId)
        {
            return await _apiRequestController.Get<List<Card>>($"{Constants.UrlSuffixGroup.Boards}/{boardId}/cards/");
        }

        /// <inheritdoc />
        public async Task<List<Card>> GetCardsOnBoardFilteredAsync(string boardId, CardsFilter filter)
        {
            return await _apiRequestController.Get<List<Card>>($"{Constants.UrlSuffixGroup.Boards}/{boardId}/cards/{filter.GetJsonPropertyName()}");
        }

        /// <inheritdoc />
        public async Task<Card> UpdateCardAsync(Card card)
        {
            var parameters = _uriParameterBuilder.GetViaUriParameterAttribute(card);
            return await _apiRequestController.Put<Card>($"{Constants.UrlSuffixGroup.Cards}/{card.Id}", parameters.ToArray());
        }
       

    }
}