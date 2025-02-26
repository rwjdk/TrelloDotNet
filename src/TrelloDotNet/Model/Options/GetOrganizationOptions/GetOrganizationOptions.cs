using System.Collections.Generic;

namespace TrelloDotNet.Model.Options.GetOrganizationOptions
{
    /// <summary>
    /// Options on how and what should be included on the Organizations (Example only a few fields to increase performance or more nested data to avoid more API calls)
    /// </summary>
    public class GetOrganizationOptions
    {
        /// <summary>
        /// What Organization (Workspace)-fields to include if IncludeBoard are set to True
        /// </summary>
        public OrganizationFields OrganizationFields { get; set; }

        /// <summary>
        /// Additional Parameters not supported out-of-the-box
        /// </summary>
        public List<QueryParameter> AdditionalParameters { get; set; } = new List<QueryParameter>();

        internal QueryParameter[] GetParameters()
        {
            List<QueryParameter> parameters = new List<QueryParameter>();

            if (OrganizationFields != null)
            {
                parameters.Add(new QueryParameter("fields", string.Join(",", OrganizationFields.Fields)));
            }

            parameters.AddRange(AdditionalParameters);

            return parameters.ToArray();
        }
    }
}