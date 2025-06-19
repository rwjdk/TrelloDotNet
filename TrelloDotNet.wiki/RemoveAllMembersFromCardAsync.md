[Back to Member Features](TrelloClient#member-features)

Remove all Members from a Card

## Signature
```cs
/// <summary>
/// Remove all Members from a Card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task<Card> RemoveAllMembersFromCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

Card cardWithAllMembersRemoved = await _trelloClient.RemoveAllMembersFromCardAsync(cardId);
```