[Back to Board Features](TrelloClient#board-features)

Update a Board

## Signature
```cs
/// <summary>
/// Update a Board
/// </summary>
/// <param name="boardWithChanges">The board with the changes</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Updated Card</returns>
public async Task<Board> UpdateBoardAsync(Board boardWithChanges, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
Board board = await _trelloClient.GetBoardAsync(boardId);
board.Name = "New name";
board.Description = "New Description";
Board updatedBoard = await _trelloClient.UpdateBoardAsync(board);
```