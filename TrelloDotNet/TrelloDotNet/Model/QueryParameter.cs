﻿using System;
using System.Diagnostics;
using System.Globalization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// URI Parameter to build the URL
    /// </summary>
    [DebuggerDisplay("{Name} (Type: {Type})")]
    public class QueryParameter
    {
        private readonly object _valueAsObject;
        /// <summary>
        /// Name of the Parameter (found on Trello API reference page: https://developer.atlassian.com/cloud/trello/rest)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Type of the parameter
        /// </summary>
        public QueryParameterType Type { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(string name, string value)
        {
            Name = name;
            Type = QueryParameterType.String;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(string name, decimal? value)
        {
            Name = name;
            Type = QueryParameterType.Decimal;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(string name, bool? value)
        {
            Name = name;
            Type = QueryParameterType.Boolean;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(string name, int? value)
        {
            Name = name;
            Type = QueryParameterType.Integer;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(string name, DateTimeOffset? value)
        {
            Name = name;
            Type = QueryParameterType.DateTimeOffset;
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
                case QueryParameterType.String:
                    return System.Web.HttpUtility.UrlEncode((string)_valueAsObject);
                case QueryParameterType.Boolean:
                    return ((bool)_valueAsObject).ToString().ToLowerInvariant();
                case QueryParameterType.Integer:
                    return ((int)_valueAsObject).ToString(CultureInfo.InvariantCulture);
                case QueryParameterType.Decimal:
                    return ((decimal)_valueAsObject).ToString(CultureInfo.InvariantCulture);
                case QueryParameterType.DateTimeOffset:
                    return ((DateTimeOffset)_valueAsObject).ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}