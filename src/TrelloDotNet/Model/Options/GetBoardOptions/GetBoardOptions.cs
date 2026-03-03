using System;
using System.Collections.Generic;
using System.Linq;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Options.GetBoardOptions
{
    /// <summary>
    /// Options on how and what should be included on the Boards (Example only a few fields to increase performance or more nested data to avoid more API calls)
    /// </summary>
    public class GetBoardOptions
    {
        /// <summary>
        /// Provide one or more action types there (TrelloDotNet.Model.Webhook.WebhookActionTypes) to get them included with the Board
        /// </summary>
        public ActionTypesToInclude ActionsTypes { get; set; }

        /// <summary>
        /// Filter of various Boards (Default: All)
        /// </summary>
        public GetBoardOptionsTypesOfBoardsToInclude TypesOfBoardsToInclude { get; set; } = GetBoardOptionsTypesOfBoardsToInclude.All;

        /// <summary>
        /// Whether to return Cards on the board (Default: None) [NOTE: This option only works when retrieving a single board. On methods 'GetBoardsForMemberAsync', 'GetBoardsCurrentTokenCanAccessAsync' and 'GetBoardsInOrganization' it have no effect]
        /// </summary>
        public GetBoardOptionsIncludeCards IncludeCards { get; set; }

        /// <summary>
        /// Whether to return Lists on the board (Default: None)
        /// </summary>
        public GetBoardOptionsIncludeLists IncludeLists { get; set; }

        /// <summary>
        /// Whether to return the Organization of the board (Default: False)
        /// </summary>
        public bool IncludeOrganization { get; set; }

        /// <summary>
        /// Whether to return Labels on the board (Default: False)
        /// </summary>
        public bool IncludeLabels { get; set; }

        /// <summary>
        /// What board-fields to include if IncludeBoard are set to True
        /// </summary>
        public BoardFields BoardFields { get; set; }

        /// <summary>
        /// What Organization (Workspace)-fields to include if IncludeBoard are set to True
        /// </summary>
        public OrganizationFields OrganizationFields { get; set; }

        /// <summary>
        /// What card-fields to include if IncludeCards are set to other than None. all or a comma-separated list of fields. Defaults: badges, checkItemStates, closed, dateLastActivity, desc, descData, due, start, email, idBoard, idChecklists, idLabels, idList, idMembers, idShort, idAttachmentCover, manualCoverAttachment, labels, name, pos, shortUrl, url
        /// </summary>
        public CardFields CardFields { get; set; }

        /// <summary>
        /// Whether to return Plugin object of the card (Default: False)
        /// </summary>
        public bool IncludePluginData { get; set; }

        /// <summary>
        /// What types of boards are returned (Default: All)
        /// </summary> 
        public GetBoardOptionsFilter Filter { get; set; } = GetBoardOptionsFilter.All;

        /// <summary>
        /// Additional Parameters not supported out-of-the-box
        /// </summary>
        public List<QueryParameter> AdditionalParameters { get; set; } = new List<QueryParameter>();

        /// <summary>
        /// Order the cards returned by this (only used when multiple cards are returned)
        /// </summary>
        public CardsOrderBy? CardsOrderBy { get; set; }

        /// <summary>
        /// Add conditions of which of the data from the call is actually returned (Note: this is in-memory filtering as Trello's API is not able to filter on server-side)
        /// </summary>
        public List<CardsFilterCondition> CardsFilterConditions { get; set; } = new List<CardsFilterCondition>();

        internal QueryParameter[] GetParameters()
        {
            List<QueryParameter> parameters = new List<QueryParameter>();

            if (BoardFields != null)
            {
                parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.Fields, string.Join(",", BoardFields.Fields)));
            }

            if (CardFields != null)
            {
                parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.CardFields, string.Join(",", CardFields.Fields)));
            }

            if (OrganizationFields != null)
            {
                parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.OrganizationFields, string.Join(",", OrganizationFields.Fields)));
            }

            if (ActionsTypes != null)
            {
                parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.Actions, string.Join(",", ActionsTypes.ActionTypes)));
            }

            if (TypesOfBoardsToInclude != GetBoardOptionsTypesOfBoardsToInclude.All)
            {
                var selected = Enum.GetValues(TypesOfBoardsToInclude.GetType()).Cast<Enum>().Where(TypesOfBoardsToInclude.HasFlag).ToArray();

                parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.Filter, string.Join(",", selected.Select(x => x.GetJsonPropertyName()))));
            }

            parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.Lists, IncludeLists.GetJsonPropertyName()));
            parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.Organization, IncludeOrganization));
            parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.Filter, Filter.GetJsonPropertyName()));
            parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.Cards, IncludeCards.GetJsonPropertyName()));
            parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.Labels, IncludeLabels ? "all" : "none"));
            parameters.Add(new QueryParameter(Constants.TrelloIds.QueryParameterNames.PluginData, IncludePluginData));
            parameters.AddRange(AdditionalParameters);

            return parameters.ToArray();
        }

        internal void AdjustFieldsBasedOnSelectedOptions()
        {
            List<CardFieldsType> cardFieldsNeededForFilterAndOrder = new List<CardFieldsType>();
            switch (CardsOrderBy)
            {
                case Model.CardsOrderBy.StartDateAsc:
                case Model.CardsOrderBy.StartDateDesc:
                    cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Start);
                    break;
                case Model.CardsOrderBy.DueDateAsc:
                case Model.CardsOrderBy.DueDateDesc:
                    cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Due);
                    break;
                case Model.CardsOrderBy.NameAsc:
                case Model.CardsOrderBy.NameDesc:
                    cardFieldsNeededForFilterAndOrder.Add(CardFieldsType.Name);
                    break;
            }

            if (CardsFilterConditions != null && CardsFilterConditions.Count != 0)
            {
                foreach (CardsFilterCondition condition in CardsFilterConditions)
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


