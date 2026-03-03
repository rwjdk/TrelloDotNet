using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The name of a position instead of a numeric value
    /// </summary>
    public enum NamedPosition
    {
        /// <summary>
        /// Top
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Top)]
        Top,

        /// <summary>
        /// Bottom
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Bottom)]
        Bottom,
    }
}



