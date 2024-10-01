using System;
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
        /// Get Attachments on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Attachments</returns>
        public async Task<List<Attachment>> GetAttachmentsOnCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Attachment>>(GetUrlBuilder.GetAttachmentsOnCard(cardId), cancellationToken);
        }

        /// <summary>
        /// Delete an Attachments on a card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="attachmentId">Id of Attachment</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteAttachmentOnCardAsync(string cardId, string attachmentId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete(GetUrlBuilder.GetAttachmentOnCard(cardId, attachmentId), cancellationToken, 0);
        }

        /// <summary>
        /// Add an Attachment to a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="attachmentUrlLink">A Link Attachment</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Created Attachment</returns>
        public async Task<Attachment> AddAttachmentToCardAsync(string cardId, AttachmentUrlLink attachmentUrlLink, CancellationToken cancellationToken = default)
        {
            var parameters = new List<QueryParameter> { new QueryParameter("url", attachmentUrlLink.Url) };
            if (!string.IsNullOrWhiteSpace(attachmentUrlLink.Name))
            {
                parameters.Add(new QueryParameter("name", attachmentUrlLink.Name));
            }

            if (attachmentUrlLink.NamedPosition.HasValue)
            {
                switch (attachmentUrlLink.NamedPosition.Value)
                {
                    case NamedPosition.Top:
                        parameters.Add(new QueryParameter("pos", "bottom")); //NB: Trello have an mis-implementation where these are reversed on attachments so however wrong this looks, it is correct
                    break;
                    case NamedPosition.Bottom:
                        parameters.Add(new QueryParameter("pos", "top")); //NB: Trello have an mis-implementation where these are reversed on attachments so however wrong this looks, it is correct
                        break;
                    default:
                        parameters.Add(new QueryParameter("pos", Convert.ToInt32(attachmentUrlLink.NamedPosition.Value)));
                        break;
                };
            }

            return await _apiRequestController.Post<Attachment>($"{UrlPaths.Cards}/{cardId}/attachments", cancellationToken, parameters.ToArray());
        }

        /// <summary>
        /// Add an Attachment to a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="attachmentFileUpload">A Link Attachment</param>
        /// <param name="setAsCover">Make this attachment the cover of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Created Attachment</returns>
        public async Task<Attachment> AddAttachmentToCardAsync(string cardId, AttachmentFileUpload attachmentFileUpload, bool setAsCover = false, CancellationToken cancellationToken = default)
        {
            var parameters = new List<QueryParameter>();
            if (!string.IsNullOrWhiteSpace(attachmentFileUpload.Name))
            {
                parameters.Add(new QueryParameter("name", attachmentFileUpload.Name));
            }

            if (setAsCover)
            {
                parameters.Add(new QueryParameter("setCover", "true"));
            }
            
            return await _apiRequestController.PostWithAttachmentFileUpload<Attachment>($"{UrlPaths.Cards}/{cardId}/attachments", attachmentFileUpload, cancellationToken, parameters.ToArray());
        }
    }
}