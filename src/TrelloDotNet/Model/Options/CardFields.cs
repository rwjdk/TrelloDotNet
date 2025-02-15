using System.Collections.Generic;
using System.Linq;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent Card-Fields to include
    /// </summary>
    public class CardFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; set; }

        /// <summary>
        /// The default Fields the API return if not specified
        /// </summary>
        internal static CardFields DefaultFields => new CardFields(
            CardFieldsType.DueComplete,
            CardFieldsType.Closed,
            CardFieldsType.Description,
            CardFieldsType.BoardId,
            CardFieldsType.ChecklistIds,
            CardFieldsType.ListId,
            CardFieldsType.MemberIds,
            CardFieldsType.LabelIds,
            CardFieldsType.Labels,
            CardFieldsType.IdShort,
            CardFieldsType.MembersVotedIds,
            CardFieldsType.AttachmentCover,
            CardFieldsType.Name,
            CardFieldsType.ShortUrl,
            CardFieldsType.Url,
            CardFieldsType.Start,
            CardFieldsType.Cover,
            CardFieldsType.Due,
            CardFieldsType.IsTemplate,
            CardFieldsType.Position,
            CardFieldsType.Subscribed,
            CardFieldsType.LastActivity);

        /// <summary>
        /// All Fields
        /// </summary>
        public static CardFields All => new CardFields("all");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">The card-fields to include ('all' or see list of fields here: https://developer.atlassian.com/cloud/trello/guides/rest-api/object-definitions/#card-object)</param>
        public CardFields(params string[] fields)
        {
            Fields = fields;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">Type of Field to include</param>
        public CardFields(params CardFieldsType[] fields)
        {
            Fields = fields.Select(x => x.GetJsonPropertyName()).ToArray();
        }

        internal void AddIfMissing(CardFieldsType field)
        {
            var propertyName = field.GetJsonPropertyName();
            if (!Fields.Contains(propertyName))
            {
                Fields = Fields.Union(new List<string>
                {
                    propertyName
                }).ToArray();
            }
        }
    }
}