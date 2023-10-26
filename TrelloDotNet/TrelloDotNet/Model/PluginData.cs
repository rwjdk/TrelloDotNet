using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent plugin data on a board or 
    /// </summary>
    [DebuggerDisplay("Plugin: {PluginId} - Value: {Value}")]
    public class PluginData
    {
        /// <summary>
        /// Id of the Data
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Id of the plugin the data belong to
        /// </summary>
        [JsonPropertyName("idPlugin")]
        [JsonInclude]
        public string PluginId { get; private set; }

        /// <summary>
        /// Scope of the Data ('board', 'card', 'member' or 'organization')
        /// </summary>
        [JsonPropertyName("scope")]
        [JsonInclude]
        public string Scope { get; private set; }

        /// <summary>
        /// Raw Value(s) of the data from the plugin (normally in JSON Format)
        /// </summary>
        [JsonPropertyName("value")]
        [JsonInclude]
        public string Value { get; private set; }

        /// <summary>
        /// Visibility of the Data ('shared' or 'private')
        /// </summary>
        [JsonPropertyName("access")]
        [JsonInclude]
        public string Visibility { get; private set; }

        /// <summary>
        /// When there was last update of the pluginData [stored UTC]
        /// </summary>
        [JsonPropertyName("dateLastUpdated")]
        [JsonInclude]
        public DateTimeOffset LastUpdated { get; private set; }
    }
}