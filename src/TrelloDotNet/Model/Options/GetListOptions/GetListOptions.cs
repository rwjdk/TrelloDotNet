using System.Collections.Generic;
using System.Linq;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Options.GetListOptions
{
    /// <summary>
    /// Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)
    /// </summary>
    public class GetListOptions
    {
        /// <summary>
        /// All or a comma-separated list of fields. Defaults: badges, checkItemStates, closed, dateLastActivity, desc, descData, due, start, email, idBoard, idChecklists, idLabels, idList, idMembers, idShort, idAttachmentCover, manualCoverAttachment, labels, name, pos, shortUrl, url
        /// </summary>
        public CardFields CardFields { get; set; }

        /// <summary>
        /// Whether to return Board object the List is on (Default: False)
        /// </summary>
        public bool IncludeBoard { get; set; }

        /// <summary>
        /// Whether to return Card on the list is on (Default: None)
        /// </summary>
        public GetListOptionsIncludeCards IncludeCards { get; set; } = GetListOptionsIncludeCards.None;

        /// <summary>
        /// What board-fields to include if IncludeBoard are set to True
        /// </summary>
        public BoardFields BoardFields { get; set; }

        /// <summary>
        /// What Kind of Lists should be included (All, Closed or Open)]
        /// </summary>
        public ListFilter? Filter { get; set; }

        /// <summary>
        /// Order the cards returned by this (only used when multiple cards are returned)
        /// </summary>
        public CardsOrderBy? CardsOrderBy { get; set; }

        /// <summary>
        /// Add conditions of which of the data from the call is actually returned (Note: this is in-memory filtering as Trello's API is not able to filter on server-side)
        /// </summary>
        public List<CardsFilterCondition> CardsFilterConditions { get; set; } = new List<CardsFilterCondition>();

        /// <summary>
        /// Send these Additional Parameters not supported out-of-the-box (should you need to do something to the query-parameters not yet supported by this API)
        /// </summary>
        public List<QueryParameter> AdditionalParameters { get; set; } = new List<QueryParameter>();

        internal QueryParameter[] GetParameters()
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            if (CardFields != null)
            {
                parameters.Add(new QueryParameter("fields", string.Join(",", CardFields.Fields)));
            }

            if (BoardFields != null)
            {
                parameters.Add(new QueryParameter("board_fields", string.Join(",", BoardFields.Fields)));
            }

            parameters.Add(new QueryParameter("board", IncludeBoard));
            parameters.Add(new QueryParameter("cards", IncludeCards.GetJsonPropertyName()));

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