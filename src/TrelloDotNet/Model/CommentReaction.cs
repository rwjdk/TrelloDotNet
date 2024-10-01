using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Comment Reaction
    /// </summary>
    public class CommentReaction
    {
        /// <summary>
        /// The Id
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The Member Id
        /// </summary>
        [JsonPropertyName("idMember")]
        [JsonInclude]
        public string MemberId { get; private set; }

        /// <summary>
        /// The Parent CommentActionId
        /// </summary>
        [JsonPropertyName("idModel")]
        [JsonInclude]
        public string CommentId { get; private set; }

        /// <summary>
        /// The Id of the Emojis
        /// </summary>
        [JsonPropertyName("idEmoji")]
        [JsonInclude]
        public string EmojiId { get; private set; }

        /// <summary>
        /// The Member
        /// </summary>
        [JsonPropertyName("member")]
        [JsonInclude]
        public Member Member { get; private set; }

        /// <summary>
        /// The Emojis
        /// </summary>
        [JsonPropertyName("emoji")]
        [JsonInclude]
        public Emoji Emoji { get; private set; }
    }
}