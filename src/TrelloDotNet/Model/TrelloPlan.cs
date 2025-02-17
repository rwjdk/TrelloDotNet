namespace TrelloDotNet.Model
{
    /// <summary>
    /// What Trello Plan a Board/Workspace is using (https://trello.com/pricing)
    /// </summary>
    public enum TrelloPlan
    {
        /// <summary>
        /// The Plan could not be determined
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// The Free Plan
        /// </summary>
        Free,

        /// <summary>
        /// The Standard Plan
        /// </summary>
        Standard,

        /// <summary>
        /// The Premium Plan
        /// </summary>
        Premium,

        /// <summary>
        /// The Enterprise Plan
        /// </summary>
        Enterprise
    }
}