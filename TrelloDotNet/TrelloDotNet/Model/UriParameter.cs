using System;
using System.Globalization;

namespace TrelloDotNet.Model
{
    public class UriParameter
    {
        private readonly object _valueAsObject;
        public string Name { get; set; }
        public RequestParameterType Type { get; }

        public UriParameter(string name, string value)
        {
            Name = name;
            Type = RequestParameterType.String;
            _valueAsObject = value;
        }
        
        public UriParameter(string name, bool value)
        {
            Name = name;
            Type = RequestParameterType.Boolean;
            _valueAsObject = value;
        }
        
        public UriParameter(string name, int value)
        {
            Name = name;
            Type = RequestParameterType.Integer;
            _valueAsObject = value;
        }

        public UriParameter(string name, DateTimeOffset value)
        {
            Name = name;
            Type = RequestParameterType.DateTimeOffset;
            _valueAsObject = value;
        }

        public string GetValueAsApiFormattedString()
        {
            switch (Type)
            {
                case RequestParameterType.String:
                    return (string)_valueAsObject;
                case RequestParameterType.Boolean:
                    return ((bool)_valueAsObject).ToString().ToLowerInvariant();
                case RequestParameterType.Integer:
                    return ((int)_valueAsObject).ToString(CultureInfo.InvariantCulture);
                case RequestParameterType.DateTimeOffset:
                    return ((DateTimeOffset)_valueAsObject).ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
        }
    }
}