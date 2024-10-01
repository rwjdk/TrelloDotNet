using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// A URL Link Attachment
    /// </summary>
    [DebuggerDisplay("{Name} (Url: {Url})")]
    public class AttachmentUrlLink
    {
        /// <summary>
        /// Url of the Link
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Optional name of the link (if not provided URL will be the name)
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The named position of the Attachment: Top or Bottom
        /// </summary>
        [JsonIgnore]
        public NamedPosition? NamedPosition { internal get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">Url of the Link</param>
        /// <param name="name">Optional name of the link (if not provided URL will be the name)</param>
        public AttachmentUrlLink(string url, string name = null)
        {
            Url = url;
            Name = name;
        }
    }
}