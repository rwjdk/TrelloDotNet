[Back to Cover Features](TrelloClient#cover-features)

Remove a cover from a Card

## Signature
```cs
/// <summary>
/// Remove a cover from a Card
/// </summary>
/// <param name="cardId">Id of Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Card with the removed Cover</returns>
public async Task<Card> RemoveCoverFromCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

await _trelloClient.RemoveCoverFromCardAsync(cardId);
```