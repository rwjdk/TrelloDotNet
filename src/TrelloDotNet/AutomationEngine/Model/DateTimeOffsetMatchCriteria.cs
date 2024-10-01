namespace TrelloDotNet.AutomationEngine.Model
{
    /// <summary>
    /// A DateTimeOffset match-criteria
    /// </summary>
    public enum DateTimeOffsetMatchCriteria
    {
        /// <summary>
        /// The value is the same
        /// </summary>
        Equal,
        
        /// <summary>
        /// The value is before the provided
        /// </summary>
        Before,
        
        /// <summary>
        /// The value is after the provided
        /// </summary>
        After
    }
}