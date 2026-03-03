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
        [JsonPropertyName(Constants.TrelloIds.PluginFields.Id)]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Plugin
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.PluginFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// The ID of the Workspace that Own the Add-on
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.IdOrganizationOwner)]
        [JsonInclude]
        public string OrganizationOwnerId { get; private set; }

        /// <summary>
        /// The Author of the Plugin
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Author)]
        [JsonInclude]
        public string Author { get; private set; }

        /// <summary>
        /// The iFrameUrl of the Plugin
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.PluginFields.IframeConnectorUrl)]
        [JsonInclude]
        public string IframeConnectorUrl { get; private set; }

        /// <summary>
        /// The Capabilities the Plugin have registered
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.PluginFields.Capabilities)]
        [JsonInclude]
        public string[] Capabilities { get; private set; }

        /// <summary>
        /// The Categories the Plugin is listed under
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.PluginFields.Categories)]
        [JsonInclude]
        public string[] Categories { get; private set; }

        /// <summary>
        /// The Privacy URL of the Plugin
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.PrivacyUrl)]
        [JsonInclude]
        public string PrivacyUrl { get; private set; }

        /// <summary>
        /// Is the Plugin Public (In PowerUps Marketplace) or not
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.PluginFields.Public)]
        [JsonInclude]
        public bool Public { get; private set; }


        /// <summary>
        /// The Support Email of the Plugin
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.SupportEmail)]
        [JsonInclude]
        public string SupportEmail { get; private set; }
    }
}





