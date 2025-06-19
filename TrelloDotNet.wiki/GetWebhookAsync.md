[Back to Weebhook Features](TrelloClient#webhook-features)

Get a Webhook from its Id

## Signature
```cs
/// <summary>
/// Get a Webhook from its Id
/// </summary>
/// <param name="webhookId">Id of the Webhook</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Webhook</returns>
public async Task<Webhook> GetWebhookAsync(string webhookId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var webhookId = "3423343ffe344ea0cb0c9e565";
Webhook webhook = await _trelloClient.GetWebhookAsync(webhookId);
```