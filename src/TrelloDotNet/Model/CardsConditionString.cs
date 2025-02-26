namespace TrelloDotNet.Model
{
    /// <summary>
    /// Card conditions that are based on strings
    /// </summary>
    public enum CardsConditionString
    {
        /// <summary>
        /// String is equal to given value
        /// </summary>
        Equal = 1,

        /// <summary>
        /// String is not equal to given value
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// String contains given value
        /// </summary>
        Contains = 9,

        /// <summary>
        /// String does not contain given value
        /// </summary>
        DoNotContains = 10,

        /// <summary>
        /// String have any of the given values
        /// </summary>
        AnyOfThese = 11,

        /// <summary>
        /// String exist with all the given values (Only apply to Labels and Members)
        /// </summary>
        AllOfThese = 12,

        /// <summary>
        /// String have any of the given values
        /// </summary>
        NoneOfThese = 13,

        /// <summary>
        /// String RegEx-math the given value (match pattern)
        /// </summary>
        RegEx = 14,

        /// <summary>
        /// String start with the given value
        /// </summary>
        StartsWith = 15,

        /// <summary>
        /// String end with the given value
        /// </summary>
        EndsWith = 16,

        /// <summary>
        /// String do not start with the given value
        /// </summary>
        DoNotStartWith = 17,

        /// <summary>
        /// String do not end with the given value
        /// </summary>
        DoNotEndWith = 18,
    }
}