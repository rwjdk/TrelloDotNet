[Back to Card Features](TrelloClient#card-features)

Get all open cards on a specific list

## Signature
```cs
/// <summary>
/// Get all open cards on a specific list
/// </summary>
/// <param name="listId">Id of the List</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Cards</returns>
public async Task<List<Card>> GetCardsInListAsync(string listId, CancellationToken cancellationToken = default) {...}
```
### Examples

- [E-Learning video](https://youtu.be/ucFGNB1R9Ic)

```cs
var listId = "63c939a5cea0cb006dc9e89d";
var cardsOnList = await _trelloClient.GetCardsInListAsync(listId);
```