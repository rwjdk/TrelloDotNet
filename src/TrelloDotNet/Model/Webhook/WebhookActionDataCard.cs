using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using TrelloDotNet.Model.Options.GetCardOptions;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Card of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataCard
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
        /// pos
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal? Position { get; private set; }

        /// <summary>
        /// If the Card is completed
        /// </summary>
        [JsonPropertyName("dueComplete")]
        [JsonInclude]
        public bool? DueComplete { get; private set; }

        /// <summary>
        /// due
        /// </summary>
        [JsonPropertyName("due")]
        [JsonInclude]
        public DateTimeOffset? Due { get; private set; }

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

        /// <summary>
        /// cover
        /// </summary>
        [JsonPropertyName("cover")]
        [JsonInclude]
        public CardCover Cover { get; private set; }

        /// <summary>
        /// Get the Full Card Object
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Card</returns>
        public async Task<Card> GetAsync(CancellationToken cancellationToken = default)
        {
            return await Parent.Parent.TrelloClient.GetCardAsync(Id, cancellationToken);
        }

        /// <summary>
        /// Get the Full Card Object
        /// </summary>
        /// <param name="options">Options</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Card</returns>
        public async Task<Card> GetAsync(GetCardOptions options, CancellationToken cancellationToken = default)
        {
            return await Parent.Parent.TrelloClient.GetCardAsync(Id, options, cancellationToken);
        }

        /// <summary>
        /// Parent
        /// </summary>
        public WebhookActionData Parent { get; internal set; }

        internal static WebhookActionDataCard CreateDummy(Card cardToSimulate)
        {
            if (cardToSimulate != null)
            {
                return new WebhookActionDataCard()
                {
                    Id = cardToSimulate.Id,
                    DueComplete = cardToSimulate.DueComplete,
                    Name = cardToSimulate.Name,
                    Position = cardToSimulate.Position
                };
            }

            return new WebhookActionDataCard()
            {
                Id = "63d128787441d05619f44dbe",
                DueComplete = true,
                Name = "MyCard",
                Position = 1
            };
        }
    }
}