using System.Diagnostics;
using TrelloDotNet.Control;
using TrelloDotNet.Model.Options;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace TrelloDotNet.Model
{
    /// <summary>
    /// URI Parameter to build the URL
    /// </summary>
    [DebuggerDisplay("{_name} (Type: {_type})")]
    public class ListUpdate
    {
        private readonly object _valueAsObject;

        /// <summary>
        /// Name of the Parameter (found on Trello API reference page: https://developer.atlassian.com/cloud/trello/rest)
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Type of the parameter
        /// </summary>
        private readonly QueryParameterType _type;

        /// <summary>
        /// Create a Name Parameter
        /// </summary>
        /// <param name="value">Name</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate Name(string value) => new ListUpdate(ListFieldsType.Name, value ?? "");

        /// <summary>
        /// Create a Closed Parameter
        /// </summary>
        /// <param name="value">Closed</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate Closed(bool value) => new ListUpdate(ListFieldsType.Closed, value);


        /// <summary>
        /// Create a Subscribed Parameter
        /// </summary>
        /// <param name="value">Description</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate Subscribed(bool value) => new ListUpdate(ListFieldsType.Subscribed, value);

        /// <summary>
        /// Create a Position Parameter
        /// </summary>
        /// <param name="value">Position</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate Position(decimal value) => new ListUpdate(ListFieldsType.Position, value);

        /// <summary>
        /// Create a Position Parameter
        /// </summary>
        /// <param name="value">Position</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate Position(NamedPosition value) => new ListUpdate(ListFieldsType.Position, value.GetJsonPropertyName());

        /// <summary>
        /// Create a Board Parameter
        /// </summary>
        /// <param name="value">Board</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate Board(string value) => new ListUpdate(ListFieldsType.BoardId, value);

        /// <summary>
        /// Create a Board Parameter
        /// </summary>
        /// <param name="value">Board</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate Board(Board value) => new ListUpdate(ListFieldsType.BoardId, value.Id);

        /// <summary>
        /// Create a Color Parameter
        /// </summary>
        /// <param name="value">Color</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate Color(ListColor? value) => new ListUpdate(ListFieldsType.Color, value?.GetJsonPropertyName() ?? ListColor.Gray.GetJsonPropertyName());

        /// <summary>
        /// Create an Additional Parameter (aka one that is not a named BoardFieldsType)
        /// </summary>
        /// <param name="additionalParameter">The Additional Parameter</param>
        /// <returns>ListUpdate Object</returns>
        public static ListUpdate AdditionalParameter(QueryParameter additionalParameter) => new ListUpdate(additionalParameter);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parameter">The raw Parameter</param>
        private ListUpdate(QueryParameter parameter)
        {
            _name = parameter.Name;
            _type = parameter.Type;
            _valueAsObject = parameter.GetRawValue();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        private ListUpdate(ListFieldsType name, string value)
        {
            _name = name.GetJsonPropertyName();
            _type = QueryParameterType.String;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        private ListUpdate(ListFieldsType name, decimal? value)
        {
            _name = name.GetJsonPropertyName();
            _type = QueryParameterType.Decimal;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        private ListUpdate(ListFieldsType name, bool? value)
        {
            _name = name.GetJsonPropertyName();
            _type = QueryParameterType.Boolean;
            _valueAsObject = value;
        }

        internal QueryParameter ToQueryParameter()
        {
            return new QueryParameter(_name, _type, _valueAsObject);
        }
    }
}