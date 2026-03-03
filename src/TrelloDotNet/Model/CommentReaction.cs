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
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The Member Id
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.IdMember)]
        [JsonInclude]
        public string MemberId { get; private set; }

        /// <summary>
        /// The Parent CommentActionId
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.TokenFields.IdModel)]
        [JsonInclude]
        public string CommentId { get; private set; }

        /// <summary>
        /// The Id of the Emojis
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.LabelFields.IdEmoji)]
        [JsonInclude]
        public string EmojiId { get; private set; }

        /// <summary>
        /// The Member
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Member)]
        [JsonInclude]
        public Member Member { get; private set; }

        /// <summary>
        /// The Emojis
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Emoji)]
        [JsonInclude]
        public Emoji Emoji { get; private set; }
    }
}




