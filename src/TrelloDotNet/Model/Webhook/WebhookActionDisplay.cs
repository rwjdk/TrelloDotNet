using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Display of the Webhook Action Data
    /// </summary>
    public class WebhookActionDisplay
    {
        /// <summary>
        /// Translation Key
        /// </summary>
        [JsonPropertyName("translationKey")]
        [JsonInclude]
        public string TranslationKey { get; private set; }

        internal static WebhookActionDisplay CreateDummy(WebhookAction.WebhookActionDummyCreationScenario scenario)
        {
            const string translationKey = "foo";
            return new WebhookActionDisplay()
            {
                TranslationKey = translationKey
            };
        }
    }
}