using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    public class CardUpdate
    {
        private readonly object _valueAsObject;

        /// <summary>
        /// Name of the Parameter (found on Trello API reference page: https://developer.atlassian.com/cloud/trello/rest)
        /// </summary>
        private string _name { get; set; }

        /// <summary>
        /// Type of the parameter
        /// </summary>
        private QueryParameterType _type { get; set; }

        /// <summary>
        /// Create a Due Date Parameter
        /// </summary>
        /// <param name="value">Due Date</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate DueDate(DateTimeOffset value) => new CardUpdate(CardFieldsType.Due, value);

        /// <summary>
        /// Create a Due Completed Parameter
        /// </summary>
        /// <param name="value">Due Completed</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate DueComplete(bool value) => new CardUpdate(CardFieldsType.DueComplete, value);

        /// <summary>
        /// Create a Closed Parameter
        /// </summary>
        /// <param name="value">Closed</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Closed(bool value) => new CardUpdate(CardFieldsType.Closed, value);

        /// <summary>
        /// Create a Position Parameter
        /// </summary>
        /// <param name="value">Position</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Position(decimal value) => new CardUpdate(CardFieldsType.Position, value);

        /// <summary>
        /// Create a Position Parameter
        /// </summary>
        /// <param name="value">Position</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Position(NamedPosition value) => new CardUpdate(CardFieldsType.Position, value.GetJsonPropertyName());

        /// <summary>
        /// Create a Start Parameter
        /// </summary>
        /// <param name="value">Start</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate StartDate(DateTimeOffset value) => new CardUpdate(CardFieldsType.Start, value);

        /// <summary>
        /// Create a Name Parameter
        /// </summary>
        /// <param name="value">Name</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Name(string value) => new CardUpdate(CardFieldsType.Name, value);

        /// <summary>
        /// Create a Board Parameter
        /// </summary>
        /// <param name="value">Board</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Board(string value) => new CardUpdate(CardFieldsType.BoardId, value);

        /// <summary>
        /// Create a Board Parameter
        /// </summary>
        /// <param name="value">Board</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Board(Board value) => new CardUpdate(CardFieldsType.BoardId, value.Id);

        /// <summary>
        /// Create a List Parameter
        /// </summary>
        /// <param name="value">List</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate List(string value) => new CardUpdate(CardFieldsType.ListId, value);

        /// <summary>
        /// Create a List Parameter
        /// </summary>
        /// <param name="value">List</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate List(List value) => new CardUpdate(CardFieldsType.ListId, value.Id);

        /// <summary>
        /// Create a Description Parameter
        /// </summary>
        /// <param name="value">Description</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Description(string value) => new CardUpdate(CardFieldsType.Description, value);

        /// <summary>
        /// Create a Members Parameter
        /// </summary>
        /// <param name="memberIds">Members</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Members(List<string> memberIds) => new CardUpdate(CardFieldsType.MemberIds, memberIds);

        /// <summary>
        /// Create a Members Parameter
        /// </summary>
        /// <param name="members">Members</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Members(List<Member> members) => new CardUpdate(CardFieldsType.MemberIds, members.Select(x => x.Id).ToList());

        /// <summary>
        /// Create a Labels Parameter
        /// </summary>
        /// <param name="labelIds">Labels</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Labels(List<string> labelIds) => new CardUpdate(CardFieldsType.LabelIds, labelIds);

        /// <summary>
        /// Create a Labels Parameter
        /// </summary>
        /// <param name="labels">Labels</param>
        /// <returns>CardUpdate Object</returns>
        public static CardUpdate Labels(List<Label> labels) => new CardUpdate(CardFieldsType.LabelIds, labels.Select(x => x.Id).ToList());


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        private CardUpdate(CardFieldsType name, string value)
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
        private CardUpdate(CardFieldsType name, List<string> value)
        {
            _name = name.GetJsonPropertyName();
            _type = QueryParameterType.String;
            _valueAsObject = value != null ? string.Join(",", value) : null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        private CardUpdate(CardFieldsType name, decimal? value)
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
        private CardUpdate(CardFieldsType name, bool? value)
        {
            _name = name.GetJsonPropertyName();
            _type = QueryParameterType.Boolean;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        private CardUpdate(CardFieldsType name, int? value)
        {
            _name = name.GetJsonPropertyName();
            _type = QueryParameterType.Integer;
            _valueAsObject = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="value">Value of the Parameter</param>
        private CardUpdate(CardFieldsType name, DateTimeOffset? value)
        {
            _name = name.GetJsonPropertyName();
            _type = QueryParameterType.DateTimeOffset;
            _valueAsObject = value;
        }

        internal QueryParameter ToQueryParameter()
        {
            return new QueryParameter(_name, _type, _valueAsObject);
        }
    }
}