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
        /// Constructor
        /// </summary>
        /// <param name="jsonFromWebhook">The JSON from the Webhook</param>
        public ProcessingRequest(string jsonFromWebhook)
        {
            JsonFromWebhook = jsonFromWebhook;
        }
    }
}