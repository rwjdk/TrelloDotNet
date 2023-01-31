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

        public string GetValueAsString()
        {
            return (string)_valueAsObject;
        }
    }
}