using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.IntegrationTests;

[Collection("Webhook Management")] //In own collection to not overlap other tests
public class WebhookManagementTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string? _boardId = fixture.BoardId;

    [Fact]
    public async Task WebHookCrud()
    {
        //Find current webhooks
        var currentWebhooks = await TrelloClient.GetWebhooksForCurrentTokenAsync();

        //Add Webhook
        const string callbackUrl = "https://trello.com";
        var description = Guid.NewGuid().ToString();
        var webhook = new Webhook(description, callbackUrl, _boardId);
        var addedWebHook = await TrelloClient.AddWebhookAsync(webhook);
        Assert.Equal(callbackUrl, addedWebHook.CallbackUrl);
        Assert.Equal(description, addedWebHook.Description);
        Assert.True(addedWebHook.Active);
        var webhookId = addedWebHook.Id;

        //Update Webhook
        var updatedDescription = Guid.NewGuid().ToString();
        addedWebHook.Description = updatedDescription;
        addedWebHook.Active = false;
        var updatedWebHook = await TrelloClient.UpdateWebhookAsync(addedWebHook);
        Assert.Equal(callbackUrl, updatedWebHook.CallbackUrl);
        Assert.Equal(updatedDescription, updatedWebHook.Description);
        Assert.False(addedWebHook.Active);

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

        //Delete Webhook
        await TrelloClient.DeleteWebhookAsync(webhookId);
        var webhooksAfterDelete = await TrelloClient.GetWebhooksForCurrentTokenAsync();
        Assert.Equal(currentWebhooks.Count, webhooksAfterDelete.Count);

        //Update Webhook by URL
        await TrelloClient.AddWebhookAsync(new Webhook("ByCallBack", callbackUrl, _boardId));
        const string newCallbackUrl = "https://www.rwj.dk";
        await TrelloClient.UpdateWebhookByCallbackUrlAsync(callbackUrl, newCallbackUrl);
        var webhooksAfterChangeByCallback = await TrelloClient.GetWebhooksForCurrentTokenAsync();
        Assert.Contains(newCallbackUrl, webhooksAfterChangeByCallback.Select(x => x.CallbackUrl));

        //Delete Webhook by URL
        await TrelloClient.DeleteWebhooksByCallbackUrlAsync(newCallbackUrl);
        var webhooksAfterDeleteByCallback = await TrelloClient.GetWebhooksForCurrentTokenAsync();
        Assert.Equal(webhooksAfterChangeByCallback.Count - 1, webhooksAfterDeleteByCallback.Count);

        await TrelloClient.AddWebhookAsync(new Webhook("DeleteById", callbackUrl, _boardId));
        await TrelloClient.DeleteWebhooksByTargetModelIdAsync(_boardId);
        var webhooksAfterDeleteById = await TrelloClient.GetWebhooksForCurrentTokenAsync();
        Assert.Equal(webhooksAfterDeleteByCallback.Count, webhooksAfterDeleteById.Count);
    }
}