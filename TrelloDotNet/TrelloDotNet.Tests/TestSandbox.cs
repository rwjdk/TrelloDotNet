using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests
{

    public class TestSandbox : TestBase
    {
        [FactManualOnly]
        public async Task GetWebhooks()
        {
            await Task.CompletedTask;
            var webhooksForCurrentToken = await TrelloClient.GetWebhooksForCurrentToken();
        }

        [FactManualOnly]
        public async Task DeleteAllWebhooks()
        {
            await Task.CompletedTask;
            var webhooksForCurrentToken = await TrelloClient.GetWebhooksForCurrentToken();
            foreach (var webhook in webhooksForCurrentToken)
            {
                await TrelloClient.DeleteWebhook(webhook.Id);
            }
        }

        [FactManualOnly]
        public async Task UpdateWebhook()
        {
            await Task.CompletedTask;
            Webhook webhook = await TrelloClient.GetWebhookAsync("63e2892778670f4f7b7ffa2e");
            webhook.CallbackUrl = "https://4cf8-185-229-154-225.eu.ngrok.io/api/FunctionTrelloWebhookEndpointReceiver";
            var updatedWebhook = await TrelloClient.UpdateWebhookAsync(webhook);
        }
    }
}
