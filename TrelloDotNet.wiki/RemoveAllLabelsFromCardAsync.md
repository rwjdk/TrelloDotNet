[Back to Label Features](TrelloClient#label-features)

Remove all Labels from a Card

## Signature
```cs
/// <summary>
/// Remove all Labels from a Card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task<Card> RemoveAllLabelsFromCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

//Remove all labels
Card cardWithAllLabelsRemoved = await _trelloClient.RemoveAllLabelsFromCardAsync(cardId);
```