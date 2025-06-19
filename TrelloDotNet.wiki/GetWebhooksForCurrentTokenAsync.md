[Back to Weebhook Features](TrelloClient#webhook-features)

Get Webhooks linked with the current Token used to authenticate with the API

## Signature
```cs
/// <summary>
/// Get Webhooks linked with the current Token used to authenticate with the API
/// </summary>
/// <returns>List of Webhooks</returns>
public async Task<List<Webhook>> GetWebhooksForCurrentTokenAsync(CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var webhooksForCurrentToken = await _trelloClient.GetWebhooksForCurrentTokenAsync();
foreach (var webhook in webhooksForCurrentToken)
{
    Console.WriteLine("- Webhook {0}: {1} ({2})", webjook.Id, webhook.Description, webhook.CallbackUrl);
}
```