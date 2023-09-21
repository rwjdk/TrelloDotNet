namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent Checklist-Fields to include
    /// </summary>
    public class ChecklistFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; }

        /// <summary>
        /// All Fields
        /// </summary>
        public static ChecklistFields All => new ChecklistFields("all");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">The checklist-fields to include ('all' or a comma-separated list of idBoard,idCard,name,pos)</param>
        public ChecklistFields(params string[] fields)
        {
            Fields = fields;
        }
    }
}