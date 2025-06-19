[Back to Sticker Features](TrelloClient#sticker-features)

Delete a sticker (WARNING: THERE IS NO WAY GOING BACK!!!).

## Signature
```cs
/// <summary>
/// Delete a sticker (WARNING: THERE IS NO WAY GOING BACK!!!).
/// </summary>
/// <param name="cardId">Id of Card that have the sticker</param>
/// <param name="stickerId">Id of the sticker</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteStickerAsync(string cardId, string stickerId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string cardId = "63c939a5cea0cb006dc9e9dd";
string stickerId = "3f5443a5ce54322dedc5e443";

await TrelloClient.DeleteStickerAsync(cardId, stickerId);
```