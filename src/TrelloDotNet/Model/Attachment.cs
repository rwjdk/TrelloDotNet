using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represents an attachment on a Card
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class Attachment
    {
        /// <summary>
        /// ID of the Attachment
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Attachment
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// File name of the Attachment
        /// </summary>
        [JsonPropertyName("fileName")]
        [JsonInclude]
        public string FileName { get; private set; }

        /// <summary>
        /// URL of the Attachment
        /// </summary>
        [JsonPropertyName("url")]
        [JsonInclude]
        public string Url { get; private set; }

        /// <summary>
        /// Position of the Attachment
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal Position { get; private set; }

        /// <summary>
        /// Size in bytes of the Attachment
        /// </summary>
        [JsonPropertyName("bytes")]
        [JsonInclude]
        public long? Bytes { get; private set; }

        /// <summary>
        /// Date the Attachment was added (stored in UTC)
        /// </summary>
        [JsonPropertyName("date")]
        [JsonInclude]
        public DateTimeOffset Date { get; private set; }

        /// <summary>
        /// Edge color of the Attachment
        /// </summary>
        [JsonPropertyName("edgeColor")]
        [JsonInclude]
        public string EdgeColor { get; private set; }

        /// <summary>
        /// ID of the Member who uploaded the Attachment
        /// </summary>
        [JsonPropertyName("idMember")]
        [JsonInclude]
        public string MemberId { get; private set; }

        /// <summary>
        /// MIME type of the Attachment
        /// </summary>
        [JsonPropertyName("mimeType")]
        [JsonInclude]
        public string MimeType { get; private set; }

        /// <summary>
        /// Indicates if the Attachment was uploaded
        /// </summary>
        [JsonPropertyName("isUpload")]
        [JsonInclude]
        public bool IsUpload { get; private set; }

        /// <summary>
        /// When the Attachment was created (stored in UTC)
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);
    }
}