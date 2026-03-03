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
        // ReSharper disable once UnusedMember.Global
        Unknown = -1,

        /// <summary>
        /// Workspace Member can Self-Join
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.TrueValue)]
        Yes,

        /// <summary>
        /// Workspace Member can not Self-Join
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.FalseValue)]
        No,
    }
}





