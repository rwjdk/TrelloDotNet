namespace TrelloDotNet.Model
{
    /// <summary>
    /// Card Conditions based on numbers (Integer and Decimal based values)
    /// </summary>
    public enum CardsConditionNumber
    {
        /// <summary>
        /// Number is equal to given value
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Number is not equal to given value
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// Number is greater than given value
        /// </summary>
        GreaterThan = 3,

        /// <summary>
        /// Number is less than given value
        /// </summary>
        LessThan = 4,

        /// <summary>
        /// Number is greater than or equal to given value
        /// </summary>
        GreaterThanOrEqual = 5,

        /// <summary>
        /// Number is less or equal to than given value
        /// </summary>
        LessThanOrEqual = 6,

        /// <summary>
        /// Number have any value (is not blank)
        /// </summary>
        HasAnyValue = 7,

        /// <summary>
        /// Number do not have any value (is blank)
        /// </summary>
        DoNotHaveAnyValue = 8,

        /// <summary>
        /// Number is any of the given values
        /// </summary>
        AnyOfThese = 11,

        /// <summary>
        /// Number is none of the given values
        /// </summary>
        NoneOfThese = 13,

        /// <summary>
        /// Number is between these two values
        /// </summary>
        Between = 19,

        /// <summary>
        /// Number is not between these values
        /// </summary>
        NotBetween = 20
    }
}