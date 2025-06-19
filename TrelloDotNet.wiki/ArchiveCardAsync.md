[Back to Card Features](TrelloClient#card-features)

Archive (Close) a Card

## Signature
```cs
/// <summary>
/// Archive (Close) a Card
/// </summary>
/// <param name="cardId">The id of card that should be archived</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Archived Card</returns>
public async Task<Card> ArchiveCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";
await _trelloClient.ArchiveCardAsync(cardId);
```