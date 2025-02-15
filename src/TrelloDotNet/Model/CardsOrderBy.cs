namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent in what order the Cards should be returned
    /// </summary>
    public enum CardsOrderBy
    {
        /// <summary>
        /// Order by Created Date (Ascending)
        /// </summary>
        CreateDateAsc,

        /// <summary>
        /// Order by Created Date (Descending)
        /// </summary>
        CreateDateDesc,

        /// <summary>
        /// Order by Start Date (Ascending)
        /// </summary>
        StartDateAsc,

        /// <summary>
        /// Order by Start Date (Descending)
        /// </summary>
        StartDateDesc,

        /// <summary>
        /// Order by Due Date (Ascending)
        /// </summary>
        DueDateAsc,

        /// <summary>
        /// Order by Due Date (Descending)
        /// </summary>
        DueDateDesc,

        /// <summary>
        /// Order by Name (Alphabetical Ascending)
        /// </summary>
        NameAsc,

        /// <summary>
        /// Order by Name (Alphabetical Descending)
        /// </summary>
        NameDesc,
    }
}