[Back to Board Features](TrelloClient#board-features)

Add a new Board

## Signature
```cs
/// <summary>
/// Add a new Board
/// </summary>
/// <param name="board">The Board to Add</param>
/// <param name="options">Options for the new board</param>
/// <param name="cancellationToken"></param>
/// <returns>The New Board</returns>
public async Task<Board> AddBoardAsync(Board board, AddBoardOptions options = null, CancellationToken cancellationToken = default){...}
```
### Examples

```cs
//Create a board in default workspace (Organization)
Board defaultBoard = await _trelloClient.AddBoardAsync(new Board("My Board Name", "My optional Board Description"));

//Create a board with options (specific workspace and no default labels or lists)
var boardOptions = new AddBoardOptions
{
    WorkspaceId = "63c939a5cea0cb006dc9e89d", //Id of the Organization
    DefaultLabels = false,
    DefaultLists = false
};
Board blankBoard = await _trelloClient.AddBoardAsync(new Board("My Second Board"), boardOptions);
```