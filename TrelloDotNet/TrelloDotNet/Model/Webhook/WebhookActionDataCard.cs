using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        /// Position
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal? Posistion { get; private set; }

        /// <summary>
        /// If the due work is completed
        /// </summary>
        [JsonPropertyName("dueComplete")]
        [JsonInclude]
        public bool? DueComplete { get; private set; }

        /// <summary>
        /// Get the Full Card Object
        /// </summary>
        /// <returns>The Card</returns>
        public async Task<Card> GetAsync()
        {
            return await Parent.Parent.TrelloClient.GetCardAsync(Id);
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
                    Posistion = cardToSimulate.Position
                };
            }

            return new WebhookActionDataCard()
            {
                Id = "63d128787441d05619f44dbe",
                DueComplete = true,
                Name = "MyCard",
                Posistion = 1
            };
        }

    }
}