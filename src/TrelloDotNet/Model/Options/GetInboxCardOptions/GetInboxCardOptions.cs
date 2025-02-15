using System.Collections.Generic;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet.Model.Options.GetInboxCardOptions
{
    /// <summary>
    /// Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)
    /// </summary>
    public class GetInboxCardOptions
    {
        /// <summary>
        /// all or a comma-separated list of fields. Defaults: badges, checkItemStates, closed, dateLastActivity, desc, descData, due, start, email, idBoard, idChecklists, idLabels, idList, idMembers, idShort, idAttachmentCover, manualCoverAttachment, labels, name, pos, shortUrl, url
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
        /// Whether to return checklist objects for members on the card (Default: False)
        /// </summary>
        public bool IncludeChecklists { get; set; }

        /// <summary>
        /// What checklist-fields to include if IncludeChecklists are set to True
        /// </summary>
        public ChecklistFields ChecklistFields { get; set; }

        /// <summary>
        /// Whether to return CustomFieldsItem objects of the card (Default: False)
        /// </summary>
        public bool IncludeCustomFieldItems { get; set; }

        /// <summary>
        /// Limit how many object are returned (Default All) [Only used by methods where multiple objects are returned
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// A Card ID
        /// </summary>
        public string Before { get; set; } = null;

        /// <summary>
        /// A Card ID
        /// </summary>
        public string Since { get; set; } = null;

        /// <summary>
        /// What Kind of Cards should be included (Active, Closed/Archived or All)
        /// </summary>
        public CardsFilter? Filter { get; set; }

        /// <summary>
        /// Order the cards returned by this (only used when multiple cards are returned)
        /// </summary>
        public CardsOrderBy? OrderBy { get; set; }

        /// <summary>
        /// Additional Parameters not supported out-of-the-box
        /// </summary>
        public List<QueryParameter> AdditionalParameters { get; set; } = new List<QueryParameter>();

        /// <summary>
        /// Add conditions of which of the data from the call is actually returned (Note: this is in-memory filtering as Trello's API is not able to filter on server-side)
        /// </summary>
        public List<CardsFilterCondition> FilterConditions { get; set; } = new List<CardsFilterCondition>();

        internal GetCardOptions.GetCardOptions ToCardOptions()
        {
            var getCardOptions = new GetCardOptions.GetCardOptions
            {
                ActionsTypes = ActionsTypes,
                AdditionalParameters = AdditionalParameters,
                AttachmentFields = AttachmentFields,
                Before = Before,
                CardFields = CardFields,
                IncludeCustomFieldItems = IncludeCustomFieldItems,
                ChecklistFields = ChecklistFields,
                IncludeAttachments = IncludeAttachments,
                IncludeChecklists = IncludeChecklists,
                Limit = Limit,
                Since = Since,
                Filter = Filter,
                FilterConditions = FilterConditions,
                OrderBy = OrderBy
            };
            getCardOptions.AdjustFieldsBasedOnSelectedOptions();
            return getCardOptions;
        }
    }
}