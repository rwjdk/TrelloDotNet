[Back to Weebhook Features](TrelloClient#webhook-features)

Delete a Webhook (WARNING: THERE IS NO WAY GOING BACK!!!).

## Signature
```cs
/// <summary>
/// Delete a Webhook (WARNING: THERE IS NO WAY GOING BACK!!!).
/// </summary>
/// <param name="webhookId">The id of the Webhook to Delete</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteWebhookAsync(string webhookId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string webhookId = "asdsad333l3oo33999k33";

await _trelloClient.DeleteWebhookAsync(webhookId);
```