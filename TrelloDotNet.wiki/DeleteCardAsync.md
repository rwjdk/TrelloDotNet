[Back to Card Features](TrelloClient#card-features)

Delete a Card (WARNING: THERE IS NO WAY GOING BACK!!!). Alternative use [ArchiveCardAsync()](ArchiveCardAsync) for non-permanency

## Signature
```cs
/// <summary>
/// Delete a Card (WARNING: THERE IS NO WAY GOING BACK!!!). Alternative use ArchiveCardAsync() for non-permanency
/// </summary>
/// <param name="cardId">The id of the Card to Delete</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";
await _trelloClient.DeleteCardAsync(cardId);
```