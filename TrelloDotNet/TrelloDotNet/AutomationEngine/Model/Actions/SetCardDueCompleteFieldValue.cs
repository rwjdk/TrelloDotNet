using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Object to set the Card Due Complete checkbox
    /// </summary>
    public class SetCardDueCompleteFieldValue : ISetCardFieldValue
    {
        /// <summary>
        /// Value to set (True = Checked | False = UnChecked)
        /// </summary>
        public bool Value { get; }

        /// <summary>
        /// Set the Field (if needed based on criteria you define)
        /// </summary>
        /// <param name="card">The Card to set the values on</param>
        /// <returns>If card was modified (aka need to be updated against the API)</returns>
        public bool SetIfNeeded(Card card)
        {
            card.DueComplete = Value;
            return true;
        }

        /// <inheritdoc />
        public QueryParameter GetQueryParameter(Card card)
        {
            return SetIfNeeded(card) ? new QueryParameter(CardFieldsType.DueComplete.GetJsonPropertyName(), card.DueComplete) : null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to set (True = Checked | False = UnChecked)</param>
        public SetCardDueCompleteFieldValue(bool value)
        {
            Value = value;
        }
    }
}