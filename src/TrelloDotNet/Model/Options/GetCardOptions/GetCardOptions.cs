using System;
using System.Collections.Generic;
using System.Linq;

namespace TrelloDotNet.Model.Options.GetCardOptions
{
    /// <summary>
    /// Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)
    /// </summary>
    public class GetCardOptions
    {
        /// <summary>
        /// All or a comma-separated list of fields. Defaults: badges, checkItemStates, closed, dateLastActivity, desc, descData, due, start, email, idBoard, idChecklists, idLabels, idList, idMembers, idShort, idAttachmentCover, manualCoverAttachment, labels, name, pos, shortUrl, url
        /// </summary>
        public CardFields CardFields { get; set; }

        /// <summary>
        /// Provide one or more Card-action types there (TrelloDotNet.Model.Webhook.WebhookActionTypes) to get them included with the Card
        /// </summary>
        public ActionTypesToInclude ActionsTypes { get; set; }

        /// <summary>
        /// Controls if cards should include their attachments (Default: False)
        /// </summary>
        public GetCardOptionsIncludeAttachments IncludeAttachments { get; set; } = GetCardOptionsIncludeAttachments.False;

        /// <summary>
        /// What attachments-fields to include if IncludeAttachments are set to 'True' or 'Cover'
        /// </summary>
        public AttachmentFields AttachmentFields { get; set; }

        /// <summary>
        /// Whether to return member objects for members on the card (Default: False)
        /// </summary>
        public bool IncludeMembers { get; set; }

        /// <summary>
        /// Whether to return checklist objects for members on the card (Default: False)
        /// </summary>
        public bool IncludeChecklists { get; set; }

        /// <summary>
        /// What member-fields to include if IncludeMembers are set to True
        /// </summary>
        public MemberFields MemberFields { get; set; }

        /// <summary>
        /// What member-fields to include if IncludeMemberVotes are set to True
        /// </summary>
        public MemberFields MembersVotedFields { get; set; }

        /// <summary>
        /// What checklist-fields to include if IncludeChecklists are set to True
        /// </summary>
        public ChecklistFields ChecklistFields { get; set; }

        /// <summary>
        /// What stickers-fields to include if IncludeStickers are set to True
        /// </summary>
        public StickerFields StickerFields { get; set; }

        /// <summary>
        /// Whether to return Board object the card is on (Default: False)
        /// </summary>
        public bool IncludeBoard { get; set; }

        /// <summary>
        /// Whether to return List object the card is in (Default: False)
        /// </summary>
        public bool IncludeList { get; set; }

        /// <summary>
        /// Whether to return Plugin object of the card (Default: False)
        /// </summary>
        public bool IncludePluginData { get; set; }

        /// <summary>
        /// Whether to return Sticker objects of the card (Default: False)
        /// </summary>
        public bool IncludeStickers { get; set; }

        /// <summary>
        /// Whether to return MemberVotes objects of the card (Default: False)
        /// </summary>
        public bool IncludeMemberVotes { get; set; }

        /// <summary>
        /// Whether to return CustomFieldsItem objects of the card (Default: False)
        /// </summary>
        public bool IncludeCustomFieldItems { get; set; }

        /// <summary>
        /// What board-fields to include if IncludeBoard are set to True
        /// </summary>
        public BoardFields BoardFields { get; set; }

        /// <summary>
        /// Limit how many object are returned (Default All) [Only used by methods where multiple objects are returned
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Pagination (Only return cards before this Card Id was created)
        /// </summary>
        public string Before { get; set; } = null;

        /// <summary>
        /// Pagination (Only return cards since this Card Id was created)
        /// </summary>
        public string Since { get; set; } = null;

        /// <summary>
        /// What Kind of Cards should be included (Active, Closed/Archived or All) [Only used on GetCardsForBoard(...)]
        /// </summary>
        public CardsFilter? Filter { get; set; }

        /// <summary>
        /// Order the cards returned by this (only used when multiple cards are returned)
        /// </summary>
        public CardsOrderBy? OrderBy { get; set; }

        /// <summary>
        /// Add conditions of which of the data from the call is actually returned (Note: this is in-memory filtering as Trello's API is not able to filter on server-side)
        /// </summary>
        public List<CardsFilterCondition> FilterConditions { get; set; } = new List<CardsFilterCondition>();

        /// <summary>
        /// Send these Additional Parameters not supported out-of-the-box (should you need to do something to the query-parameters not yet supported by this API)
        /// </summary>
        public List<QueryParameter> AdditionalParameters { get; set; } = new List<QueryParameter>();

        internal QueryParameter[] GetParameters(bool multipleObjects)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            if (CardFields != null)
            {
                parameters.Add(new QueryParameter("fields", string.Join(",", CardFields.Fields)));
            }

