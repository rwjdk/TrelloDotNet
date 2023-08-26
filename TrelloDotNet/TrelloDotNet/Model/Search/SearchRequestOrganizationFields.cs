namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// What Organization-fields should be included in the Search Result
    /// </summary>
    public class SearchRequestOrganizationFields
    {
        /// <summary>
        /// 'all' or a comma-separated list of billableMemberCount, desc, descData, displayName, idBoards, invitations, invited, logoHash, memberships, name, powerUps, prefs, premiumFeatures, products, url, website
        /// </summary>
        public string[] FieldNames { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldNames">'all' or a comma-separated list of billableMemberCount, desc, descData, displayName, idBoards, invitations, invited, logoHash, memberships, name, powerUps, prefs, premiumFeatures, products, url, website</param>
        public SearchRequestOrganizationFields(params string[] fieldNames)
        {
            FieldNames = fieldNames;
        }
    }
}