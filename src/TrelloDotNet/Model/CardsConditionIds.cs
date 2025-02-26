namespace TrelloDotNet.Model
{
    /// <summary>
    /// Cards Condition that are based on Child-object of a card (Labels, Members and Lists)
    /// </summary>
    public enum CardsConditionIds
    {
        /// <summary>
        /// Id is equal to given value
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Id is not equal to given value
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// Id is any of the given values
        /// </summary>
        AnyOfThese = 11,

        /// <summary>
        /// Ids exist with all the given values (Only apply to Labels and Members; not Lists as a Card can't be on multiple lists at the same time)
        /// </summary>
        AllOfThese = 12,

        /// <summary>
        /// Ids contains none of the given values
        /// </summary>
        NoneOfThese = 13,
    }
}