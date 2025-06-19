[Back to Sticker Features](TrelloClient#sticker-features)

Add a sticker to a card

## Signature
```cs
/// <summary>
/// Add a sticker to a card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="sticker">The Sticker to add</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The new sticker</returns>
public async Task<Sticker> AddStickerToCardAsync(string cardId, Sticker sticker, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

Sticker sticker = await _trelloClient.AddStickerToCardAsync(cardId, new Sticker(StickerDefaultImageId.Warning));
```