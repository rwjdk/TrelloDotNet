[Back to Cover Features](TrelloClient#cover-features)

Add a Cover to a card.

> Tip: It is also possible to add the cover via [`UpdateCardAsync`](UpdateCardAsync)) 

## Signature
```cs
/// <summary>
/// Add a Cover to a card. Tip: It is also possible to add the cover via UpdateCardAsync
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="coverToAdd">The Cover to Add</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task<Card> AddCoverToCardAsync(string cardId, CardCover coverToAdd, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

Card cardWithCover = await _trelloClient.AddCoverToCardAsync(cardId, new CardCover(CardCoverColor.Purple, CardCoverSize.Normal));
```