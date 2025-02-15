namespace TrelloDotNet.Model
{
    /// <summary>
    /// Cards Condition based on Dates
    /// </summary>
    public enum CardsConditionDate
    {
        /// <summary>
        /// Date is equal to given value
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Date is not equal to given value
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// Date is greater than given value
        /// </summary>
        GreaterThan = 3,

        /// <summary>
        /// Date is less than given value
        /// </summary>
        LessThan = 4,

        /// <summary>
        /// Date is greater than or equal to given value
        /// </summary>
        GreaterThanOrEqual = 5,

        /// <summary>
        /// Date is less or equal to than given value
        /// </summary>
        LessThanOrEqual = 6,

        /// <summary>
        /// Date have any value (is not blank)
        /// </summary>
        HasAnyValue = 7,

        /// <summary>
        /// Date do not have any value (is blank)
        /// </summary>
        DoNotHaveAnyValue = 8,

        /// <summary>
        /// Date is any of these
        /// </summary>
        AnyOfThese = 11,

        /// <summary>
        /// Date is none of these
        /// </summary>
        NoneOfThese = 13,

        /// <summary>
        /// Date is between these two values
        /// </summary>
        Between = 19,

        /// <summary>
        /// Date is not between these two values (before or after this are valid)
        /// </summary>
        NotBetween = 20
    }
}