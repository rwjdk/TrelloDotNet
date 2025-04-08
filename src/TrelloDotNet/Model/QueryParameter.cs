using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using TrelloDotNet.Control;
using TrelloDotNet.Model.Options;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// URI Parameter to build the URL
    /// </summary>
    [DebuggerDisplay("{Name} (Type: {Type})")]
    public class QueryParameter
    {
        private object _valueAsObject;

        /// <summary>
        /// Name of the Parameter (found on Trello API reference page: https://developer.atlassian.com/cloud/trello/rest)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the parameter
        /// </summary>
        public QueryParameterType Type { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="valueAsObject">Value of the Parameter</param>
        /// <param name="type">Type of Parameter</param>
        internal QueryParameter(string name, QueryParameterType type, object valueAsObject)
        {
            Name = name;
            Type = type;
            _valueAsObject = valueAsObject;
        }

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
        public QueryParameter(string name, List<string> value)
        {
            Name = name;
            Type = QueryParameterType.String;
            _valueAsObject = value != null ? string.Join(",", value) : null;
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
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(CardFieldsType name, string value)
        {
            Name = name.GetJsonPropertyName();
            Type = QueryParameterType.String;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(CardFieldsType name, List<string> value)
        {
            Name = name.GetJsonPropertyName();
            Type = QueryParameterType.String;
            _valueAsObject = value != null ? string.Join(",", value) : null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(CardFieldsType name, decimal? value)
        {
            Name = name.GetJsonPropertyName();
            Type = QueryParameterType.Decimal;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(CardFieldsType name, bool? value)
        {
            Name = name.GetJsonPropertyName();
            Type = QueryParameterType.Boolean;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(CardFieldsType name, int? value)
        {
            Name = name.GetJsonPropertyName();
            Type = QueryParameterType.Integer;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        public QueryParameter(CardFieldsType name, DateTimeOffset? value)
        {
            Name = name.GetJsonPropertyName();
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
                    string dateAsString = ((DateTimeOffset)_valueAsObject).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:sss.fffZ", CultureInfo.InvariantCulture);
                    return dateAsString;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal string GetRawStringValue()
        {
            return _valueAsObject?.ToString();
        }

        internal object GetRawValue()
        {
            return _valueAsObject;
        }

        internal void SetRawValue(object value)
        {
            _valueAsObject = value;
        }
    }
}