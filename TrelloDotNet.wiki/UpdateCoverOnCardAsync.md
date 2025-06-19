[Back to Cover Features](TrelloClient#cover-features)

Update a Cover to a card (this is equivalent to AddCoverToCardAsync, but here for discoverability. 

> Tip: It is also possible to update the cover via [`UpdateCardAsync`](UpdateCardAsync)) 

## Signature
```cs
/// <summary>
/// Update a Cover to a card (this is equivalent to AddCoverToCardAsync, but here for discover-ability. Tip: It is also possible to update the cover via UpdateCardAsync)
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="newCover">The new Cover</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task<Card> UpdateCoverOnCardAsync(string cardId, CardCover newCover, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

var differentCover = new CardCover(CardCoverColor.Purple, CardCoverSize.Normal);
Card cardWithCover = await _trelloClient.UpdateCoverOnCardAsync(cardId, differentCover);
```