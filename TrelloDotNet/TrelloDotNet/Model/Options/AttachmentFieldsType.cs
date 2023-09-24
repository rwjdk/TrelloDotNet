using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Type of Attachment Field
    /// </summary>
    public enum AttachmentFieldsType
    {
        /// <summary>
        /// Name of the attachment
        /// </summary>
        [JsonPropertyName("name")]
        Name,

        /// <summary>
        /// FileName of the attachment
        /// </summary>
        [JsonPropertyName("fileName")]
        FileName,

        /// <summary>
        /// URL of the attachment
        /// </summary>
        [JsonPropertyName("url")]
        Url,

        /// <summary>
        /// Position of the attachment
        /// </summary>
        [JsonPropertyName("pos")]
        Position,

        /// <summary>
        /// Bytes of the Attachment
        /// </summary>
        [JsonPropertyName("bytes")]
        Bytes,

        /// <summary>
        /// Date [stored UTC]
        /// </summary>
        [JsonPropertyName("date")]
        Date,

        /// <summary>
        /// Date [stored UTC]
        /// </summary>
        [JsonPropertyName("edgeColor")]
        EdgeColor,

        /// <summary>
        /// MemberId of the Attachment (who uploaded it)
        /// </summary>
        [JsonPropertyName("idMember")]
        MemberId,

        /// <summary>
        /// MimeType of the attachment
        /// </summary>
        [JsonPropertyName("mimeType")]
        MimeType,

        /// <summary>
        /// If attachment is uploaded or not
        /// </summary>
        [JsonPropertyName("isUpload")]
        IsUpload
    }
}