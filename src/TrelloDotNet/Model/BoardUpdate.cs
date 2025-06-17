using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
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
    public class BoardUpdate
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
        /// <returns>BoardUpdate Object</returns>
        public static BoardUpdate Name(string value) => new BoardUpdate(BoardFieldsType.Name, value ?? "");

        /// <summary>
        /// Create a Description Parameter
        /// </summary>
        /// <param name="value">Description</param>
        /// <returns>BoardUpdate Object</returns>
        public static BoardUpdate Description(string value) => new BoardUpdate(BoardFieldsType.Description, value ?? "");


        /// <summary>
        /// Create a Subscribed Parameter
        /// </summary>
        /// <param name="value">Description</param>
        /// <returns>BoardUpdate Object</returns>
        public static BoardUpdate Subscribed(bool value) => new BoardUpdate(BoardFieldsType.Subscribed, value);

        /// <summary>
        /// Create a Pinned Parameter
        /// </summary>
        /// <param name="value">Pinned</param>
        /// <returns>BoardUpdate Object</returns>
        public static BoardUpdate Pinned(bool value) => new BoardUpdate(BoardFieldsType.Pinned, value);

        /// <summary>
        /// Create an Organization Parameter
        /// </summary>
        /// <param name="value">Organization ID</param>
        /// <returns>BoardUpdate Object</returns>
        public static BoardUpdate Organization(string value) => new BoardUpdate(BoardFieldsType.OrganizationId, value);

        /// <summary>
        /// Create an Organization Parameter
        /// </summary>
        /// <param name="value">Organization</param>
        /// <returns>BoardUpdate Object</returns>
        public static BoardUpdate Organization(Organization value) => new BoardUpdate(BoardFieldsType.OrganizationId, value.Id);

        /// <summary>
        /// Create an Additional Parameter (aka one that is not a named BoardFieldsType)
        /// </summary>
        /// <param name="additionalParameter">The Additional Parameter</param>
        /// <returns>BoardUpdate Object</returns>
        public static BoardUpdate AdditionalParameter(QueryParameter additionalParameter) => new BoardUpdate(additionalParameter);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parameter">The raw Parameter</param>
        private BoardUpdate(QueryParameter parameter)
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
        private BoardUpdate(BoardFieldsType name, string value)
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
        private BoardUpdate(BoardFieldsType name, bool? value)
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