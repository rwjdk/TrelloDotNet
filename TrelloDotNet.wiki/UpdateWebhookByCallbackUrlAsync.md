[Back to Weebhook Features](TrelloClient#webhook-features)

Replace callback URL for one or more Webhooks

## Signature
```cs
/// <summary>
/// Replace callback URL for one or more Webhooks
/// </summary>
/// <param name="oldUrl">The old callback URL to find</param>
/// <param name="newUrl">The new callback URL to replace it with</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task UpdateWebhookByCallbackUrlAsync(string oldUrl, string newUrl, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var webhookId = "63e2892778670f4f7b7ffa2e";
Webhook webhook = await TrelloClient.GetWebhookAsync(webhookId);
string oldCallbackUrl = "https://www.host.com/api/OldFunctionTrelloWebhookEndpointReceiver";
string newCallbackUrl = "https://www.host.com/api/NewFunctionTrelloWebhookEndpointReceiver";
await TrelloClient.UpdateWebhookByCallbackUrlAsync(oldCallbackUrl, newCallbackUrl);
```