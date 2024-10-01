using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TrelloDotNet.Control;
using TrelloDotNet.Model.Options;

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

        public static CardUpdate DueDate(DateTimeOffset value) => new CardUpdate(CardFieldsType.Due, value);

        public static CardUpdate DueComplete(bool value) => new CardUpdate(CardFieldsType.DueComplete, value);

        public static CardUpdate Closed(bool value) => new CardUpdate(CardFieldsType.Closed, value);

        public static CardUpdate Position(decimal value) => new CardUpdate(CardFieldsType.Position, value);

        public static CardUpdate Position(NamedPosition value) => new CardUpdate(CardFieldsType.Position, value.GetJsonPropertyName());

        public static CardUpdate StartDate(DateTimeOffset value) => new CardUpdate(CardFieldsType.Start, value);

        public static CardUpdate Name(string value) => new CardUpdate(CardFieldsType.Name, value);

        public static CardUpdate Board(string value) => new CardUpdate(CardFieldsType.BoardId, value);

        public static CardUpdate Board(Board value) => new CardUpdate(CardFieldsType.BoardId, value.Id);

        public static CardUpdate List(string value) => new CardUpdate(CardFieldsType.ListId, value);

        public static CardUpdate List(List value) => new CardUpdate(CardFieldsType.ListId, value.Id);

        public static CardUpdate Description(string value) => new CardUpdate(CardFieldsType.Description, value);

        public static CardUpdate Members(List<string> memberIds) => new CardUpdate(CardFieldsType.MemberIds, memberIds);

        public static CardUpdate Members(List<Member> members) => new CardUpdate(CardFieldsType.MemberIds, members.Select(x => x.Id).ToList());

        public static CardUpdate Labels(List<string> labelIds) => new CardUpdate(CardFieldsType.LabelIds, labelIds);

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