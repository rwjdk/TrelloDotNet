using System;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    public class CardController : ICardController
    {
        private readonly ApiRequestController _apiRequestController;

        internal CardController(ApiRequestController apiRequestController)
        {
            _apiRequestController = apiRequestController;
        }

        public async Task<Card> GetAsync(string cardId)
        {
            return await _apiRequestController.GetResponse<Card>($"{Constants.UrlSuffixGroup.Cards}/{cardId}");
        }
    }
}