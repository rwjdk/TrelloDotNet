[Back to Sticker Features](TrelloClient#sticker-features)

Get a Sticker with a specific Id

## Signature
```cs
/// <summary>
/// Get a Sticker with a specific Id
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="stickerId">Id of the Sticker</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Sticker</returns>
public async Task<Sticker> GetStickerAsync(string cardId, string stickerId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string cardId = "63c939a5cea0cb006dc9e9dd";
string stickerId = "3f5443a5ce54322dedc5e443"; //Reality check: Rare that you know this without getting it from a card so method is rarely used but is here for completeness
Sticker sticker = await _trelloClient.GetStickerAsync(cardId, stickerId);
```