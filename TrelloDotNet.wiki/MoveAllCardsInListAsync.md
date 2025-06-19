[Back to Card Features](TrelloClient#card-features)

Move all cards of a list to another list

## Signature
```cs
/// <summary>
/// Move all cards of a list to another list
/// </summary>
/// <param name="currentListId">The id of the List that should have its cards moved</param>
/// <param name="newListId">The id of the new List that should receive the cards</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task MoveAllCardsInListAsync(string currentListId, string newListId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string idOfListToMoveAllCardsFrom = "63c939a5cea0cb006dc9e89d";
string idOfListToMoveAllCardsTo = "73c939a3223453453dc9e89d";
await _trelloClient.MoveAllCardsInListAsync(idOfListToMoveAllCardsFrom, idOfListToMoveAllCardsTo);
```