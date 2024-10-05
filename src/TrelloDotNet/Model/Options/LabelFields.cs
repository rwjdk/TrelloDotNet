using System.Linq;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent Label-Fields to include
    /// </summary>
    public class LabelFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; set; }

        /// <summary>
        /// All Fields
        /// </summary>
        public static LabelFields All => new LabelFields("all");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">The label-fields to include</param>
        public LabelFields(params string[] fields)
        {
            Fields = fields;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">Type of Field to include</param>
        public LabelFields(params LabelFieldsType[] fields)
        {
            Fields = fields.Select(x => EnumHelper.GetJsonPropertyName(x)).ToArray();
        }
    }
}