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

    }
}