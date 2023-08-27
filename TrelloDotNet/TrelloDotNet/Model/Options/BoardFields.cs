namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent what Board-fields to include
    /// </summary>
    public class BoardFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; }

        /// <summary>
        /// Include all fields 
        /// </summary>
        public static BoardFields All => new BoardFields("all");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields">'all' or any any of these: https://developer.atlassian.com/cloud/trello/guides/rest-api/object-definitions/#board-object</param>
        public BoardFields(params string[] fields)
        {
            Fields = fields;
            //Empty
        }
    }
}