using System;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Attribute applied to the models various properties to indicate they add add/updateable
    /// </summary>
    internal class UriParameterAttribute : Attribute
    {
        public bool IncludeIfNull { get; }

        public UriParameterAttribute(bool includeIfNull = true)
        {
            IncludeIfNull = includeIfNull;
        }
    }
}