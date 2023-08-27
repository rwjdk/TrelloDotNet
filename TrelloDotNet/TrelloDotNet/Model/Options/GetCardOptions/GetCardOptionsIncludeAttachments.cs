namespace TrelloDotNet.Model.Options.GetCardOptions
{
    /// <summary>
    /// Represent if Attachments should be included or not
    /// </summary>
    public enum GetCardOptionsIncludeAttachments
    {
        /// <summary>
        /// Include attachments
        /// </summary>
        True,
        /// <summary>
        /// Do not include attachments
        /// </summary>
        False,
        /// <summary>
        /// Only include attachments that are used as cover on the cards
        /// </summary>
        Cover
    }
}