using System.Collections.Generic;
using System.Linq;

namespace TrelloDotNet.Model.Batch
{
    /// <summary>
    /// Represent a Batch-result 
    /// </summary>
    public class BatchResult
    {
        /// <summary>
        /// The number of the URLs sent that resulted in failure (not 200 return)
        /// </summary>
        public int ErrorCount => Results.Count(x => x.StatusCode != 200);
        /// <summary>
        /// The results of the BatchRequest
        /// </summary>
        public List<BatchResultForUrl> Results { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="results">The results of the BatchRequest</param>
        public BatchResult(List<BatchResultForUrl> results)
        {
            Results = results;
        }
    }
}