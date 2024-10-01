using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Checklist of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataChecklist
    {
        /// <summary>
        /// Checklist Id
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Checklist Name
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Get the Full Checklist Object
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Checklist</returns>
        public async Task<Checklist> GetAsync(CancellationToken cancellationToken = default)
        {
            return await Parent.Parent.TrelloClient.GetChecklistAsync(Id, cancellationToken);
        }

        /// <summary>
        /// Parent
        /// </summary>
        public WebhookActionData Parent { get; internal set; }

        internal static WebhookActionDataChecklist CreateDummy()
        {
            return new WebhookActionDataChecklist()
            {
                Id = "63d1239e857afaa8b003c633",
                Name = "MyChecklist",
            };
        }
    }
}