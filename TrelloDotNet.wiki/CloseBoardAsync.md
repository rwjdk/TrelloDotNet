[Back to Board Features](TrelloClient#board-features)

Close (Archive) a Board

## Signature
```cs
/// <summary>
/// Close (Archive) a Board
/// </summary>
/// <param name="boardId">The id of board that should be closed</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Closed Board</returns>
public async Task<Board> CloseBoardAsync(string boardId, CancellationToken cancellationToken = default){...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
Board closedBoard = await _trelloClient.CloseBoardAsync(boardId);
```