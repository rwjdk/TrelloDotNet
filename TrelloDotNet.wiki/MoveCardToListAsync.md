[Back to Card Features](TrelloClient#card-features)

Move a Card to a new list on the same board

## Signature
```cs
/// <summary>
/// Move a Card to a new list on the same board
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="newListId">Id of the List you wish to move it to</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns></returns>
public async Task<Card> MoveCardToList(string cardId, string newListId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string cardId = "63c939a5cea0cb006dc9e9dd";
string listIdToMoveTheCardTo = "63d128787441d05619f44dbe"; //Use 'TrelloClient.GetListsOnBoardAsync()' to find real list Ids

var movedCard = await _trelloClient.MoveCardToListAsync(cardId, listIdToMoveTheCardTo);
```