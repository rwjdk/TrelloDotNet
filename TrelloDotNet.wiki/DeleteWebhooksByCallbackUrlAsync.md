[Back to Weebhook Features](TrelloClient#webhook-features)

Delete Webhooks using indicated Callback URL (WARNING: THERE IS NO WAY GOING BACK!!!).

## Signature
```cs
/// <summary>
/// Delete Webhooks using indicated Callback URL (WARNING: THERE IS NO WAY GOING BACK!!!).
/// </summary>
/// <param name="callbackUrl">The URL of the callback URL</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteWebhooksByCallbackUrlAsync(string callbackUrl, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string callbackUrl = "https://myUrlThatCanReceiveTheWebhook";
await _trelloClient.DeleteWebhooksByCallbackUrlAsync(callbackUrl);
```