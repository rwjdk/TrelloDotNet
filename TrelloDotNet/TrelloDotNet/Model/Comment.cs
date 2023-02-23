using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a comment on a Card
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Id of the comment
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Text of the comment
        /// </summary>
        [JsonPropertyName("text")]
        [QueryParameter]
        public string Text { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Comment()
        {
            //Serialization
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Comment Text</param>
        public Comment(string text)
        {
            Text = text;
        }
    }
}