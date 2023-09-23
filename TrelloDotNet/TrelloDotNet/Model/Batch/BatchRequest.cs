using System;

namespace TrelloDotNet.Model.Batch
{
    /// <summary>
    /// Represent a Batch-request
    /// </summary>
    public class BatchRequest
    {
        /// <summary>
        /// GET Url for the request
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// The Action that should handle what to do with the data
        /// </summary>
        public Action<BatchResult> Action { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">GET Url for the request</param>
        /// <param name="action">The Action that should handle what to do with the data</param>
        public BatchRequest(string url, Action<BatchResult> action)
        {
            Url = url;
            Action = action;
        }
    }
}