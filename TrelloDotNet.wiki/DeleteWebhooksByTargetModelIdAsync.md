[Back to Weebhook Features](TrelloClient#webhook-features)

Delete Webhooks using indicated target ModelId (WARNING: THERE IS NO WAY GOING BACK!!!).

## Signature
```cs
/// <summary>
/// Delete Webhooks using indicated target ModelId (WARNING: THERE IS NO WAY GOING BACK!!!).
/// </summary>
/// <param name="targetIdModel">The Target Model Id (example an ID of a Board)</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteWebhooksByTargetModelIdAsync(string targetIdModel, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
var idOfTypeYouWishToMonitor = boardId;
await _trelloClient.DeleteWebhooksByTargetModelIdAsync(idOfTypeYouWishToMonitor);
```