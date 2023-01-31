using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    public class CardController : ICardController
    {
        private readonly ApiRequestController _requestController;

        internal CardController(ApiRequestController requestController)
        {
            _requestController = requestController;
        }

        public async Task<Card> GetAsync(string cardId)
        {
            return await _requestController.GetResponse<Card>($"{Constants.UrlSuffixGroup.Cards}/{cardId}");
        }
    }
}