using System.Collections.Generic;
using System.Linq;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Options.GetListOptions
{
    /// <summary>
    /// Options on how and what should be included on the cards (Example only a few fields to increase performance or more nested data to avoid more API calls)
    /// </summary>
    public class GetListOptions
    {
        /// <summary>
        /// What Kind of Lists should be included (All, Closed or Open)]
        /// </summary>
        public ListFilter? Filter { get; set; }
    }
}