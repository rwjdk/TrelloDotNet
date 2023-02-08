using System;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Represent a Trello Webhook
    /// </summary>
    public class Webhook
    {
        /// <summary>
        /// Id of the Webhook (You can find these via method 'GetWebhooksForCurrentToken()')
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }


        /// <summary>
        /// Number of consecutive Failures (Will auto-disable if it ever reach 1000)
        /// </summary>
        [JsonPropertyName("consecutiveFailures")]
        [JsonInclude]
        public int ConsecutiveFailures { get; private set; }

        /// <summary>
        /// Date (UTC) since first consecutive Failure (Will auto-disable more than 30 days old)
        /// </summary>
        [JsonPropertyName("firstConsecutiveFailDate")]
        [JsonInclude]
        public DateTimeOffset? FirstConsecutiveFailDate { get; private set; }

        /// <summary>
        /// Description of the Webhook
        /// </summary>
        [JsonPropertyName("description")]
        [QueryParameter]
        public string Description { get; set; }

        /// <summary>
        /// The URL that the webhook should notify (Need to be a valid HTTPS URL that is reachable with a HEAD and POST request).
        /// </summary>
        [JsonPropertyName("callbackURL")]
        [QueryParameter]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Here you specify the Id of the API Object you wish to monitor (aka get notifications for). This can be an Id of a Board, List, Card, Member or Organization)
        /// </summary>
        [JsonPropertyName("idModel")]
        [QueryParameter]
        public string IdOfTypeYouWishToMonitor { get; set; }

        /// <summary>
        /// If the Webhook is active or not
        /// </summary>
        [JsonPropertyName("active")]
        [QueryParameter]
        public bool Active { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="description">Description of the Webhook</param>
        /// <param name="callbackUrl">The URL that the webhook should notify (Need to be a valid HTTPS URL that is reachable with a HEAD and POST request).</param>
        /// <param name="idOfTypeYouWishToMonitor">Here you specify the Id of the API Object you wish to monitor (aka get notifications for). This can be an Id of a Board, List, Card, Member or Organization)</param>
        public Webhook(string description, string callbackUrl, string idOfTypeYouWishToMonitor)
        {
            Description = description;
            CallbackUrl = callbackUrl;
            IdOfTypeYouWishToMonitor = idOfTypeYouWishToMonitor;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Webhook()
        {
            //Serialization
        }
    }
}