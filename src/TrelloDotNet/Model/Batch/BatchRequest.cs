using System;
using TrelloDotNet.Control;
using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model.Batch
{
    /// <summary>
    /// Represent a Batch-request
    /// </summary>
    public class BatchRequest
    {
        /// <summary>
        /// GET Url for the request
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The Action that should handle what to do with the data
        /// </summary>
        public Action<BatchResult> Action { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">GET Url for the request</param>
        /// <param name="action">The Action that should handle what to do with the data</param>
        public BatchRequest(string url, Action<BatchResult> action)
        {
            Url = url;
            Action = action;
        }

        /// <summary>
        /// boards/{boardId}/actions
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetActionsOnBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetActionsOnBoard(boardId), action);
        }

        /// <summary>
        /// cards/{cardId}/actions
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetActionsOnCard(string cardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetActionsOnCard(cardId), action);
        }

        /// <summary>
        /// cards/{cardId}/attachments
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetAttachmentsOnCard(string cardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetAttachmentsOnCard(cardId), action);
        }

        /// <summary>
        /// cards/{cardId}/attachments/{attachmentId}
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="attachmentId">Id of the Attachment</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetAttachmentOnCard(string cardId, string attachmentId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetAttachmentOnCard(cardId, attachmentId), action);
        }

        /// <summary>
        /// lists/{listId}/actions
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetActionsForList(string listId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetActionsForList(listId), action);
        }

        /// <summary>
        /// members/{memberId}/actions
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetActionsForMember(string memberId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetActionsForMember(memberId), action);
        }

        /// <summary>
        /// organizations/{organizationId}/actions
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetActionsForOrganization(string organizationId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetActionsForOrganization(organizationId), action);
        }

        /// <summary>
        /// boards/{boardId}
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetBoard(boardId), action);
        }

        /// <summary>
        /// boards/{boardId}
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="options">Options</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetBoard(string boardId, GetBoardOptions options, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetBoard(boardId, options), action);
        }

        /// <summary>
        /// members/{memberId}/boards/
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetBoardsForMember(string memberId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetBoardsForMember(memberId), action);
        }

        /// <summary>
        /// members/{memberId}/boards/
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="options">Options</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetBoardsForMember(string memberId, GetBoardOptions options, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetBoardsForMember(memberId, options), action);
        }

        /// <summary>
        /// organizations/{organizationId}/boards
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetBoardsInOrganization(string organizationId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetBoardsInOrganization(organizationId), action);
        }

        /// <summary>
        /// organizations/{organizationId}/boards
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <param name="options">Options</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetBoardsInOrganization(string organizationId, GetBoardOptions options, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetBoardsInOrganization(organizationId, options), action);
        }

        /// <summary>
        /// cards/{cardId}
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCard(string cardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCard(cardId), action);
        }

        /// <summary>
        /// cards/{cardId}
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="options">Options</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCard(string cardId, GetCardOptions options, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCard(cardId, options), action);
        }

        /// <summary>
        /// boards/{boardId}/cards
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCardsOnBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCardsOnBoard(boardId), action);
        }

        /// <summary>
        /// boards/{boardId}/cards
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="options">Options</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCardsOnBoard(string boardId, GetCardOptions options, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCardsOnBoard(boardId, options), action);
        }

        /// <summary>
        /// lists/{listId}/cards
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCardsInList(string listId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCardsInList(listId), action);
        }

        /// <summary>
        /// lists/{listId}/cards
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="options">Options</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCardsInList(string listId, GetCardOptions options, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCardsInList(listId, options), action);
        }

        /// <summary>
        /// members/{memberId}/cards/
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCardsForMember(string memberId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCardsForMember(memberId), action);
        }

        /// <summary>
        /// members/{memberId}/cards/
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="options">Options</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCardsForMember(string memberId, GetCardOptions options, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCardsForMember(memberId, options), action);
        }

        /// <summary>
        /// checklists/{checkListId}
        /// </summary>
        /// <param name="checkListId">Id of the Checklist</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetChecklist(string checkListId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetChecklist(checkListId), action);
        }

        /// <summary>
        /// boards/{boardId}/checklists
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetChecklistsOnBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetChecklistsOnBoard(boardId), action);
        }

        /// <summary>
        /// cards/{cardId}/checklists
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetChecklistsOnCard(string cardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetChecklistsOnCard(cardId), action);
        }

        /// <summary>
        /// actions/{commentActionId}/reactions"
        /// </summary>
        /// <param name="commentActionId">Id of the Comment</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCommentReactions(string commentActionId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCommentReactions(commentActionId), action);
        }

        /// <summary>
        /// boards/{boardId}/customFields
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCustomFieldsOnBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCustomFieldsOnBoard(boardId), action);
        }

        /// <summary>
        /// cards/{cardId}/customFieldItems
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetCustomFieldItemsForCard(string cardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetCustomFieldItemsForCard(cardId), action);
        }

        /// <summary>
        /// boards/{boardId}/labels
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetLabelsOfBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetLabelsOfBoard(boardId), action);
        }

        /// <summary>
        /// boards/{boardId}/lists
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetListsOnBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetListsOnBoard(boardId), action);
        }

        /// <summary>
        /// lists/{listId}
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetList(string listId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetList(listId), action);
        }

        /// <summary>
        /// boards/{boardId}/members/
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetMembersOfBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetMembersOfBoard(boardId), action);
        }

        /// <summary>
        /// cards/{cardId}/members/
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetMembersOfCard(string cardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetMembersOfCard(cardId), action);
        }

        /// <summary>
        /// members/{memberId}
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetMember(string memberId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetMember(memberId), action);
        }

        /// <summary>
        /// tokens/{token}/member"
        /// </summary>
        /// <param name="token">API token</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetTokenMember(string token, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetTokenMember(token), action);
        }

        /// <summary>
        /// organizations/{organizationId}/members/
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetMembersOfOrganization(string organizationId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetMembersOfOrganization(organizationId), action);
        }

        /// <summary>
        /// boards/{boardId}/memberships
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetMembershipsOfBoard(string boardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetMembershipsOfBoard(boardId), action);
        }

        /// <summary>
        /// organizations/{organizationId}
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetOrganization(string organizationId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetOrganization(organizationId), action);
        }

        /// <summary>
        /// members/{memberId}/organizations
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetOrganizationsForMember(string memberId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetOrganizationsForMember(memberId), action);
        }

        /// <summary>
        /// cards/{cardId}/stickers
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetStickersOnCard(string cardId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetStickersOnCard(cardId), action);
        }

        /// <summary>
        /// cards/{cardId}/stickers/{stickerId}
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="stickerId"></param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetSticker(string cardId, string stickerId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetSticker(cardId, stickerId), action);
        }

        /// <summary>
        /// tokens/{token}/webhooks
        /// </summary>
        /// <param name="token">The API Token</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetWebhooksForToken(string token, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetWebhooksForToken(token), action);
        }

        /// <summary>
        /// webhooks/{webhookId}
        /// </summary>
        /// <param name="webhookId">Id of the webhook</param>
        /// <param name="action">The action to deal with the batch-result</param>
        /// <returns>The Request</returns>
        public static BatchRequest GetWebhook(string webhookId, Action<BatchResult> action)
        {
            return new BatchRequest(GetUrlBuilder.GetWebhook(webhookId), action);
        }
    }
}