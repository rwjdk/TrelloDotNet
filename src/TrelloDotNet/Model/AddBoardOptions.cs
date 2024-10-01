using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Additional optional options when adding a board
    /// </summary>
    public class AddBoardOptions
    {
        /// <summary>
        /// Determines whether to use the default set of labels. [Default: True]
        /// </summary>
        [JsonPropertyName("defaultLabels")]
        [QueryParameter]
        public bool DefaultLabels { get; set; } = true;

        /// <summary>
        /// Determines whether to add the default set of lists to a board (To Do, Doing, Done) [Default: True]
        /// </summary>
        [JsonPropertyName("defaultLists")]
        [QueryParameter]
        public bool DefaultLists { get; set; } = true;

        /// <summary>
        /// The id or name of the Workspace (Organization) the board should belong to.
        /// </summary>
        [JsonPropertyName("idOrganization")]
        [QueryParameter(false)]
        public string WorkspaceId { get; set; } = null;
    }
}