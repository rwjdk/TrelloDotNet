﻿using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.IntegrationTests;

public class WebhookManagementTests : TestBaseWithNewBoard
{
    [Fact]
    public async Task WebHookCrud()
    {
        try
        {
            await CreateNewBoard();
            //Find current webhooks
            var currentWebhooks = await TrelloClient.GetWebhooksForCurrentTokenAsync();

            //Add Webhook
            var callbackUrl = "https://www.trello.com";
            var description = "Webhook1";
            var webhook = new Webhook(description, callbackUrl, BoardId);
            var addedWebHook = await TrelloClient.AddWebhookAsync(webhook);
            Assert.Equal(callbackUrl, addedWebHook.CallbackUrl);
            Assert.Equal(description, addedWebHook.Description);
            Assert.True(addedWebHook.Active);
            var webhookId = addedWebHook.Id;

            //Update Webhook
            var updatedDescription = "Webhook2";
            addedWebHook.Description = updatedDescription;
            addedWebHook.Active = false;
            var updatedWebHook = await TrelloClient.UpdateWebhookAsync(addedWebHook);
            Assert.Equal(callbackUrl, updatedWebHook.CallbackUrl);
            Assert.Equal(updatedDescription, updatedWebHook.Description);
            Assert.False(addedWebHook.Active);

            WaitToAvoidRateLimits(3);

            //Check lists of webhooks are update
            var webhooksAfterAdd = await TrelloClient.GetWebhooksForCurrentTokenAsync();
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
            var newCallbackUrl = "https://bing.com";
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
}