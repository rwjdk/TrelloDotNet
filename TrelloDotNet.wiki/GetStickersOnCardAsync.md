[Back to Sticker Features](TrelloClient#sticker-features)

Get the List of Stickers on a card

## Signature
```cs
/// <summary>
/// Get the List of Stickers on a card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The List of Stickers</returns>
public async Task<List<Sticker>> GetStickersOnCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string cardId = "63c939a5cea0cb006dc9e9dd";
List<Sticker> stickers = await _trelloClient.GetStickersOnCardAsync(cardId);
```