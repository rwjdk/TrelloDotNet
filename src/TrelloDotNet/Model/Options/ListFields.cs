using System.Linq;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent what list-fields to include
    /// </summary>
    public class ListFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; }

        /// <summary>
        /// Include all fields 
        /// </summary>
        public static ListFields All => new ListFields("all");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">'all' or any of these 'name', 'closed', 'idBoard', 'color', 'pos', 'subscribed'</param>
        public ListFields(params string[] fields)
        {
            Fields = fields;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">One or more field types</param>
        public ListFields(params ListFieldsType[] fields)
        {
            Fields = fields.Select(x => x.GetJsonPropertyName()).ToArray();
        }
    }
}