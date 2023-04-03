using System;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// The Action of the Webhook
    /// </summary>
    public class WebhookAction
    {
        /// <summary>
        /// Id of the Action
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// The Id of the Member (User) that did the action
        /// </summary>
        [JsonPropertyName("idMemberCreator")]
        [JsonInclude]
        public string MemberCreatorId { get; private set; }

        /// <summary>
        /// Type of the Action (example Update of a Card)
        /// </summary>
        [JsonPropertyName("type")]
        [JsonInclude]
        public string Type { get; private set; }

        /// <summary>
        /// Date of the Action
        /// </summary>
        [JsonPropertyName("date")]
        [JsonInclude]
        public DateTimeOffset Date { get; private set; }

        /// <summary>
        /// The Id of the Member (User) that did the action
        /// </summary>
        [JsonPropertyName("memberCreator")]
        [JsonInclude]
        public Member MemberCreator { get; private set; }

        /// <summary>
        /// Data about the Action
        /// </summary>
        [JsonPropertyName("data")]
        [JsonInclude]
        public WebhookActionData Data { get; private set; }

        /// <summary>
        /// Display of the Action
        /// </summary>
        [JsonPropertyName("display")]
        [JsonInclude]
        public WebhookActionDisplay Display { get; private set; }

        /// <summary>
        /// Trello Client
        /// </summary>
        public TrelloClient TrelloClient { get; internal set; }

        internal static WebhookAction CreateDummy(TrelloClient trelloClient, WebhookActionDummyCreationScenario scenario)
        {
            var webhookAction = new WebhookAction()
            {
                TrelloClient = trelloClient,
                Data = WebhookActionData.CreateDummy(),
                Date = new DateTimeOffset(new DateTime(2000, 12, 1)),
                Display = WebhookActionDisplay.CreateDummy(scenario),
                Id = "63d128787441d05619f44dbe",
                MemberCreator = Member.CreateDummy(),
                MemberCreatorId = "63d1239e857afaa8b003c633",
                Type = GetTypeFromScenario(scenario),
            };
            webhookAction.Data.Parent = webhookAction;
            return webhookAction;
        }

        private static string GetTypeFromScenario(WebhookActionDummyCreationScenario scenario)
        {
            switch (scenario)
            {
                case WebhookActionDummyCreationScenario.MoveCardToList:
                    return WebhookActionTypes.UpdateCard;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scenario));
            }
        }

        internal enum WebhookActionDummyCreationScenario
        {
            MoveCardToList
        }
    }
}