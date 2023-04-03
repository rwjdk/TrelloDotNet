using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Collection of Old values of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataOld
    {
        /// <summary>
        /// due
        /// </summary>
        [JsonPropertyName("due")]
        [JsonInclude]
        public DateTimeOffset? Due { get; private set; }
        
        /// <summary>
        /// dueComplete
        /// </summary>
        [JsonPropertyName("dueComplete")]
        [JsonInclude]
        public bool? DueComplete { get; private set; }

        /// <summary>
        /// start
        /// </summary>
        [JsonPropertyName("start")]
        [JsonInclude]
        public DateTimeOffset? Start { get; private set; }

        /// <summary>
        /// idLabels
        /// </summary>
        [JsonPropertyName("idLabels")]
        [JsonInclude]
        public List<string> Labels { get; private set; }

        /// <summary>
        /// locationName
        /// </summary>
        [JsonPropertyName("locationName")]
        [JsonInclude]
        public string LocationName { get; private set; }

        /// <summary>
        /// address
        /// </summary>
        [JsonPropertyName("address")]
        [JsonInclude]
        public string Address { get; private set; }

        /// <summary>
        /// name
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// desc
        /// </summary>
        [JsonPropertyName("desc")]
        [JsonInclude]
        public string Description { get; private set; }

        /// <summary>
        /// idList
        /// </summary>
        [JsonPropertyName("idList")]
        [JsonInclude]
        public string ListId { get; private set; }
        
        /// <summary>
        /// pos
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal? Position { get; private set; }

        /// <summary>
        /// dueReminder
        /// </summary>
        [JsonPropertyName("dueReminder")]
        [JsonInclude]
        public int? DueReminder { get; private set; }
        
        /// <summary>
        /// coordinates
        /// </summary>
        [JsonPropertyName("coordinates")]
        [JsonInclude]
        public Coordinates Coordinates { get; private set; }

        internal static WebhookActionDataOld CreateDummy()
        {
            return new WebhookActionDataOld()
            {
                DueComplete = true,
                Address = "My Street",
                Coordinates = Coordinates.CreateDummy(),
                Name = "MyName",
                Description = "MyDescription",
                Due = new DateTimeOffset(new DateTime(2022, 12, 31)),
                DueReminder = 10,
                Labels = new List<string> { "a", "b", "c" },
                ListId = "63d1239e857afaa8b003c633",
                LocationName = "My Location",
                Position = 42,
                Start = new DateTimeOffset(new DateTime(2023, 1, 1))
            };
        }
    }
}