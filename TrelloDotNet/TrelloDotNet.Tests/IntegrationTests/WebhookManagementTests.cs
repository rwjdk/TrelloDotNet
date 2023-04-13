using TrelloDotNet.Model.Webhook;
using Xunit.Abstractions;

namespace TrelloDotNet.Tests.IntegrationTests;

[Collection("Webhook Management")] //In own collection to not overlap other tests
public class WebhookManagementTests : TestBaseWithNewBoard
{
    private readonly ITestOutputHelper _output;

    public WebhookManagementTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task WebHookCrud()
    {
        try
        {
            _output.WriteLine("Creating new board");
            await CreateNewBoard();
            //Find current webhooks
            var currentWebhooks = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            _output.WriteLine($"There are {currentWebhooks.Count} webhooks");
            foreach (var currentWebhook in currentWebhooks)
            {
                LogWebhook("List Before", currentWebhook);
            }

            //Add Webhook
            const string callbackUrl = "https://www.trello.com";
            var description = Guid.NewGuid().ToString();
            var webhook = new Webhook(description, callbackUrl, BoardId);
            var addedWebHook = await TrelloClient.AddWebhookAsync(webhook);
            LogWebhook("Added", addedWebHook);
            Assert.Equal(callbackUrl, addedWebHook.CallbackUrl);
            Assert.Equal(description, addedWebHook.Description);
            Assert.True(addedWebHook.Active);
            var webhookId = addedWebHook.Id;

            //Update Webhook
            var updatedDescription = Guid.NewGuid().ToString();
            addedWebHook.Description = updatedDescription;
            addedWebHook.Active = false;
            var updatedWebHook = await TrelloClient.UpdateWebhookAsync(addedWebHook);
            LogWebhook("Updated", addedWebHook);
            Assert.Equal(callbackUrl, updatedWebHook.CallbackUrl);
            Assert.Equal(updatedDescription, updatedWebHook.Description);
            Assert.False(addedWebHook.Active);

            WaitToAvoidRateLimits(10);

            //Check lists of webhooks are update
            var webhooksAfterAdd = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            _output.WriteLine($"There are now {webhooksAfterAdd.Count} webhooks after add and update");
            foreach (var currentWebhook in webhooksAfterAdd)
            {
                LogWebhook("List After", currentWebhook);
            }


            Assert.Equal(currentWebhooks.Count + 1, webhooksAfterAdd.Count);
            Assert.Equal(webhookId, webhooksAfterAdd.First(x => x.Id == webhookId).Id);
            Assert.Equal(updatedDescription, webhooksAfterAdd.First(x => x.Id == webhookId).Description);
            Assert.Equal(callbackUrl, webhooksAfterAdd.First(x => x.Id == webhookId).CallbackUrl);

            //Get Webhook
            var getWebhook = await TrelloClient.GetWebhookAsync(webhookId);
            Assert.Equal(getWebhook.Id, updatedWebHook.Id);
            Assert.Equal(getWebhook.Description, updatedWebHook.Description);
            Assert.Equal(getWebhook.CallbackUrl, updatedWebHook.CallbackUrl);

            WaitToAvoidRateLimits(3);

            //Delete Webhook
            await TrelloClient.DeleteWebhookAsync(webhookId);
            var webhooksAfterDelete = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            Assert.Equal(currentWebhooks.Count, webhooksAfterDelete.Count);

            WaitToAvoidRateLimits(3);

            //Update Webhook by URL
            await TrelloClient.AddWebhookAsync(new Webhook("ByCallBack", callbackUrl, BoardId));
            const string newCallbackUrl = "https://www.rwj.dk";
            await TrelloClient.UpdateWebhookByCallbackUrlAsync(callbackUrl, newCallbackUrl);
            var webhooksAfterChangeByCallback = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            Assert.Contains(newCallbackUrl, webhooksAfterChangeByCallback.Select(x => x.CallbackUrl));

            WaitToAvoidRateLimits(3);

            //Delete Webhook by URL
            await TrelloClient.DeleteWebhooksByCallbackUrlAsync(newCallbackUrl);
            var webhooksAfterDeleteByCallback = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            Assert.Equal(webhooksAfterChangeByCallback.Count-1, webhooksAfterDeleteByCallback.Count);

            await TrelloClient.AddWebhookAsync(new Webhook("DeleteById", callbackUrl, BoardId));
            await TrelloClient.DeleteWebhooksByTargetModelIdAsync(BoardId);
            var webhooksAfterDeleteById = await TrelloClient.GetWebhooksForCurrentTokenAsync();
            Assert.Equal(webhooksAfterDeleteByCallback.Count, webhooksAfterDeleteById.Count);
        }
        finally
        {
            await DeleteBoard();
        }
    }

    private void LogWebhook(string action, Webhook webhook)
    {
        _output.WriteLine($"'{action}' - Webhook: {webhook.Id} - Name: {webhook.Description} - CallbackUrl: {webhook.CallbackUrl} - Active: {webhook.Active}");
    }
}