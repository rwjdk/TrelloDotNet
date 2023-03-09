using System;
using TrelloDotNet.Model;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Object to set the Card Name
    /// </summary>
    public class SetCardNameFieldValue : ISetCardFieldValue
    {
        /// <summary>
        /// Value to set (Tip: You can use the keyword '**NAME**' to append in current name)
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
                    card.Name = Value?.Replace("**NAME**", card.Name);
                    return true;
                case SetFieldsOnCardValueCriteria.OnlySetIfBlank:
                    if (string.IsNullOrWhiteSpace(card.Name))
                    {
                        card.Name = Value;
                        return true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to set (Tip: You can use the keyword '**NAME**' to append in current name)</param>
        /// <param name="criteria">The criteria of when to use the Set Value</param>
        public SetCardNameFieldValue(string value, SetFieldsOnCardValueCriteria criteria = SetFieldsOnCardValueCriteria.OverwriteAnyPreviousValue)
        {
            Value = value;
            Criteria = criteria;
        }
    }
}