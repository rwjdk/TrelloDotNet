[Back to Sticker Features](TrelloClient#sticker-features)

Update a sticker

## Signature
```cs
/// <summary>
/// Update a sticker
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="stickerWithUpdates">The Sticker to update</param>
/// <param name="cancellationToken"></param>
/// <returns>The Updated Sticker</returns>
public async Task<Sticker> UpdateStickerAsync(string cardId, Sticker stickerWithUpdates, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string cardId = "63c939a5cea0cb006dc9e9dd";
List<Sticker> stickers = await _trelloClient.GetStickersOnCardAsync(cardId);
//Grab the first sticker
Sticker? firstSticker = stickers.FirstOrDefault();

if (firstSticker != null)
{
    firstSticker.ImageId = Sticker.DefaultImageToString(StickerDefaultImageId.Heart);
    firstSticker.Rotation = 45;
    firstSticker.Left = 10;
    firstSticker.Top = 3;

    Sticker updatedSticker = await _trelloClient.UpdateStickerAsync(cardId, firstSticker);
}
```