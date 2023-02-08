namespace TrelloDotNet.Model
{
    /// <summary>
    /// EventHandler of the Webhooks
    /// </summary>
    /// <typeparam name="T">Arguments Class</typeparam>
    public delegate void WebhookEventHandler<in T>(T args);
}