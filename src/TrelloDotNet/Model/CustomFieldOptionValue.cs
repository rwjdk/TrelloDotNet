using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Value of a Custom Field Option
    /// </summary>
    public class CustomFieldOptionValue
    {
        /// <summary>
        /// The Option Text
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Text)]
        [JsonInclude]
        public string Text { get; private set; }
    }
}



