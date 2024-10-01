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
    }
}