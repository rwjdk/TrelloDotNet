using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a new reaction to a Comment
    /// </summary>
    [DebuggerDisplay("Native = {Native}")]
    public class Reaction
    {
        /// <summary>
        /// The Native Emoji (aka the visual emoji) [see https://developer.atlassian.com/cloud/trello/rest/api-group-emoji/#api-emoji-get]
        /// </summary>
        [JsonPropertyName("native")]
        public string Native { get; set; }
    }
}