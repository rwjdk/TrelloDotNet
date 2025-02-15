namespace TrelloDotNet.Model
{
    /// <summary>
    /// Generic Card Condition
    /// </summary>
    public enum CardsCondition
    {
        /// <summary>
        /// Thing are Equal
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Thing are not Equal
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// Thing are Greater Than
        /// </summary>
        GreaterThan = 3,

        /// <summary>
        /// Thing are Less Than
        /// </summary>
        LessThan = 4,

        /// <summary>
        /// Thing are Greater Than or Equal 
        /// </summary>
        GreaterThanOrEqual = 5,

        /// <summary>
        /// Thing are Less Than or Equal
        /// </summary>
        LessThanOrEqual = 6,

        /// <summary>
        /// Thing have any value (is not blank)
        /// </summary>
        HasAnyValue = 7,

        /// <summary>
        /// Thing do not have any value (is blank)
        /// </summary>
        DoNotHaveAnyValue = 8,

        /// <summary>
        /// Thing contains the value
        /// </summary>
        Contains = 9,

        /// <summary>
        /// Thing does not contain the value
        /// </summary>
        DoNotContains = 10,

        /// <summary>
        /// Thing is any of these values
        /// </summary>
        AnyOfThese = 11,

        /// <summary>
        /// Thing have all of these values (only apply to Labels and Members)
        /// </summary>
        AllOfThese = 12,

        /// <summary>
        /// Thing have none of these values
        /// </summary>
        NoneOfThese = 13,

        /// <summary>
        /// Thing RegEx-Match the value
        /// </summary>
        RegEx = 14,

        /// <summary>
        /// Thing starts with
        /// </summary>
        StartsWith = 15,

        /// <summary>
        /// Thing ends with
        /// </summary>
        EndsWith = 16,

        /// <summary>
        /// Thing does not start with
        /// </summary>
        DoNotStartWith = 17,

        /// <summary>
        /// Thing does not end with
        /// </summary>
        DoNotEndWith = 18,

        /// <summary>
        /// Thing is between (Only apply to Date and Integer-based values)
        /// </summary>
        Between = 19,

        /// <summary>
        /// Thing is not between (Only apply to Date and Integer-based values)
        /// </summary>
        NotBetween = 20
    }
}