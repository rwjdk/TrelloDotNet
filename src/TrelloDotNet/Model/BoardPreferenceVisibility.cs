using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Visibility of a board
    /// </summary>
    public enum BoardPreferenceVisibility
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Board is Private
        /// </summary>
        [JsonPropertyName("private")]
        Private,

        /// <summary>
        /// Board is Workspace (All workspace members can see/edit this board)
        /// </summary>
        [JsonPropertyName("org")]
        Workspace,

        /// <summary>
        /// The Board is Public
        /// </summary>
        [JsonPropertyName("public")]
        Public
    }
}