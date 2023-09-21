using System;
using System.Collections.Generic;

namespace TrelloDotNet.Model.Options.GetCardOptions
{
    /// <summary>
    /// Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)
    /// </summary>
    public class GetCardOptions
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
        /// What checklist-fields to include if IncludeChecklists are set to True
        /// </summary>
        public ChecklistFields ChecklistFields { get; set; }

        /// <summary>
        /// Whether to return Board object the card is on (Default: False)
        /// </summary>
        public bool IncludeBoard { get; set; }

        /// <summary>
        /// Whether to return List object the card is in (Default: False)
        /// </summary>
        public bool IncludeList { get; set; }

        /// <summary>
        /// What board-fields to include if IncludeBoard are set to True
        /// </summary>
        public BoardFields BoardFields { get; set; }

        internal QueryParameter[] GetParameters()
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
            
            parameters.Add(new QueryParameter("members", IncludeMembers));
            parameters.Add(new QueryParameter("board", IncludeBoard));
            parameters.Add(new QueryParameter("list", IncludeList));
            parameters.Add(new QueryParameter("checklists", IncludeChecklists ? "all" : "none"));

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

            return parameters.ToArray();
        }
    }
}