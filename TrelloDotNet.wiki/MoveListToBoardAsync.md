[Back to List Features](TrelloClient#list-features)

Move an entire list to another board

## Signature
```cs
/// <summary>
/// Move an entire list to another board
/// </summary>
/// <param name="listId">The id of the List to move</param>
/// <param name="newBoardId">The id of the board the list should be moved to [It needs to be the long version of the boardId]</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Updated List</returns>
public async Task<List> MoveListToBoardAsync(string listId, string newBoardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var listId = "63d1239e857afaa8b003c633";

var targetBoardId = "63c939a5cea0cb006dc9e88b";
List list = await _trelloClient.MoveListToBoardAsync(listId, targetBoardId);
var newBoardId = list.BoardId; //Same as TargetBoardId
```