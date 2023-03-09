using System;
using TrelloDotNet.Model;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Object to set the Card Start Date
    /// </summary>
    public class SetCardStartFieldValue : ISetCardFieldValue
    {
        /// <summary>
        /// Value to Set
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
                    card.Start = Value;
                    return true;
                case SetFieldsOnCardValueCriteria.OnlySetIfBlank:
                    if (!card.Start.HasValue && Value != null)
                    {
                        card.Start = Value;
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
        /// <param name="value">Value to set</param>
        /// <param name="criteria">The criteria of when to use the Set Value</param>
        public SetCardStartFieldValue(DateTimeOffset? value, SetFieldsOnCardValueCriteria criteria = SetFieldsOnCardValueCriteria.OnlySetIfBlank)
        {
            Value = value;
            Criteria = criteria;
        }
    }
}