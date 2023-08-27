namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent the Attachments Fields to include
    /// </summary>
    public class AttachmentFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; }

        /// <summary>
        /// All Fields
        /// </summary>
        public static AttachmentFields All => new AttachmentFields("all");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields">'all' or any of these 'id', 'bytes', 'date', 'edgeColor', 'idMember', 'isUpload', 'mimeType', 'name', 'pos', 'previews', 'url'</param>
        public AttachmentFields(params string[] fields)
        {
            Fields = fields;
        }
    }
}