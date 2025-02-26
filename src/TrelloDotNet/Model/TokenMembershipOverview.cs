using System.Collections.Generic;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// An overview of what Membership the Current Token User have on various boards and workspaces
    /// </summary>
    public class TokenMembershipOverview
    {
        /// <summary>
        /// The Workspace Memberships
        /// </summary>
        public Dictionary<Organization, MembershipType> OrganizationMemberships { get; set; }

        /// <summary>
        /// The Board Memberships
        /// </summary>
        public Dictionary<Board, MembershipType> BoardMemberships { get; set; }
    }
}