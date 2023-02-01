using System;
using System.Collections.Generic;
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

        public async Task<Card> UpdateAsync( 
            string cardId,
            string newName = null,
            string newDescription = null,
            bool? closed = null,
            List<string> memberIds = null, //todo - use
            string newListId = null,
            List<string> labelsIds = null, //todo - use
            string newLongBoardId = null,
            int? position = null,
            DateTimeOffset? due = null, //todo - use
            DateTimeOffset? start = null, //todo - use
            bool? dueComplete = null,
            bool? subscribed = null 
        )
        {
            //todo - idAttachmentCover
            //todo - cover update
            var parameters = new List<UriParameter>(); //todo - can this de done better
            if (newName != null)
            {
                parameters.Add(new UriParameter("name", newName));
            }
            if (newDescription != null)
            {
                parameters.Add(new UriParameter("desc", newDescription));
            }
            if (closed.HasValue)
            {
                parameters.Add(new UriParameter("closed", closed.Value));
            }
            if (dueComplete.HasValue)
            {
                parameters.Add(new UriParameter("dueComplete", dueComplete.Value));
            }
            if (subscribed.HasValue)
            {
                parameters.Add(new UriParameter("subscribed", subscribed.Value));
            }

            if (due.HasValue)
            {
                parameters.Add(new UriParameter("due", due.Value));
            }
            
            return await _apiRequestController.Put<Card>($"{Constants.UrlSuffixGroup.Cards}/{cardId}", parameters.ToArray());
        }
    }
}