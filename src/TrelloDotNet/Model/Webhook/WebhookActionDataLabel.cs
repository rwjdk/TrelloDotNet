﻿using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Label of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataLabel
    {
        /// <summary>
        /// Label Id
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Label Name
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Color of Label
        /// </summary>
        [JsonPropertyName("color")]
        [JsonInclude]
        public string Color { get; private set; }

        internal static WebhookActionDataLabel CreateDummy()
        {
            return new WebhookActionDataLabel()
            {
                Id = "63d1239e857afaa8b003c633",
                Name = "MyLabel",
                Color = "red",
            };
        }
    }
}