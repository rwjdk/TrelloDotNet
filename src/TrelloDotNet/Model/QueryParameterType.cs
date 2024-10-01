namespace TrelloDotNet.Model
{
    /// <summary>
    /// Type of the Query Parameter
    /// </summary>
    public enum QueryParameterType
    {
        /// <summary>
        /// String
        /// </summary>
        String,

        /// <summary>
        /// Boolean (true/false)
        /// </summary>
        Boolean,

        /// <summary>
        /// Integer
        /// </summary>
        Integer,

        /// <summary>
        /// DateTimeOffset
        /// </summary>
        DateTimeOffset,

        /// <summary>
        /// Decimal
        /// </summary>
        Decimal
    }
}