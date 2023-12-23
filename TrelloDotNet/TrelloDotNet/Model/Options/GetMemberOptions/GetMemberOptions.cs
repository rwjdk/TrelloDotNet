using System.Collections.Generic;

namespace TrelloDotNet.Model.Options.GetMemberOptions
{
    /// <summary>
    /// Options when retrieving members
    /// </summary>
    public class GetMemberOptions
    {
        /// <summary>
        /// all or a comma-separated list of fields.
        /// </summary>
        public MemberFields MemberFields { get; set; }

        internal QueryParameter[] GetParameters()
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            if (MemberFields != null)
            {
                parameters.Add(new QueryParameter("fields", string.Join(",", MemberFields.Fields)));
            }

            return parameters.ToArray();
        }
    }
}