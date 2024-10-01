namespace TrelloDotNet.Model.Search
{
    /// <summary>
    /// Represent a SearchMember Request
    /// </summary>
    public class SearchMemberRequest
    {
        /// <summary>
        /// The search query with a length of 1 to 16384 characters
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// The maximum number of results to return. Maximum of 20. Default 8
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Return only Members that belong to this board
        /// </summary>
        public string BoardIdFilter { get; set; }

        /// <summary>
        /// Return only Members that belong to this organization
        /// </summary>
        public string OrganizationIdFilter { get; set; }

        /// <summary>
        /// Return only Members that belong to the current Organization
        /// </summary>
        public bool OnlyOrgMembersFilter { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="searchTerm">The search query with a length of 1 to 16384 characters</param>
        public SearchMemberRequest(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}