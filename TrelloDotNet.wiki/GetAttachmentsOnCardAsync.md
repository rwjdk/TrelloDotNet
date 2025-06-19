[Back to Attachment Features](TrelloClient#attachment-features)

Get Attachments on a card

## Signature
```cs
/// <summary>
/// Get Attachments on a card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Attachments</returns>
public async Task<List<Attachment>> GetAttachmentsOnCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e88c";

//Get attachments linked to a card
List<Attachment> attachments = await _trelloClient.GetAttachmentsOnCardAsync(cardId);
```