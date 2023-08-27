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
        internal string[] Fields { get; }

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
    }
}