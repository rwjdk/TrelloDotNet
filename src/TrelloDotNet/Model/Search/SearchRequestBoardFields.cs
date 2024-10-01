namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// What Board-fields should be included in the Search Result
    /// </summary>
    public class SearchRequestBoardFields
    {
        /// <summary>
        /// 'all' or any of these: closed, dateLastActivity, dateLastView, desc, descData, idOrganization, invitations, invited, labelNames, memberships, name, pinned, powerUps, prefs, shortLink, shortUrl, starred, subscribed, url
        /// </summary>
        public string[] FieldNames { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldNames">'all' or any of these: closed, dateLastActivity, dateLastView, desc, descData, idOrganization, invitations, invited, labelNames, memberships, name, pinned, powerUps, prefs, shortLink, shortUrl, starred, subscribed, url</param>
        public SearchRequestBoardFields(params string[] fieldNames)
        {
            FieldNames = fieldNames;
        }
    }
}