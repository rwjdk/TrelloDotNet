using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Retrieves all attachments associated with the specified card, including file uploads and link attachments.
        /// </summary>
        /// <param name="cardId">ID of the Card to retrieve attachments from</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A list of all attachments present on the card</returns>
        public async Task<List<Attachment>> GetAttachmentsOnCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Attachment>>(GetUrlBuilder.GetAttachmentsOnCard(cardId), cancellationToken);
        }

        /// <summary>
        /// Deletes a specific attachment from a card by its ID, removing the file or link from the card's attachments.
        /// </summary>
        /// <param name="cardId">ID of the Card containing the attachment</param>
        /// <param name="attachmentId">ID of the Attachment to delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteAttachmentOnCardAsync(string cardId, string attachmentId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete(GetUrlBuilder.GetAttachmentOnCard(cardId, attachmentId), cancellationToken, 0);
        }

        /// <summary>
        /// Retrieves a single attachment from a card by its ID, returning detailed information about the attachment.
        /// </summary>
        /// <param name="cardId">ID of the Card containing the attachment</param>
        /// <param name="attachmentId">ID of the Attachment to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task<Attachment> GetAttachmentOnCardAsync(string cardId, string attachmentId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Attachment>(GetUrlBuilder.GetAttachmentOnCard(cardId, attachmentId), cancellationToken);
        }

        /// <summary>
        /// Adds a link attachment (URL) to a card, optionally specifying a name and position for the attachment.
        /// </summary>
        /// <param name="cardId">ID of the Card to add the attachment to</param>
        /// <param name="attachmentUrlLink">A URL link attachment to add to the card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The created Attachment object representing the new link attachment</returns>
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
                        parameters.Add(new QueryParameter("pos", "bottom")); //NB: Trello have a mis-implementation where these are reversed on attachments so however wrong this looks, it is correct
                        break;
                    case NamedPosition.Bottom:
                        parameters.Add(new QueryParameter("pos", "top")); //NB: Trello have a mis-implementation where these are reversed on attachments so however wrong this looks, it is correct
                        break;
                    default:
                        parameters.Add(new QueryParameter("pos", Convert.ToInt32(attachmentUrlLink.NamedPosition.Value)));
                        break;
                }
            }

            return await _apiRequestController.Post<Attachment>($"{UrlPaths.Cards}/{cardId}/attachments", cancellationToken, parameters.ToArray());
        }

        /// <summary>
        /// Uploads a file as an attachment to a card, with the option to set the uploaded file as the card's cover image.
        /// </summary>
        /// <param name="cardId">ID of the Card to add the file attachment to</param>
        /// <param name="attachmentFileUpload">A file upload attachment to add to the card</param>
        /// <param name="setAsCover">If true, sets the uploaded attachment as the card's cover image</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The created Attachment object representing the uploaded file</returns>
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

        /// <summary>
        /// Downloads the content stream of a specific attachment on a card by its ID, allowing you to access the file data.
        /// </summary>
        /// <param name="cardId">ID of the Card containing the attachment</param>
        /// <param name="attachmentId">ID of the Attachment to download</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A stream containing the downloaded attachment data</returns>
        public async Task<Stream> DownloadAttachmentAsync(string cardId, string attachmentId, CancellationToken cancellationToken = default)
        {
            Attachment attachment = await GetAttachmentOnCardAsync(cardId, attachmentId, cancellationToken);
            return await DownloadAttachmentAsync(attachment.Url, cancellationToken);
        }

        /// <summary>
        /// Downloads the content stream of an attachment from a direct URL, using Trello authentication if required.
        /// </summary>
        /// <param name="url">URL of the attachment to download</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A stream containing the downloaded attachment data</returns>
        public async Task<Stream> DownloadAttachmentAsync(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                _apiRequestController.HttpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"OAuth oauth_consumer_key=\"{_apiRequestController.ApiKey}\", oauth_token=\"{_apiRequestController.Token}\"");
                return await _apiRequestController.HttpClient.GetStreamAsync(url);
            }
            finally
            {
                _apiRequestController.HttpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}