[Back to Card Features](TrelloClient#card-features)

Archive all cards on in a List

## Signature
```cs
/// <summary>
/// Archive all cards on in a List
/// </summary>
/// <param name="listId">The id of the List that should have its cards archived</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task ArchiveAllCardsInListAsync(string listId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string idOfListYouWishToArchiveAllcardsOn = "53c93955ceaa0cb006dc3e9dd";

await _trelloClient.ArchiveAllCardsInListAsync(idOfListYouWishToArchiveAllcardsOn);
```