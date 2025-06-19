[Back to Board Features](TrelloClient#board-features)

Delete an entire board

## Signature
```cs
/// <summary>
/// Delete an entire board (WARNING: THERE IS NO WAY GOING BACK!!!). Alternative use CloseBoard() for non-permanency
/// </summary>
/// <remarks>
/// As this is a major thing, there is a secondary confirm needed by setting: Options.AllowDeleteOfBoards = true
/// </remarks>
/// <param name="boardId">The id of the Board to Delete</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteBoardAsync(string boardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
try
{
      _trelloClient.Options.AllowDeleteOfBoards = true; //If you do not set this to true, DeleteBoardAsync will throw an exception
     var boardId = "63c939a5cea0cb006dc9e88c";
     await _trelloClient.DeleteBoardAsync(boardId);
}
finally
{
    _trelloClient.Options.AllowDeleteOfBoards = false;
}
```