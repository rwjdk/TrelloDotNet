using System;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Data of a Webhook Action
    /// </summary>
    public class WebhookActionData
    {
        /// <summary>
        /// List Data Object
        /// </summary>
        [JsonPropertyName("list")]
        [JsonInclude]
        public WebhookActionDataList List { get; private set; }

        /// <summary>
        /// Board Data Object
        /// </summary>
        [JsonPropertyName("board")]
        [JsonInclude]
        public WebhookActionDataBoard Board { get; private set; }

        /// <summary>
        /// Card Data Object
        /// </summary>
        [JsonPropertyName("card")]
        [JsonInclude]
        public WebhookActionDataCard Card { get; private set; }

        /// <summary>
        /// Label Data Object
        /// </summary>
        [JsonPropertyName("label")]
        [JsonInclude]
        public WebhookActionDataLabel Label { get; private set; }

        /// <summary>
        /// Checklist Data Object
        /// </summary>
        [JsonPropertyName("checklist")]
        [JsonInclude]
        public WebhookActionDataChecklist Checklist { get; private set; }

        /// <summary>
        /// CheckItem Data Object
        /// </summary>
        [JsonPropertyName("checkItem")]
        [JsonInclude]
        public WebhookActionDataCheckItem CheckItem { get; private set; }

        /// <summary>
        /// Before List
        /// </summary>
        [JsonPropertyName("listBefore")]
        [JsonInclude]
        public WebhookActionDataList ListBefore { get; private set; }

        /// <summary>
        /// After List
        /// </summary>
        [JsonPropertyName("listAfter")]
        [JsonInclude]
        public WebhookActionDataList ListAfter { get; private set; }

        /// <summary>
        /// Old
        /// </summary>
        [JsonPropertyName("old")]
        [JsonInclude]
        public WebhookActionDataOld Old { get; private set; }

        /// <summary>
        /// Member
        /// </summary>
        [JsonPropertyName("member")]
        [JsonInclude]
        public WebhookActionDataMember Member { get; set; }

        /// <summary>
        /// Parent
        /// </summary>
        public WebhookAction Parent { get; internal set; }

        internal static WebhookActionData CreateDummy(WebhookAction.WebhookActionDummyCreationScenario webhookActionDummyCreationScenario)
        {
            var listAfter = WebhookActionDataList.CreateDummy();
            var listBefore = WebhookActionDataList.CreateDummy();
            var card = WebhookActionDataCard.CreateDummy();
            var board = WebhookActionDataBoard.CreateDummy();
            var member = WebhookActionDataMember.CreateDummy();
            var checkItem = WebhookActionDataCheckItem.CreateDummy();
            var checklist = WebhookActionDataChecklist.CreateDummy();
            var label = WebhookActionDataLabel.CreateDummy();
            var list = WebhookActionDataList.CreateDummy();

            switch (webhookActionDummyCreationScenario)
            {
                case WebhookAction.WebhookActionDummyCreationScenario.MoveCardToList:
                    member = null;
                    checkItem = null;
                    checklist = null;
                    label = null;
                    list = null;
                    break;
                case WebhookAction.WebhookActionDummyCreationScenario.NoListAfter:
                    listAfter = null;
                    break;
            }

            var webhookActionData = new WebhookActionData()
            {
                Card = card,
                Board = board,
                Member = member,
                CheckItem = checkItem,
                Checklist = checklist,
                Label = label,
                List = list,
                ListAfter = listAfter,
                ListBefore = listBefore,
                Old = WebhookActionDataOld.CreateDummy(),
            };
            if (webhookActionData.Member != null)
            {
                webhookActionData.Member.Parent = webhookActionData;
            }
            if (webhookActionData.Card != null)
            {
                webhookActionData.Card.Parent = webhookActionData;
            }
            if (webhookActionData.Board != null)
            {
                webhookActionData.Board.Parent = webhookActionData;
            }
            if (webhookActionData.Checklist != null)
            {
                webhookActionData.Checklist.Parent = webhookActionData;
            }

            if (webhookActionData.ListBefore != null)
            {
                webhookActionData.ListBefore.Parent = webhookActionData;
            }

            if (webhookActionData.ListAfter != null)
            {
                webhookActionData.ListAfter.Parent = webhookActionData;
            }
            if (webhookActionData.List != null)
            {
                webhookActionData.List.Parent = webhookActionData;
            }
            return webhookActionData;
        }
    }
}