using System;
using System.Security.Cryptography;
using System.Text;
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
        private readonly byte[] _secret;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trelloClient">The base TrelloClient</param>
        /// <param name="secret">
        /// The client secret you get on https://trello.com/power-ups/admin/.
        /// When passing a secret, signature validation will be turned on for calls to <see cref="ProcessJsonIntoEvents"/>.
        /// </param>
        public WebhookDataReceiver(TrelloClient trelloClient, string secret = null)
        {
            _trelloClient = trelloClient;
            
            if (!string.IsNullOrEmpty(secret))
            {
                _secret = Encoding.ASCII.GetBytes(secret);
            }
        }

        /// <summary>
        /// Class that can turn data from an into events based on what the data contain
        /// </summary>
        /// <param name="json">The raw incoming JSON</param>
        /// <param name="signature">Signature from X-Trello-Webhook header for signature validation</param>
        /// <param name="webhookUrl">Webhook URL for signature validation</param>
        /// <returns>If the Event was processed (aka it was a supported event)</returns>
        public async void ProcessJsonIntoEvents(string json, string signature = null, string webhookUrl = null)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return; // Most Likely the Head Call when setting up webhook - Just ignore
            }

            if (_secret != null && !ValidateSignature(json, signature, webhookUrl))
            {
                return; // Invalid signature
            }
            var webhookNotification = JsonSerializer.Deserialize<WebhookNotification>(json);
            BasicEvents.FireEvent(webhookNotification.Action);
            await SmartEvents.FireEvent(webhookNotification.Action, _trelloClient);
        }

        private string Base64Digest(string data)
        {
            var hmac = new HMACSHA1(_secret);
            var digest = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(digest);
        }

        private bool ValidateSignature(string json, string signature, string webhookUrl)
        {
            var content = json + webhookUrl;
            var hash = Base64Digest(content);
            return hash == signature;
        }

        /// <summary>
        /// Convert JSON for the webhook into a Notification
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotification ConvertJsonToWebhookNotification(string json)
        {
            var result = JsonSerializer.Deserialize<WebhookNotification>(json);
            SetTrelloClientAndParents(result.Action);
            return result;
        }

        private void SetTrelloClientAndParents(WebhookAction action)
        {
            action.TrelloClient = _trelloClient;
            action.Data.Parent = action;
            if (action.Data.Board != null)
            {
                action.Data.Board.Parent = action.Data;
            }
            if (action.Data.Card != null)
            {
                action.Data.Card.Parent = action.Data;
            }
            if (action.Data.List != null)
            {
                action.Data.List.Parent = action.Data;
            }
            if (action.Data.ListAfter != null)
            {
                action.Data.ListAfter.Parent = action.Data;
            }
            if (action.Data.ListBefore != null)
            {
                action.Data.ListBefore.Parent = action.Data;
            }
            if (action.Data.Checklist != null)
            {
                action.Data.Checklist.Parent = action.Data;
            }
            if (action.Data.Member != null)
            {
                action.Data.Member.Parent = action.Data;
            }
        }

        /// <summary>
        /// Convert JSON for the webhook into a Board Notification (require that you webhook set it idModel to a BoardId)
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotificationBoard ConvertJsonToWebhookNotificationBoard(string json)
        {
            var result = JsonSerializer.Deserialize<WebhookNotificationBoard>(json);
            SetTrelloClientAndParents(result.Action);
            return result;
        }

        /// <summary>
        /// Convert JSON for the webhook into a Card Notification (require that you webhook set it idModel to a CardId)
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotificationCard ConvertJsonToWebhookNotificationCard(string json)
        {
            var result = JsonSerializer.Deserialize<WebhookNotificationCard>(json);
            SetTrelloClientAndParents(result.Action);
            return result;
        }

        /// <summary>
        /// Convert JSON for the webhook into a List Notification (require that you webhook set it idModel to a ListId)
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotificationList ConvertJsonToWebhookNotificationList(string json)
        {
            var result = JsonSerializer.Deserialize<WebhookNotificationList>(json);
            SetTrelloClientAndParents(result.Action);
            return result;
        }

        /// <summary>
        /// Convert JSON for the webhook into a Member Notification (require that you webhook set it idModel to a MemberId)
        /// </summary>
        /// <param name="json">Raw JSON from the webhook</param>
        /// <returns>C# Class representing the JSON</returns>
        public WebhookNotificationMember ConvertJsonToWebhookNotificationMember(string json)
        {
            var result = JsonSerializer.Deserialize<WebhookNotificationMember>(json);
            SetTrelloClientAndParents(result.Action);
            return result;
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