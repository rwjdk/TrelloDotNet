using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Optional settings for adding a Board
    /// </summary>
    [DebuggerDisplay("DefaultLabels = {DefaultLabels}, DefaultLists = {DefaultLists}, WorkspaceId = {WorkspaceId}")]
    public class AddBoardOptions
    {
        /// <summary>
        /// Indicates if the default set of labels should be added to the Board. [Default: True]
        /// </summary>
        [JsonPropertyName("defaultLabels")]
        [QueryParameter]
        public bool DefaultLabels { get; set; } = true;

        /// <summary>
        /// Indicates if the default set of lists (To Do, Doing, Done) should be added to the Board. [Default: True]
        /// </summary>
        [JsonPropertyName("defaultLists")]
        [QueryParameter]
        public bool DefaultLists { get; set; } = true;

        /// <summary>
        /// The ID of the Workspace (Organization) the board should belong to.
        /// </summary>
        [JsonPropertyName("idOrganization")]
        [QueryParameter(false)]
        public string WorkspaceId { get; set; } = null;
    }
}