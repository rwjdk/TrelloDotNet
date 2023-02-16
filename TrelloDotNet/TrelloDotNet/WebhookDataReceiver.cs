using System.Text.Json;
using TrelloDotNet.Control.Webhook;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet
{
    /// <summary>
    /// Class to help you process a Webhook event but either turning the JSON into classes or fire events based on the JSON given to it
    /// </summary>
    public class WebhookDataReceiver
    {
        private readonly TrelloClient _trelloClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trelloClient">The base TrelloClient</param>
        public WebhookDataReceiver(TrelloClient trelloClient)
        {
            _trelloClient = trelloClient;
        }

        /// <summary>
        /// Class that can turn data from an into events based on what the data contain
        /// </summary>
        /// <param name="json">The raw incoming JSON</param>
        /// <returns>If the Event was processed (aka it was a supported event)</returns>
        public async void ProcessJsonIntoEvents(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return; //Most Likely the Head Call when setting up webhook - Just ignore
            }
            var webhookNotification = JsonSerializer.Deserialize<WebhookNotification>(json);
            BasicEvents.FireEvent(webhookNotification.Action);
            await SmartEvents.FireEvent(webhookNotification.Action, _trelloClient);
        }

        /// <summary>
        /// Convert JSON for the webhook into a Board Notification (require that you webhook set it idModel to a BoardId)
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotificationBoard ConvertJsonToWebhookNotificationBoard(string json)
        {
            return JsonSerializer.Deserialize<WebhookNotificationBoard>(json);
        }

        /// <summary>
        /// Convert JSON for the webhook into a Card Notification (require that you webhook set it idModel to a CardId)
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotificationCard ConvertJsonToWebhookNotificationCard(string json)
        {
            return JsonSerializer.Deserialize<WebhookNotificationCard>(json);
        }

        /// <summary>
        /// Convert JSON for the webhook into a List Notification (require that you webhook set it idModel to a ListId)
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotificationList ConvertJsonToWebhookNotificationList(string json)
        {
            return JsonSerializer.Deserialize<WebhookNotificationList>(json);
        }

        /// <summary>
        /// Convert JSON for the webhook into a Member Notification (require that you webhook set it idModel to a MemberId)
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotificationMember ConvertJsonToWebhookNotificationMember(string json)
        {
            return JsonSerializer.Deserialize<WebhookNotificationMember>(json);
        }

        /// <summary>
        /// Basic Webhook event where the arguments are generic (aka support everything but there is more work for you to use the event)
        /// </summary>
        public WebhookDataReceiverBasicEvents BasicEvents { get; } = new WebhookDataReceiverBasicEvents();

        /// <summary>
        /// Curated common events that most integrations need (aka much easier to use, but might not have the specific type event you need (ask on github for more events)) 
        /// </summary>
        public WebhookDataReceiverSmartEvents SmartEvents { get; } = new WebhookDataReceiverSmartEvents();
    }
}