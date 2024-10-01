using System;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Object to set the Card Due date
    /// </summary>
    public class SetCardDueFieldValue : ISetCardFieldValue
    {
        /// <summary>
        /// Value to set
        /// </summary>
        public DateTimeOffset? Value { get; }

        /// <summary>
        /// The criteria of when to use the Set Value
        /// </summary>
        public SetFieldsOnCardValueCriteria Criteria { get; }

        /// <summary>
        /// Set the Field (if needed based on criteria you define)
        /// </summary>
        /// <param name="card">The Card to set the values on</param>
        /// <returns>If card was modified (aka need to be updated against the API)</returns>
        public bool SetIfNeeded(Card card)
        {
            switch (Criteria)
            {
                case SetFieldsOnCardValueCriteria.OverwriteAnyPreviousValue:
                    card.Due = Value;

                    return true;
                case SetFieldsOnCardValueCriteria.OnlySetIfBlank:
                    if (!card.Due.HasValue && Value != null)
                    {
                        card.Due = Value;
                        return true;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        /// <inheritdoc />
        public QueryParameter GetQueryParameter(Card card)
        {
            return SetIfNeeded(card) ? new QueryParameter(CardFieldsType.Due.GetJsonPropertyName(), card.Due) : null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <param name="criteria">The criteria of when to use the Set Value</param>
        public SetCardDueFieldValue(DateTimeOffset? value, SetFieldsOnCardValueCriteria criteria = SetFieldsOnCardValueCriteria.OnlySetIfBlank)
        {
            Value = value;
            Criteria = criteria;
        }
    }
}