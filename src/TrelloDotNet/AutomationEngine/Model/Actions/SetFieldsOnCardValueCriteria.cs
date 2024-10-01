namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// How the system should use you new set-value (if it should use it or not)
    /// </summary>
    public enum SetFieldsOnCardValueCriteria
    {
        /// <summary>
        /// Always set the value regardless of previous value
        /// </summary>
        OverwriteAnyPreviousValue,

        /// <summary>
        /// Only set the value if the field is blank
        /// </summary>
        OnlySetIfBlank,
    }
}