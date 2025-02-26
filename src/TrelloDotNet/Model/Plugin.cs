using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Plugin
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class Plugin
    {
        /// <summary>
        /// Id of the Plugin
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Plugin
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// The ID of the Workspace that Own the Add-on
        /// </summary>
        [JsonPropertyName("idOrganizationOwner")]
        [JsonInclude]
        public string OrganizationOwnerId { get; private set; }

        /// <summary>
        /// The Author of the Plugin
        /// </summary>
        [JsonPropertyName("author")]
        [JsonInclude]
        public string Author { get; private set; }

        /// <summary>
        /// The iFrameUrl of the Plugin
        /// </summary>
        [JsonPropertyName("iframeConnectorUrl")]
        [JsonInclude]
        public string IframeConnectorUrl { get; private set; }

        /// <summary>
        /// The Capabilities the Plugin have registered
        /// </summary>
        [JsonPropertyName("capabilities")]
        [JsonInclude]
        public string[] Capabilities { get; private set; }

        /// <summary>
        /// The Categories the Plugin is listed under
        /// </summary>
        [JsonPropertyName("categories")]
        [JsonInclude]
        public string[] Categories { get; private set; }

        /// <summary>
        /// The Privacy URL of the Plugin
        /// </summary>
        [JsonPropertyName("privacyUrl")]
        [JsonInclude]
        public string PrivacyUrl { get; private set; }

        /// <summary>
        /// Is the Plugin Public (In PowerUps Marketplace) or not
        /// </summary>
        [JsonPropertyName("public")]
        [JsonInclude]
        public bool Public { get; private set; }


        /// <summary>
        /// The Support Email of the Plugin
        /// </summary>
        [JsonPropertyName("supportEmail")]
        [JsonInclude]
        public string SupportEmail { get; private set; }
    }
}