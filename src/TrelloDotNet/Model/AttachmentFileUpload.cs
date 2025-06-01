using System.Diagnostics;
using System.IO;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represents a file to be uploaded as an attachment.
    /// </summary>
    [DebuggerDisplay("{Name} (Filename: {Filename})")]
    public class AttachmentFileUpload
    {
        /// <summary>
        /// Gets the file stream containing the attachment data.
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Gets the optional name of the attachment. If not provided, the <see cref="Filename"/> is used.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the filename of the attachment.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentFileUpload"/> class.
        /// </summary>
        /// <param name="stream">The file stream containing the attachment data.</param>
        /// <param name="filename">The filename of the attachment.</param>
        /// <param name="name">The optional name of the attachment. If not provided, the <paramref name="filename"/> is used.</param>
        public AttachmentFileUpload(Stream stream, string filename, string name = null)
        {
            Stream = stream;
            Name = name;
            Filename = filename;
        }
    }
}