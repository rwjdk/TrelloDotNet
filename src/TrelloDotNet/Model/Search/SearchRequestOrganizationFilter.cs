namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// Filter what specific organizations should be included in the search
    /// </summary>
    public class SearchRequestOrganizationFilter
    {
        /// <summary>
        /// Ids of Organizations to search in
        /// </summary>
        public string[] OrganizationIds { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="organizationIds">Ids of Organizations to search in</param>
        public SearchRequestOrganizationFilter(params string[] organizationIds)
        {
            OrganizationIds = organizationIds;
        }
    }
}