using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent an attachment
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class Attachment
    {
        /// <summary>
        /// Id of the Attachment
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the attachment
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// FileName of the attachment
        /// </summary>
        [JsonPropertyName("fileName")]
        [JsonInclude]
        public string FileName { get; private set; }

        /// <summary>
        /// URL of the attachment
        /// </summary>
        [JsonPropertyName("url")]
        [JsonInclude]
        public string Url { get; private set; }

        /// <summary>
        /// Position of the attachment
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal Position { get; private set; }
        
        /// <summary>
        /// Bytes of the Attachment
        /// </summary>
        [JsonPropertyName("bytes")]
        [JsonInclude]
        public long? Bytes { get; private set; }
        
        /// <summary>
        /// Date [stored UTC]
        /// </summary>
        [JsonPropertyName("date")]
        [JsonInclude]
        public DateTimeOffset Date { get; private set; }

        /// <summary>
        /// Date [stored UTC]
        /// </summary>
        [JsonPropertyName("edgeColor")]
        [JsonInclude]
        public string EdgeColor { get; private set; }

        /// <summary>
        /// MemberId of the Attachment (who uploaded it)
        /// </summary>
        [JsonPropertyName("idMember")]
        [JsonInclude]
        public string MemberId { get; private set; }
        
        /// <summary>
        /// MimeType of the attachment
        /// </summary>
        [JsonPropertyName("mimeType")]
        [JsonInclude]
        public string MimeType { get; private set; }

        /// <summary>
        /// If attachment is uploaded or not
        /// </summary>
        [JsonPropertyName("isUpload")]
        [JsonInclude]
        public bool IsUpload { get; private set; }

        /// <summary>
        /// When the Attachment was created [stored in UTC]
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);
    }
}