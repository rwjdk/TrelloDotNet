[Back to Board Features](TrelloClient#board-features)

Get the Boards that the token provided to the TrelloClient can Access

## Signature
```cs
/// <summary>
/// Get the Boards that the token provided to the TrelloClient can Access
/// </summary>
/// <returns>The Active Boards there is access to</returns>
public async Task<List<Board>> GetBoardsCurrentTokenCanAccessAsync(CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
List<Board> boardsForTokenMember = await _trelloClient.GetBoardsCurrentTokenCanAccessAsync();
```