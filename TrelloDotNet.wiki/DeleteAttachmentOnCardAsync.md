[Back to Attachment Features](TrelloClient#attachment-features)

Delete Attachments on a card

## Signature
```cs
/// <summary>
/// Delete an Attachments on a card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="attachmentId">Id of Attachment</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteAttachmentOnCardAsync(string cardId, string attachmentId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e88c";

//Delete all attachments on a card

List<Attachment> attachments = await _trelloClient.GetAttachmentsOnCardAsync(cardId);

foreach (var attachment in attachments)
{
    await _trelloClient.DeleteAttachmentOnCardAsync(cardId, attachment.Id);
}
```