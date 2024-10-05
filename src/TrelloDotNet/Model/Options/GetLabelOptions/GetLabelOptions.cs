using System.Collections.Generic;
using System;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet.Model.Options.GetLabelOptions
{
    /// <summary>
    /// Options on how and what should be included on the Labels
    /// </summary>
    public class GetLabelOptions
    {
        /// <summary>
        /// How many labels to return (Min: 0, Default: 50, Max: 1000) [Used by GetLabelsOfBoardAsync]
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// all or a comma-separated list of fields.
        /// </summary>
        public LabelFields LabelFields { get; set; }

        /// <summary>
        /// Additional Parameters not supported out-of-the-box
        /// </summary>
        public List<QueryParameter> AdditionalParameters { get; set; } = new List<QueryParameter>();

        internal QueryParameter[] GetParameters()
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            if (LabelFields != null)
            {
                parameters.Add(new QueryParameter("fields", string.Join(",", LabelFields.Fields)));
            }

            if (Limit != null)
            {
                parameters.Add(new QueryParameter("limit", Limit.Value));
            }

            parameters.AddRange(AdditionalParameters);

            return parameters.ToArray();
        }
    }
}