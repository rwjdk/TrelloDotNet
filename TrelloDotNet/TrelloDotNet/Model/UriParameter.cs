using System;
using System.Globalization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// URI Parameter to build the URL
    /// </summary>
    public class UriParameter
    {
        private readonly object _valueAsObject;
        /// <summary>
        /// Name of the Parameter (found on Trello API reference page: https://developer.atlassian.com/cloud/trello/rest)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Type of the parameter
        /// </summary>
        public UriParameterType Type { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public UriParameter(string name, string value)
        {
            Name = name;
            Type = UriParameterType.String;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public UriParameter(string name, bool? value)
        {
            Name = name;
            Type = UriParameterType.Boolean;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public UriParameter(string name, int? value)
        {
            Name = name;
            Type = UriParameterType.Integer;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public UriParameter(string name, DateTimeOffset? value)
        {
            Name = name;
            Type = UriParameterType.DateTimeOffset;
            _valueAsObject = value;
        }

        /// <summary>
        /// Get string-base-formatted version of the URI Parameter value
        /// </summary>
        /// <returns>Formatted string-value</returns>
        /// <exception cref="ArgumentOutOfRangeException">If wrong type in enum sent</exception>
        public string GetValueAsApiFormattedString()
        {
            if (_valueAsObject == null)
            {
                return "null";
            }
            switch (Type)
            {
                case UriParameterType.String:
                    return (string)_valueAsObject;
                case UriParameterType.Boolean:
                    return ((bool)_valueAsObject).ToString().ToLowerInvariant();
                case UriParameterType.Integer:
                    return ((int)_valueAsObject).ToString(CultureInfo.InvariantCulture);
                case UriParameterType.DateTimeOffset:
                    return ((DateTimeOffset)_valueAsObject).ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}