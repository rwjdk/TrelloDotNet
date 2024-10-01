using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent an Organization (also known as a Workspace)
    /// </summary>
    [DebuggerDisplay("{DisplayName} ({Id})")]
    public class Organization
    {
        /// <summary>
        /// Id of the Organization
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Organization (Please Note: This name need lowercase and need to be Unique among all Trello users worldwide. If provided is not unique during add, API will make it unique). If non is provided one will be generated (in update, it needs to be unique)
        /// </summary>
        /// <remarks>
        /// At least 3 lowercase letters, underscores, and numbers
        /// </remarks>
        [JsonPropertyName("name")]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// Display Name of the Organization
        /// </summary>
        [JsonPropertyName("displayName")]
        [QueryParameter]
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the Organization (Optional)
        /// </summary>
        [JsonPropertyName("desc")]
        [QueryParameter]
        public string Description { get; set; }

        /// <summary>
        /// URL of the Organization
        /// </summary>
        [JsonPropertyName("url")]
        [JsonInclude]
        public string Url { get; private set; }

        /// <summary>
        /// Website of the Organization (optional)
        /// </summary>
        [JsonPropertyName("website")]
        [QueryParameter]
        public string Website { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="displayName">Display Name</param>
        public Organization(string displayName)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Organization()
        {
            //Serialization
        }
    }
}