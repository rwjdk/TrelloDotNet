namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// What Card-fields should be included in the Search Result
    /// </summary>
    public class SearchRequestCardFields
    {
        /// <summary>
        /// 'all' or a comma-separated list of: badges, checkItemStates, closed, dateLastActivity, desc, descData, due, email, idAttachmentCover, idBoard, idChecklists, idLabels, idList, idMembers, idMembersVoted, idShort, labels, manualCoverAttachment, name, pos, shortLink, shortUrl, subscribed, url
        /// </summary>
        public string[] FieldNames { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldNames">'all' or a comma-separated list of: badges, checkItemStates, closed, dateLastActivity, desc, descData, due, email, idAttachmentCover, idBoard, idChecklists, idLabels, idList, idMembers, idMembersVoted, idShort, labels, manualCoverAttachment, name, pos, shortLink, shortUrl, subscribed, url</param>
        public SearchRequestCardFields(params string[] fieldNames)
        {
            FieldNames = fieldNames;
        }
    }
}