namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent the Action Types to include
    /// </summary>
    public class ActionTypesToInclude
    {
        /// <summary>
        /// The Selected Action Types
        /// </summary>
        internal string[] ActionTypes { get; }

        /// <summary>
        /// All Action Types
        /// </summary>
        public static ActionTypesToInclude All => new ActionTypesToInclude("all");
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="actionTypes">The action types to include</param>
        public ActionTypesToInclude(params string[] actionTypes)
        {
            ActionTypes = actionTypes;
        }
    }
}