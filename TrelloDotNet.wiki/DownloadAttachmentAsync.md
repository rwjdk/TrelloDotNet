[Back to Attachment Features](TrelloClient#attachment-features)

## Signature
```cs
/// <summary>
/// Download an Attachment
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="attachmentId">Id of Attachment</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns></returns>
public async Task<Stream> DownloadAttachmentAsync(string cardId, string attachmentId, CancellationToken cancellationToken = default) { ... }
```

or

```cs
/// <summary>
/// Download an Attachment
/// </summary>
/// <param name="url">URL of the attachment</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns></returns>
public async Task<Stream> DownloadAttachmentAsync(string url, CancellationToken cancellationToken = default) { ... }
```

### Examples

```cs
//Download all Attachments on a Card
var cardId = "6725ecb5bd6ed225c2ec21aa";
var attachments = await client.GetAttachmentsOnCardAsync(cardId);
foreach (Attachment attachment in attachments)
{
    Stream stream = await client.DownloadAttachmentAsync(cardId, attachment.Id);
    await using var fileStream = File.Create("C:\\Temp\\" + attachment.FileName);
    await stream.CopyToAsync(fileStream);
}
```