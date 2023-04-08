using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Board of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataBoard
    {
        /// <summary>
        /// Id of the Board
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the board
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Short Version of Id for the Board
        /// </summary>
        [JsonPropertyName("shortLink")]
        [JsonInclude]
        public string ShortLink { get; private set; }

        /// <summary>
        /// Get the Full Board Object
        /// </summary>
        /// <returns></returns>
        public async Task<Board> GetAsync()
        {
            return await Parent.Parent.TrelloClient.GetBoardAsync(Id);
        }

        /// <summary>
        /// Parent
        /// </summary>
        public WebhookActionData Parent { get; internal set; }

        internal static WebhookActionDataBoard CreateDummy(Board boardToSimulate)
        {
            if (boardToSimulate != null)
            {
                return new WebhookActionDataBoard()
                {
                    Id = boardToSimulate.Id,
                    Name = boardToSimulate.Name,
                    ShortLink = boardToSimulate.ShortUrl
                };
            }
            return new WebhookActionDataBoard()
            {
                Id = "63d128787441d05619f44dbe",
                Name = "MyBoard",
                ShortLink = "https://myUrl.com"
            };
        }
    }
}