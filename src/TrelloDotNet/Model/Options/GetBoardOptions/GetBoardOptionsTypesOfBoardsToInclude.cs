using System;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Options.GetBoardOptions
{
    /// <summary>
    /// Determine what 'Type' of boards are to be included in a multi-board result
    /// </summary>
    [Flags]
    public enum GetBoardOptionsTypesOfBoardsToInclude
    {
        /// <summary>
        /// All Boards (including closed)
        /// </summary>
        [JsonPropertyName("all")] All = 1,

        /// <summary>
        /// Closed Boards
        /// </summary>
        [JsonPropertyName("closed")] Closed = 2,

        /// <summary>
        /// Unknown set of Boards (not documented by Trello). If you know what this does then please add an issue on GitHub
        /// </summary>
        [JsonPropertyName("members")] Members = 4,

        /// <summary>
        /// Open Boards
        /// </summary>
        [JsonPropertyName("open")] Open = 8,

        /// <summary>
        /// Unknown set of Boards (not documented by Trello). If you know what this does then please add an issue on GitHub
        /// </summary>
        [JsonPropertyName("organization")] Organization = 16,

        /// <summary>
        /// Public Boards
        /// </summary>
        [JsonPropertyName("public")] Public = 32,

        /// <summary>
        /// Starred Boards (by the member owning the Trello Token)
        /// </summary>
        [JsonPropertyName("starred")] Starred = 64,
    }
}