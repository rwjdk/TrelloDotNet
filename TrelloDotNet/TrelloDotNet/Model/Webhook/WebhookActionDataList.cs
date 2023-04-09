using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// List of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataList
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
        /// Position (Only present when list is moved)
        /// </summary>
        [JsonPropertyName("pos")]
        [JsonInclude]
        public decimal Posistion { get; private set; }

        /// <summary>
        /// Get the Full List Object
        /// </summary>
        /// <returns>The List</returns>
        public async Task<List> GetAsync()
        {
            return await Parent.Parent.TrelloClient.GetListAsync(Id);
        }

        /// <summary>
        /// Parent
        /// </summary>
        public WebhookActionData Parent { get; internal set; }

        internal static WebhookActionDataList CreateDummy(List listToSimulate = null)
        {
            if (listToSimulate != null)
            {
                return new WebhookActionDataList()
                {
                    Id = listToSimulate.Id,
                    Name = listToSimulate.Name,
                    Posistion = listToSimulate.Position
                };
            }

            return new WebhookActionDataList()
            {
                Id = "63d1239e857afaa8b003c633",
                Name = "MyList",
                Posistion = 1
            };
        }
    }
}