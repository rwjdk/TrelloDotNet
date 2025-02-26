using System.Linq;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent what organization-fields to include
    /// </summary>
    public class OrganizationFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; }

        /// <summary>
        /// Include all fields 
        /// </summary>
        public static OrganizationFields All => new OrganizationFields("all");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">'all' or any of these 'name', 'desc', 'displayName', 'idBoards', 'url', 'website', 'memberships'</param>
        public OrganizationFields(params string[] fields)
        {
            Fields = fields;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">One or more field types</param>
        public OrganizationFields(params OrganizationFieldsType[] fields)
        {
            Fields = fields.Select(x => x.GetJsonPropertyName()).ToArray();
        }
    }
}