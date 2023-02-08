using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Control.Webhook
{
    /// <summary>
    /// The various Basic Events that can be subscribed to
    /// </summary>
    public class WebhookDataReceiverBasicEvents
    {
        /// <summary>
        /// OnUpdateCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateCard;
        /// <summary>
        /// OnAcceptEnterpriseJoinRequest
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAcceptEnterpriseJoinRequest;
        /// <summary>
        /// OnAddAttachmentToCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddAttachmentToCard;
        /// <summary>
        /// OnAddChecklistToCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddChecklistToCard;
        /// <summary>
        /// OnAddMemberToBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddMemberToBoard;
        /// <summary>
        /// OnAddMemberToCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddMemberToCard;
        /// <summary>
        /// OnAddMemberToOrganization
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddMemberToOrganization;
        /// <summary>
        /// OnAddOrganizationToEnterprise
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddOrganizationToEnterprise;
        /// <summary>
        /// OnAddToEnterprisePluginWhitelist
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddToEnterprisePluginWhitelist;
        /// <summary>
        /// OnAddToOrganizationBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddToOrganizationBoard;
        /// <summary>
        /// OnCommentCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCommentCard;
        /// <summary>
        /// OnConvertToCardFromCheckItem
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnConvertToCardFromCheckItem;
        /// <summary>
        /// OnCopyBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCopyBoard;
        /// <summary>
        /// OnCopyCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCopyCard;
        /// <summary>
        /// OnCopyCommentCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCopyCommentCard;
        /// <summary>
        /// OnCreateBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateBoard;
        /// <summary>
        /// OnCreateCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateCard;
        /// <summary>
        /// OnCreateList
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateList;
        /// <summary>
        /// OnCreateOrganization
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateOrganization;
        /// <summary>
        /// OnDeleteBoardInvitation
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDeleteBoardInvitation;
        /// <summary>
        /// OnDeleteCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDeleteCard;
        /// <summary>
        /// OnDeleteOrganizationInvitation
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDeleteOrganizationInvitation;
        /// <summary>
        /// OnDisableEnterprisePluginWhitelist
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDisableEnterprisePluginWhitelist;
        /// <summary>
        /// OnDisablePlugin
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDisablePlugin;
        /// <summary>
        /// OnDisablePowerUp
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDisablePowerUp;
        /// <summary>
        /// OnEmailCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnEmailCard;
        /// <summary>
        /// OnEnableEnterprisePluginWhitelist
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnEnableEnterprisePluginWhitelist;
        /// <summary>
        /// OnEnablePlugin
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnEnablePlugin;
        /// <summary>
        /// OnEnablePowerUp
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnEnablePowerUp;
        /// <summary>
        /// OnMakeAdminOfBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMakeAdminOfBoard;
        /// <summary>
        /// OnMakeNormalMemberOfBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMakeNormalMemberOfBoard;
        /// <summary>
        /// OnMakeNormalMemberOfOrganization
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMakeNormalMemberOfOrganization;
        /// <summary>
        /// OnMakeObserverOfBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMakeObserverOfBoard;
        /// <summary>
        /// OnMemberJoinedTrello
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMemberJoinedTrello;
        /// <summary>
        /// OnMoveCardFromBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMoveCardFromBoard;
        /// <summary>
        /// OnMoveCardToBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMoveCardToBoard;
        /// <summary>
        /// OnMoveListFromBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMoveListFromBoard;
        /// <summary>
        /// OnMoveListToBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMoveListToBoard;
        /// <summary>
        /// OnRemoveChecklistFromCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnRemoveChecklistFromCard;
        /// <summary>
        /// OnRemoveFromEnterprisePluginWhitelist
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnRemoveFromEnterprisePluginWhitelist;
        /// <summary>
        /// OnRemoveFromOrganizationBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnRemoveFromOrganizationBoard;
        /// <summary>
        /// OnRemoveMemberFromCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnRemoveMemberFromCard;
        /// <summary>
        /// OnRemoveOrganizationFromEnterprise
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnRemoveOrganizationFromEnterprise;
        /// <summary>
        /// OnUnconfirmedBoardInvitation
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUnconfirmedBoardInvitation;
        /// <summary>
        /// OnUnconfirmedOrganizationInvitation
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUnconfirmedOrganizationInvitation;
        /// <summary>
        /// OnUpdateBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateBoard;
        /// <summary>
        /// OnUpdateCheckItemStateOnCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateCheckItemStateOnCard;
        /// <summary>
        /// OnUpdateChecklist
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateChecklist;
        /// <summary>
        /// OnUpdateList
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateList;
        /// <summary>
        /// OnUpdateMember
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateMember;
        /// <summary>
        /// OnUpdateOrganization
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateOrganization;
        /// <summary>
        /// OnAddLabelToCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnAddLabelToCard;
        /// <summary>
        /// OnCopyChecklist
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCopyChecklist;
        /// <summary>
        /// OnCreateBoardInvitation
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateBoardInvitation;
        /// <summary>
        /// OnCreateBoardPreference
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateBoardPreference;
        /// <summary>
        /// OnCreateCheckItem
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateCheckItem;
        /// <summary>
        /// OnCreateLabel
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateLabel;
        /// <summary>
        /// OnCreateOrganizationInvitation
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnCreateOrganizationInvitation;
        /// <summary>
        /// OnDeleteAttachmentFromCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDeleteAttachmentFromCard;
        /// <summary>
        /// OnDeleteCheckItem
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDeleteCheckItem;
        /// <summary>
        /// OnDeleteComment
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDeleteComment;
        /// <summary>
        /// OnDeleteLabel
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnDeleteLabel;
        /// <summary>
        /// OnMakeAdminOfOrganization
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnMakeAdminOfOrganization;
        /// <summary>
        /// OnRemoveLabelFromCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnRemoveLabelFromCard;
        /// <summary>
        /// OnRemoveMemberFromBoard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnRemoveMemberFromBoard;
        /// <summary>
        /// OnRemoveMemberFromOrganization
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnRemoveMemberFromOrganization;
        /// <summary>
        /// OnUpdateCheckItem
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateCheckItem;
        /// <summary>
        /// OnUpdateComment
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateComment;
        /// <summary>
        /// OnUpdateLabel
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUpdateLabel;
        /// <summary>
        /// OnVoteOnCard
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnVoteOnCard;
        /// <summary>
        /// OnUnknownActionType
        /// </summary>
        public event WebhookEventHandler<WebhookAction> OnUnknownActionType;

        internal void FireEvent(WebhookAction action)
        {
            switch (action.Type)
            {
                case "acceptEnterpriseJoinRequest":
                    OnAcceptEnterpriseJoinRequest?.Invoke(action);
                    break;
                case "addAttachmentToCard":
                    OnAddAttachmentToCard?.Invoke(action);
                    break;
                case "addChecklistToCard":
                    OnAddChecklistToCard?.Invoke(action);
                    break;
                case "addMemberToBoard":
                    OnAddMemberToBoard?.Invoke(action);
                    break;
                case "addMemberToCard":
                    OnAddMemberToCard?.Invoke(action);
                    break;
                case "addMemberToOrganization":
                    OnAddMemberToOrganization?.Invoke(action);
                    break;
                case "addOrganizationToEnterprise":
                    OnAddOrganizationToEnterprise?.Invoke(action);
                    break;
                case "addToEnterprisePluginWhitelist":
                    OnAddToEnterprisePluginWhitelist?.Invoke(action);
                    break;
                case "addToOrganizationBoard":
                    OnAddToOrganizationBoard?.Invoke(action);
                    break;
                case "commentCard":
                    OnCommentCard?.Invoke(action);
                    break;
                case "convertToCardFromCheckItem":
                    OnConvertToCardFromCheckItem?.Invoke(action);
                    break;
                case "copyBoard":
                    OnCopyBoard?.Invoke(action);
                    break;
                case "copyCard":
                    OnCopyCard?.Invoke(action);
                    break;
                case "copyCommentCard":
                    OnCopyCommentCard?.Invoke(action);
                    break;
                case "createBoard":
                    OnCreateBoard?.Invoke(action);
                    break;
                case "createCard":
                    OnCreateCard?.Invoke(action);
                    break;
                case "createList":
                    OnCreateList?.Invoke(action);
                    break;
                case "createOrganization":
                    OnCreateOrganization?.Invoke(action);
                    break;
                case "deleteBoardInvitation":
                    OnDeleteBoardInvitation?.Invoke(action);
                    break;
                case "deleteCard":
                    OnDeleteCard?.Invoke(action);
                    break;
                case "deleteOrganizationInvitation":
                    OnDeleteOrganizationInvitation?.Invoke(action);
                    break;
                case "disableEnterprisePluginWhitelist":
                    OnDisableEnterprisePluginWhitelist?.Invoke(action);
                    break;
                case "disablePlugin":
                    OnDisablePlugin?.Invoke(action);
                    break;
                case "disablePowerUp":
                    OnDisablePowerUp?.Invoke(action);
                    break;
                case "emailCard":
                    OnEmailCard?.Invoke(action);
                    break;
                case "enableEnterprisePluginWhitelist":
                    OnEnableEnterprisePluginWhitelist?.Invoke(action);
                    break;
                case "enablePlugin":
                    OnEnablePlugin?.Invoke(action);
                    break;
                case "enablePowerUp":
                    OnEnablePowerUp?.Invoke(action);
                    break;
                case "makeAdminOfBoard":
                    OnMakeAdminOfBoard?.Invoke(action);
                    break;
                case "makeNormalMemberOfBoard":
                    OnMakeNormalMemberOfBoard?.Invoke(action);
                    break;
                case "makeNormalMemberOfOrganization":
                    OnMakeNormalMemberOfOrganization?.Invoke(action);
                    break;
                case "makeObserverOfBoard":
                    OnMakeObserverOfBoard?.Invoke(action);
                    break;
                case "memberJoinedTrello":
                    OnMemberJoinedTrello?.Invoke(action);
                    break;
                case "moveCardFromBoard":
                    OnMoveCardFromBoard?.Invoke(action);
                    break;
                case "moveCardToBoard":
                    OnMoveCardToBoard?.Invoke(action);
                    break;
                case "moveListFromBoard":
                    OnMoveListFromBoard?.Invoke(action);
                    break;
                case "moveListToBoard":
                    OnMoveListToBoard?.Invoke(action);
                    break;
                case "removeChecklistFromCard":
                    OnRemoveChecklistFromCard?.Invoke(action);
                    break;
                case "removeFromEnterprisePluginWhitelist":
                    OnRemoveFromEnterprisePluginWhitelist?.Invoke(action);
                    break;
                case "removeFromOrganizationBoard":
                    OnRemoveFromOrganizationBoard?.Invoke(action);
                    break;
                case "removeMemberFromCard":
                    OnRemoveMemberFromCard?.Invoke(action);
                    break;
                case "removeOrganizationFromEnterprise":
                    OnRemoveOrganizationFromEnterprise?.Invoke(action);
                    break;
                case "unconfirmedBoardInvitation":
                    OnUnconfirmedBoardInvitation?.Invoke(action);
                    break;
                case "unconfirmedOrganizationInvitation":
                    OnUnconfirmedOrganizationInvitation?.Invoke(action);
                    break;
                case "updateBoard":
                    OnUpdateBoard?.Invoke(action);
                    break;
                case "updateCard":
                    OnUpdateCard?.Invoke(action);
                    break;
                case "updateCheckItemStateOnCard":
                    OnUpdateCheckItemStateOnCard?.Invoke(action);
                    break;
                case "updateChecklist":
                    OnUpdateChecklist?.Invoke(action);
                    break;
                case "updateList":
                    OnUpdateList?.Invoke(action);
                    break;
                case "updateMember":
                    OnUpdateMember?.Invoke(action);
                    break;
                case "updateOrganization":
                    OnUpdateOrganization?.Invoke(action);
                    break;
                case "addLabelToCard":
                    OnAddLabelToCard?.Invoke(action);
                    break;
                case "copyChecklist":
                    OnCopyChecklist?.Invoke(action);
                    break;
                case "createBoardInvitation":
                    OnCreateBoardInvitation?.Invoke(action);
                    break;
                case "createBoardPreference":
                    OnCreateBoardPreference?.Invoke(action);
                    break;
                case "createCheckItem":
                    OnCreateCheckItem?.Invoke(action);
                    break;
                case "createLabel":
                    OnCreateLabel?.Invoke(action);
                    break;
                case "createOrganizationInvitation":
                    OnCreateOrganizationInvitation?.Invoke(action);
                    break;
                case "deleteAttachmentFromCard":
                    OnDeleteAttachmentFromCard?.Invoke(action);
                    break;
                case "deleteCheckItem":
                    OnDeleteCheckItem?.Invoke(action);
                    break;
                case "deleteComment":
                    OnDeleteComment?.Invoke(action);
                    break;
                case "deleteLabel":
                    OnDeleteLabel?.Invoke(action);
                    break;
                case "makeAdminOfOrganization":
                    OnMakeAdminOfOrganization?.Invoke(action);
                    break;
                case "removeLabelFromCard":
                    OnRemoveLabelFromCard?.Invoke(action);
                    break;
                case "removeMemberFromBoard":
                    OnRemoveMemberFromBoard?.Invoke(action);
                    break;
                case "removeMemberFromOrganization":
                    OnRemoveMemberFromOrganization?.Invoke(action);
                    break;
                case "updateCheckItem":
                    OnUpdateCheckItem?.Invoke(action);
                    break;
                case "updateComment":
                    OnUpdateComment?.Invoke(action);
                    break;
                case "updateLabel":
                    OnUpdateLabel?.Invoke(action);
                    break;
                case "voteOnCard":
                    OnVoteOnCard?.Invoke(action);
                    break;
                default:
                    OnUnknownActionType?.Invoke(action);
                    break;
            }
        }
    }
}