            if (ActionsTypes != null)
            {
                parameters.Add(new QueryParameter("actions", string.Join(",", ActionsTypes.ActionTypes)));
            }

            if (AttachmentFields != null)
            {
                parameters.Add(new QueryParameter("attachment_fields", string.Join(",", AttachmentFields.Fields)));
            }

            if (MemberFields != null)
            {
                parameters.Add(new QueryParameter("member_fields", string.Join(",", MemberFields.Fields)));
            }

            if (BoardFields != null)
            {
                parameters.Add(new QueryParameter("board_fields", string.Join(",", BoardFields.Fields)));
            }

            if (ChecklistFields != null)
            {
                parameters.Add(new QueryParameter("checklist_fields", string.Join(",", ChecklistFields.Fields)));
            }

            if (StickerFields != null)
            {
                parameters.Add(new QueryParameter("sticker_fields", string.Join(",", StickerFields.Fields)));
            }

            if (MembersVotedFields != null)
            {
                parameters.Add(new QueryParameter("memberVoted_fields", string.Join(",", MembersVotedFields.Fields)));
            }

            parameters.Add(new QueryParameter("members", IncludeMembers));
            parameters.Add(new QueryParameter("board", IncludeBoard));
            parameters.Add(new QueryParameter("list", IncludeList));
            parameters.Add(new QueryParameter("checklists", IncludeChecklists ? "all" : "none"));
            parameters.Add(new QueryParameter("pluginData", IncludePluginData));
            parameters.Add(new QueryParameter("stickers", IncludeStickers));
            parameters.Add(new QueryParameter("customFieldItems", IncludeCustomFieldItems));
            parameters.Add(new QueryParameter("membersVoted", IncludeMemberVotes));

            switch (IncludeAttachments)
            {
                case GetCardOptionsIncludeAttachments.True:
                    parameters.Add(new QueryParameter("attachments", true));
                    break;
                case GetCardOptionsIncludeAttachments.False:
                    parameters.Add(new QueryParameter("attachments", false));
                    break;
                case GetCardOptionsIncludeAttachments.Cover:
                    parameters.Add(new QueryParameter("attachments", "cover"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (multipleObjects)
            {
                if (Limit.HasValue)
                {
                    parameters.Add(new QueryParameter("limit", Limit.Value));
                }

                if (Before != null)
                {
                    parameters.Add(new QueryParameter("before", Before));
                }

                if (Since != null)
                {
                    parameters.Add(new QueryParameter("since", Since));
                }
            }

            parameters.AddRange(AdditionalParameters);

            return parameters.ToArray();
        }

        internal void AdjustFieldsBasedOnSelectedOptions()
        {
            List<CardFieldsType> cardFieldsNeededForFilterAndOrder = new List<CardFieldsType>();
            switch (OrderBy)
            {
                case CardsOrderBy.StartDateAsc:
                case CardsOrderBy.StartDateDesc:
                    cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Start);
                    break;
                case CardsOrderBy.DueDateAsc:
                case CardsOrderBy.DueDateDesc:
                    cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Due);
                    break;
                case CardsOrderBy.NameAsc:
                case CardsOrderBy.NameDesc:
                    cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Name);
                    break;
            }

            if (FilterConditions != null && FilterConditions.Count != 0)
            {
                foreach (CardsFilterCondition condition in FilterConditions)
                {
                    switch (condition.Field)
                    {
                        case CardsConditionField.Name:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Name);
                            break;
                        case CardsConditionField.ListId:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.ListId);
                            break;
                        case CardsConditionField.ListName:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.ListId);
                            IncludeList = true;
                            break;
                        case CardsConditionField.LabelId:
                        case CardsConditionField.LabelName:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.LabelIds);
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Labels);
                            break;
                        case CardsConditionField.MemberId:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.MemberIds);
                            break;
                        case CardsConditionField.MemberName:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.MemberIds);
                            IncludeMembers = true;
                            break;
                        case CardsConditionField.Description:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Description);
                            break;
                        case CardsConditionField.Due:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Due);
                            break;
                        case CardsConditionField.DueWithNoDueComplete:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Due);
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.DueComplete);
                            break;
                        case CardsConditionField.Start:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Start);
                            break;
                        case CardsConditionField.CustomField:
                            IncludeCustomFieldItems = true;
                            break;
                        case CardsConditionField.DueComplete:
                            cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.DueComplete);
                            break;
                    }
                }
            }

            // ReSharper disable once InvertIf
            if (cardFieldsNeededForFilterAndOrder.Count > 0)
            {
                if (CardFields == null)
                {
                    CardFields = CardFields.DefaultFields;
                }

                foreach (CardFieldsType cardFieldsType in cardFieldsNeededForFilterAndOrder.Distinct())
                {
                    CardFields.AddIfMissing(cardFieldsType);
                }
            }
        }
    }
}