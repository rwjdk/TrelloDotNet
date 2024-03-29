﻿using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Webhook Action Member
    /// </summary>
    public class WebhookActionDataMember
    {
        /// <summary>
        /// List Id
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// List Name
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Get the Full Member Object
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Member</returns>
        public async Task<Member> GetAsync(CancellationToken cancellationToken = default)
        {
            return await Parent.Parent.TrelloClient.GetMemberAsync(Id, cancellationToken);
        }

        /// <summary>
        /// Parent
        /// </summary>
        public WebhookActionData Parent { get; internal set; }

        internal static WebhookActionDataMember CreateDummy()
        {
            return new WebhookActionDataMember()
            {
                Id = "63d1239e857afaa8b003c633",
                Name = "Rasmus"
            };
        }
    }
}