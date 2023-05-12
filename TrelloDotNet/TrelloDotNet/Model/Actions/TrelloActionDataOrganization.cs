﻿using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Actions
{
    /// <summary>
    /// Represent an Organization
    /// </summary>
    public class TrelloActionDataOrganization
    {
        /// <summary>
        /// Id of the Organization 
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Organization
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Display Name of the Organization
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonInclude]
        public string DisplayName { get; private set; }

        /// <summary>
        /// Website of the Organization
        /// </summary>
        [JsonPropertyName("website")]
        [JsonInclude]
        public string Website { get; private set; }
    }
}