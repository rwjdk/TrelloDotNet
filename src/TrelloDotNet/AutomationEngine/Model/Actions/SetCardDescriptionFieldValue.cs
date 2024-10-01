using System;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Object to set the Card Description
    /// </summary>
    public class SetCardDescriptionFieldValue : ISetCardFieldValue
    {
        /// <summary>
        /// Value to set (Tip: You can use the keyword '**DESCRIPTION**' to append in current description)
        /// </summary>
        public string Value { get; }

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
                    card.Description = Value?.Replace("**DESCRIPTION**", card.Description);
                    return true;
                case SetFieldsOnCardValueCriteria.OnlySetIfBlank:
                    if (string.IsNullOrWhiteSpace(card.Description))
                    {
                        card.Description = Value;
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
            return SetIfNeeded(card) ? new QueryParameter(CardFieldsType.Description.GetJsonPropertyName(), card.Description) : null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to set (Tip: You can use the keyword '**DESCRIPTION**' to append in current description)</param>
        /// <param name="criteria">The criteria of when to use the Set Value</param>
        public SetCardDescriptionFieldValue(string value, SetFieldsOnCardValueCriteria criteria = SetFieldsOnCardValueCriteria.OnlySetIfBlank)
        {
            Value = value;
            Criteria = criteria;
        }
    }
}