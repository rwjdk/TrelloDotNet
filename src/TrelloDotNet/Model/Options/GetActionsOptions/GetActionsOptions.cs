using System.Collections.Generic;

namespace TrelloDotNet.Model.Options.GetActionsOptions
{
    /// <summary>
    /// Options on how and what should be included on Actions
    /// </summary>
    public class GetActionsOptions
    {
        /// <summary>
        /// A set of event-types to filter by (You can see a list of event in TrelloDotNet.Model.Webhook.WebhookActionTypes)
        /// </summary>
        public List<string> Filter { get; set; }

        /// <summary>
        /// How many recent events to get back; Default 50, Max 1000
        /// </summary>
        public int Limit { get; set; } = 50;

        /// <summary>
        /// The page of results for actions
        /// </summary>
        public int Page { get; set; } = 0;

        /// <summary>
        /// An Action ID
        /// </summary>
        public string Before { get; set; } = null;

        /// <summary>
        /// An Action ID
        /// </summary>
        public string Since { get; set; } = null;

        /// <summary>
        /// Additional Parameters not supported out-of-the-box
        /// </summary>
        public List<QueryParameter> AdditionalParameters { get; set; } = new List<QueryParameter>();
    }
}