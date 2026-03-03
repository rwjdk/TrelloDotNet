using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Name)]
        Name,

        /// <summary>
        /// FileName of the attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.FileName)]
        FileName,

        /// <summary>
        /// URL of the attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Url)]
        Url,

        /// <summary>
        /// Position of the attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        Position,

        /// <summary>
        /// Bytes of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Bytes)]
        Bytes,

        /// <summary>
        /// Date [stored UTC]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Date)]
        Date,

        /// <summary>
        /// Date [stored UTC]
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.EdgeColor)]
        EdgeColor,

        /// <summary>
        /// MemberId of the Attachment (who uploaded it)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.IdMember)]
        MemberId,

        /// <summary>
        /// MimeType of the attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.MimeType)]
        MimeType,

        /// <summary>
        /// If attachment is uploaded or not
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.IsUpload)]
        IsUpload
    }
}





