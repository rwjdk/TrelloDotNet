namespace TrelloDotNet.AutomationEngine.Model
{
    /// <summary>
    /// A Processing Request (containing the JSON of the Webhook)
    /// </summary>
    public class ProcessingRequest
    {
        /// <summary>
        /// The Json from the Webhook
        /// </summary>
        public string JsonFromWebhook { get; }
        
        /// <summary>
        /// The Signature from the X-Trello-Webhook header for signature validation
        /// </summary>
        public string Signature { get; }
        
        
        /// <summary>
        /// The Webhook URL for signature validation
        /// </summary>
        public string WebhookUrl { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jsonFromWebhook">The JSON from the Webhook</param>
        /// <param name="signature">Signature from X-Trello-Webhook header for signature validation</param>
        /// <param name="webhookUrl">Webhook URL for signature validation</param>
        public ProcessingRequest(string jsonFromWebhook, string signature = null, string webhookUrl = null)
        {
            JsonFromWebhook = jsonFromWebhook;
            Signature = signature;
            WebhookUrl = webhookUrl;
        }
    }
}