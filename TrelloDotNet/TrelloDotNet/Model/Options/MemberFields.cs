namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent what member-fields to include
    /// </summary>
    public class MemberFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; }

        /// <summary>
        /// Include all fields 
        /// </summary>
        public static MemberFields All => new MemberFields("all");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields">'all' or any any of these 'id', 'avatarUrl', 'initials', 'fullName', 'username', 'confirmed', 'idOrganizations', 'idBoards'</param>
        public MemberFields(params string[] fields)
        {
            Fields = fields;
            //Empty
        }
    }
}