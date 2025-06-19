[Back to Weebhook Features](TrelloClient#webhook-features)

Add a new Webhook

> Important: The URL that the webhook should notify Need to be a valid HTTPS URL that is reachable with a HEAD and POST requests.

## Signature
```cs
/// <summary>
/// Add a new Webhook
/// </summary>
/// <param name="webhook">The Webhook to add</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Webhook</returns>
public async Task<Webhook> AddWebhookAsync(Webhook webhook, CancellationToken cancellationToken = default) {...}
```
### Examples

- [Video E-learning](https://youtu.be/A3_B-SLBm_0)

```cs
var boardId = "63c939a5cea0cb006dc9e88b";

string description = "My first Webhook";
string callbackUrl = "https://myUrlThatCanReceiveTheWebhook";
var idOfTypeYouWishToMonitor = boardId;
var webhook = new Webhook(description, callbackUrl, idOfTypeYouWishToMonitor);
var addedWebhook = _trelloClient.AddWebhookAsync(webhook);
```