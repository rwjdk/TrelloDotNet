namespace TrelloDotNet.Model
{
    /// <summary>
    /// Card Condition based on Counting things (Example number of Labels on a Card)
    /// </summary>
    public enum CardsConditionCount
    {
        /// <summary>
        /// Count is equal to given value
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Count is not equal to given value
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// Count is greater than given value
        /// </summary>
        GreaterThan = 3,

        /// <summary>
        /// Count is less than given value
        /// </summary>
        LessThan = 4,

        /// <summary>
        /// Count is greater than or equal to given value
        /// </summary>
        GreaterThanOrEqual = 5,

        /// <summary>
        /// Count is less or equal to than given value
        /// </summary>
        LessThanOrEqual = 6,
    }
}