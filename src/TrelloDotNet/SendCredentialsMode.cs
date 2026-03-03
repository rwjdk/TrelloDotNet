namespace TrelloDotNet
{
    /// <summary>
    /// Determines how API key and token are sent to Trello.
    /// </summary>
    public enum SendCredentialsMode
    {
        /// <summary>
        /// Send credentials as key and token query parameters.
        /// </summary>
        QueryString,

        /// <summary>
        /// Send credentials via Authorization header.
        /// </summary>
        Header
    }
}
