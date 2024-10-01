using System.Diagnostics;
using System.IO;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a File Upload as Attachment
    /// </summary>
    [DebuggerDisplay("{Name} (Filename: {Filename})")]
    public class AttachmentFileUpload
    {
        /// <summary>
        /// The File Stream
        /// </summary>
        public Stream Stream { get; }
        /// <summary>
        /// The Optional name of the Attachment (if not provided the Filename is used)
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The Filename
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream">The File Stream</param>
        /// <param name="filename">The Filename</param>
        /// <param name="name">The Optional name of the Attachment (if not provided the Filename is used)</param>
        public AttachmentFileUpload(Stream stream, string filename, string name = null)
        {
            Stream = stream;
            Name = name;
            Filename = filename;
        }
    }
}