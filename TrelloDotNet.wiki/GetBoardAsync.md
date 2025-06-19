[Back to Board Features](TrelloClient#board-features)

Get a Board by its Id

## Signature
```cs
/// <summary>
/// Get a Board by its Id
/// </summary>
/// <param name="boardId">Id of the Board (in its long or short version)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Board</returns>
public async Task<Board> GetBoardAsync(string boardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
Board board = await _trelloClient.GetBoardAsync(boardId);
```