using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet.Control
{
    /// <summary>
    /// Utility to build the the needed URL Suffixes to call the Trello Rest API
    /// </summary>
    public static class GetUrlBuilder
    {
        /// <summary>
        /// boards/{boardId}/actions
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <returns>The URL Suffix</returns>
        public static string GetActionsOnBoard(string boardId)
        {
            return $"{UrlPaths.Boards}/{boardId}/{UrlPaths.Actions}";
        }

        /// <summary>
        /// cards}/{cardId}/actions
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The URL Suffix</returns>
        public static string GetActionsOnCard(string cardId)
        {
            return $"{UrlPaths.Cards}/{cardId}/{UrlPaths.Actions}";
        }

        /// <summary>
        /// cards/{cardId}/attachments
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The URL Suffix</returns>
        public static string GetAttachmentsOnCard(string cardId)
        {
            return $"{UrlPaths.Cards}/{cardId}/attachments";
        }

        /// <summary>
        /// cards/{cardId}/attachments/{attachmentId}
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="attachmentId">Id of the Attachment</param>
        /// <returns>The URL Suffix</returns>
        public static string GetAttachmentOnCard(string cardId, string attachmentId)
        {
            return $"{UrlPaths.Cards}/{cardId}/attachments/{attachmentId}";
        }

        /// <summary>
        /// lists/{listId}/actions
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <returns>The URL Suffix</returns>
        public static string GetActionsForList(string listId)
        {
            return $"{UrlPaths.Lists}/{listId}/{UrlPaths.Actions}";
        }

        /// <summary>
        /// members/{memberId}/actions
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <returns>The URL Suffix</returns>
        public static string GetActionsForMember(string memberId)
        {
            return $"{UrlPaths.Members}/{memberId}/{UrlPaths.Actions}";
        }

        /// <summary>
        /// organizations/{organizationId}/actions
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <returns>The URL Suffix</returns>
        public static string GetActionsForOrganization(string organizationId)
        {
            return $"{UrlPaths.Organizations}/{organizationId}/{UrlPaths.Actions}";
        }

        /// <summary>
        /// boards/{boardId}
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="options">Options</param>
        /// <returns>The URL Suffix</returns>
        public static string GetBoard(string boardId, GetBoardOptions options = null)
        {
            return $"{UrlPaths.Boards}/{boardId}"+AddGetBoardOptions(options);
        }

        /// <summary>
        /// members/{memberId}/boards/
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="options">Options</param>
        /// <returns>The URL Suffix</returns>
        public static string GetBoardsForMember(string memberId, GetBoardOptions options = null)
        {
            return $"{UrlPaths.Members}/{memberId}/{UrlPaths.Boards}/"+AddGetBoardOptions(options);
        }

        /// <summary>
        /// organizations/{organizationId}/boards
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <param name="options">Options</param>
        /// <returns>The URL Suffix</returns>
        public static string GetBoardsInOrganization(string organizationId, GetBoardOptions options = null)
        {
            return $"{UrlPaths.Organizations}/{organizationId}/{UrlPaths.Boards}"+AddGetBoardOptions(options);
        }

        /// <summary>
        /// cards/{cardId}
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="options">Options</param>
        /// <returns>The URL Suffix</returns>
        public static string GetCard(string cardId, GetCardOptions options = null)
        {
            return $"{UrlPaths.Cards}/{cardId}"+AddGetCardOptions(options);
        }

        /// <summary>
        /// boards/{boardId}/cards
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="options">Options</param>
        /// <returns>The URL Suffix</returns>
        public static string GetCardsOnBoard(string boardId, GetCardOptions options = null)
        {
            return $"{UrlPaths.Boards}/{boardId}/{UrlPaths.Cards}/"+AddGetCardOptions(options);
        }

        /// <summary>
        /// lists/{listId}/cards
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <param name="options">Options</param>
        /// <returns>The URL Suffix</returns>
        public static string GetCardsInList(string listId, GetCardOptions options = null)
        {
            return $"{UrlPaths.Lists}/{listId}/{UrlPaths.Cards}/"+AddGetCardOptions(options);
        }

        /// <summary>
        /// members/{memberId}/cards/
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="options">Options</param>
        /// <returns>The URL Suffix</returns>
        public static string GetCardsForMember(string memberId, GetCardOptions options = null)
        {
            return $"{UrlPaths.Members}/{memberId}/{UrlPaths.Cards}/"+AddGetCardOptions(options);
        }

        /// <summary>
        /// checklists/{checkListId}
        /// </summary>
        /// <param name="checkListId">Id of the Checklist</param>
        /// <returns>The URL Suffix</returns>
        public static string GetChecklist(string checkListId)
        {
            return $"{UrlPaths.Checklists}/{checkListId}";
        }

        /// <summary>
        /// boards/{boardId}/checklists
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <returns>The URL Suffix</returns>
        public static string GetChecklistsOnBoard(string boardId)
        {
            return $"{UrlPaths.Boards}/{boardId}/{UrlPaths.Checklists}";
        }

        /// <summary>
        /// cards/{cardId}/checklists
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The URL Suffix</returns>
        public static string GetChecklistsOnCard(string cardId)
        {
            return $"{UrlPaths.Cards}/{cardId}/{UrlPaths.Checklists}";
        }

        /// <summary>
        /// actions/{commentActionId}/reactions"
        /// </summary>
        /// <param name="commentActionId">Id of the Comment</param>
        /// <returns>The URL Suffix</returns>
        public static string GetCommentReactions(string commentActionId)
        {
            return $"{UrlPaths.Actions}/{commentActionId}/reactions";
        }

        /// <summary>
        /// boards/{boardId}/customFields
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <returns>The URL Suffix</returns>
        public static string GetCustomFieldsOnBoard(string boardId)
        {
            return $"{UrlPaths.Boards}/{boardId}/{UrlPaths.CustomFields}";
        }

        /// <summary>
        /// cards/{cardId}/customFieldItems
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The URL Suffix</returns>
        public static string GetCustomFieldItemsForCard(string cardId)
        {
            return $"{UrlPaths.Cards}/{cardId}/{UrlPaths.CustomFieldItems}";
        }

        /// <summary>
        /// boards/{boardId}/labels"
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <returns>The URL Suffix</returns>
        public static string GetLabelsOfBoard(string boardId)
        {
            return $"{UrlPaths.Boards}/{boardId}/{UrlPaths.Labels}";
        }

        /// <summary>
        /// boards/{boardId}/lists
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <returns>The URL Suffix</returns>
        public static string GetListsOnBoard(string boardId)
        {
            return $"{UrlPaths.Boards}/{boardId}/{UrlPaths.Lists}";
        }

        /// <summary>
        /// lists/{listId}
        /// </summary>
        /// <param name="listId">Id of the List</param>
        /// <returns>The URL Suffix</returns>
        public static string GetList(string listId)
        {
            return $"{UrlPaths.Lists}/{listId}";
        }

        /// <summary>
        /// boards/{boardId}/members/
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <returns>The URL Suffix</returns>
        public static string GetMembersOfBoard(string boardId)
        {
            return $"{UrlPaths.Boards}/{boardId}/{UrlPaths.Members}/";
        }

        /// <summary>
        /// cards/{cardId}/members/
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The URL Suffix</returns>
        public static string GetMembersOfCard(string cardId)
        {
            return $"{UrlPaths.Cards}/{cardId}/{UrlPaths.Members}/";
        }

        /// <summary>
        /// cards/{cardId}/membersVoted
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The URL Suffix</returns>
        public static string GetMembersWhoVotedOnOfCard(string cardId)
        {
            return $"{UrlPaths.Cards}/{cardId}/membersVoted";
        }

        /// <summary>
        /// members/{memberId}
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <returns>The URL Suffix</returns>
        public static string GetMember(string memberId)
        {
            return $"{UrlPaths.Members}/{memberId}";
        }

        /// <summary>
        /// tokens/{token}/member"
        /// </summary>
        /// <param name="token">API token</param>
        /// <returns>The URL Suffix</returns>
        public static string GetTokenMember(string token)
        {
            return $"{UrlPaths.Tokens}/{token}/member";
        }

        /// <summary>
        /// organizations/{organizationId}/members/
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <returns>The URL Suffix</returns>
        public static string GetMembersOfOrganization(string organizationId)
        {
            return $"{UrlPaths.Organizations}/{organizationId}/{UrlPaths.Members}/";
        }

        /// <summary>
        /// boards/{boardId}/memberships
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <returns>The URL Suffix</returns>
        public static string GetMembershipsOfBoard(string boardId)
        {
            return $"{UrlPaths.Boards}/{boardId}/memberships";
        }

        /// <summary>
        /// organizations/{organizationId}
        /// </summary>
        /// <param name="organizationId">Id of the Organization</param>
        /// <returns>The URL Suffix</returns>
        public static string GetOrganization(string organizationId)
        {
            return $"{UrlPaths.Organizations}/{organizationId}";
        }

        /// <summary>
        /// members/{memberId}/organizations
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <returns>The URL Suffix</returns>
        public static string GetOrganizationsForMember(string memberId)
        {
            return $"{UrlPaths.Members}/{memberId}/organizations";
        }

        /// <summary>
        /// cards/{cardId}/stickers
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The URL Suffix</returns>
        public static string GetStickersOnCard(string cardId)
        {
            return $"{UrlPaths.Cards}/{cardId}/stickers";
        }

        /// <summary>
        /// cards/{cardId}/stickers/{stickerId}
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="stickerId"></param>
        /// <returns>The URL Suffix</returns>
        public static string GetSticker(string cardId, string stickerId)
        {
            return $"{UrlPaths.Cards}/{cardId}/stickers/{stickerId}";
        }

        /// <summary>
        /// tokens/{token}/webhooks
        /// </summary>
        /// <param name="token">The API Token</param>
        /// <returns>The URL Suffix</returns>
        public static string GetWebhooksForToken(string token)
        {
            return $"{UrlPaths.Tokens}/{token}/webhooks";
        }

        /// <summary>
        /// webhooks/{webhookId}
        /// </summary>
        /// <param name="webhookId">Id of the webhook</param>
        /// <returns>The URL Suffix</returns>
        public static string GetWebhook(string webhookId)
        {
            return $"{UrlPaths.Webhooks}/{webhookId}";
        }

        /// <summary>
        /// cards/{cardId}/pluginData
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <returns>The URL Suffix</returns>
        public static string GetPluginDataOnCard(string cardId)
        {
            return $"{UrlPaths.Cards}/{cardId}/pluginData";
        }
        
        /// <summary>
        /// boards/{boardId}/pluginData
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <returns>The URL Suffix</returns>
        public static string GetPluginDataOfBoard(string boardId)
        {
            return $"{UrlPaths.Boards}/{boardId}/pluginData";
        }

        private static string AddGetCardOptions(GetCardOptions options)
        {
            return options != null ? ApiRequestController.GetParametersAsString(options.GetParameters()).Replace("&", "?", 0, 1).ToString() : string.Empty;
        }
        
        private static string AddGetBoardOptions(GetBoardOptions options)
        {
            return options != null ? ApiRequestController.GetParametersAsString(options.GetParameters()).Replace("&", "?", 0, 1).ToString() : string.Empty;
        }
    }
}