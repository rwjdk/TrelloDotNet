namespace TrelloDotNet.AutomationEngine.Model.Conditions
{
    /// <summary>
    /// The constraint when checking a field value
    /// </summary>
    public enum CardFieldConditionConstraint
    {
        /// <summary>
        /// Check if the field have no value (is blank/empty)
        /// </summary>
        IsNotSet,
        
        /// <summary>
        /// Has any value (is not blank/empty)
        /// </summary>
        IsSet,
        
        /// <summary>
        /// Have a specific value (based on the match-value criteria)
        /// </summary>
        Value,
    }
}