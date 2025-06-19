[Back to Weebhook Features](TrelloClient#webhook-features)

Update a webhook

## Signature
```cs
/// <summary>
/// Update a webhook
/// </summary>
/// <param name="webhookWithChanges">The Webhook with changes</param>
/// <param name="cancellationToken"></param>
/// <returns>The Updated Webhook</returns>
public async Task<Webhook> UpdateWebhookAsync(Webhook webhookWithChanges, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
Webhook webhook = await _trelloClient.GetWebhookAsync("63e2892778670f4f7b7ffa2e");
webhook.CallbackUrl = "https://www.host.com/api/NewTrelloWebhookEndpointReceiverUrl";
var updatedWebhook = await _trelloClient.UpdateWebhookAsync(webhook);
```