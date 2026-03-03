using System;
using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [JsonPropertyName(Constants.TrelloIds.BoardFields.All)]
        All = 1,

        /// <summary>
        /// Closed Boards
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Closed)]
        Closed = 2,

        /// <summary>
        /// Unknown set of Boards (not documented by Trello). If you know what this does then please add an issue on GitHub
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.MemberFields.Members)]
        Members = 4,

        /// <summary>
        /// Open Boards
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Open)]
        Open = 8,

        /// <summary>
        /// Unknown set of Boards (not documented by Trello). If you know what this does then please add an issue on GitHub
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Organization)]
        Organization = 16,

        /// <summary>
        /// Public Boards
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Public)]
        Public = 32,

        /// <summary>
        /// Starred Boards (by the member owning the Trello Token)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.BoardFields.Starred)]
        Starred = 64,
    }
}





