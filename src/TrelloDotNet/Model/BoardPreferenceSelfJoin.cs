using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// If Workspace Members Self-join this board
    /// </summary>
    public enum BoardPreferenceSelfJoin
    {
        /// <summary>
        /// Unknown value retrieved from the Trello REST API
        /// </summary>
        Unknown,

        /// <summary>
        /// Workspace Member can Self-Join
        /// </summary>
        [JsonPropertyName("true")]
        Yes,

        /// <summary>
        /// Workspace Member can not Self-Join
        /// </summary>
        [JsonPropertyName("false")]
        No,
    }
}