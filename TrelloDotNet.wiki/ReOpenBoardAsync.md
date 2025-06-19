[Back to Board Features](TrelloClient#board-features)

ReOpen a Board that was previously archived

## Signature
```cs
/// <summary>
/// ReOpen a Board that was previously archived
/// </summary>
/// <param name="boardId">The id of board that should be reopened</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The ReOpened Board</returns>
public async Task<Board> ReOpenBoardAsync(string boardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
Board reOpenedBoard = await _trelloClient.ReOpenBoardAsync(boardId);
```