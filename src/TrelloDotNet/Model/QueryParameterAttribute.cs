using System;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Attribute applied to the models various properties to indicate they are add/updateable
    /// </summary>
    internal class QueryParameterAttribute : Attribute
    {
        public bool IncludeIfNull { get; }

        public QueryParameterAttribute(bool includeIfNull = true)
        {
            IncludeIfNull = includeIfNull;
        }
    }
}