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
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// File name of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.FileName)]
        [JsonInclude]
        public string FileName { get; private set; }

        /// <summary>
        /// URL of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Url)]
        [JsonInclude]
        public string Url { get; private set; }

        /// <summary>
        /// Position of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        [JsonInclude]
        public decimal Position { get; private set; }

        /// <summary>
        /// Size in bytes of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.Bytes)]
        [JsonInclude]
        public long? Bytes { get; private set; }

        /// <summary>
        /// Date the Attachment was added (stored in UTC)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Date)]
        [JsonInclude]
        public DateTimeOffset Date { get; private set; }

        /// <summary>
        /// Edge color of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.EdgeColor)]
        [JsonInclude]
        public string EdgeColor { get; private set; }

        /// <summary>
        /// ID of the Member who uploaded the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.IdMember)]
        [JsonInclude]
        public string MemberId { get; private set; }

        /// <summary>
        /// MIME type of the Attachment
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.MimeType)]
        [JsonInclude]
        public string MimeType { get; private set; }

        /// <summary>
        /// Indicates if the Attachment was uploaded
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.AttachmentFields.IsUpload)]
        [JsonInclude]
        public bool IsUpload { get; private set; }

        /// <summary>
        /// When the Attachment was created (stored in UTC)
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);
    }
}





