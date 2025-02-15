namespace TrelloDotNet.Model
{
    /// <summary>
    /// Card Condition that is Boolean based (DueComplete and Custom Fields of type checkbox)
    /// </summary>
    public enum CardsConditionBoolean
    {
        /// <summary>
        /// Value is equal to the indicated Bool value
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Value is not equal to the indicated Bool value
        /// </summary>
        NotEqual = 2,
    }
}