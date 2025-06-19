[Back to Label Features](TrelloClient#label-features)

Get a list of Labels defined for a board

## Signature
```cs
/// <summary>
/// Get a list of Labels defined for a board
/// </summary>
/// <param name="boardId">Id of the Board (in its long or short version)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Labels</returns>
public async Task<List<Label>> GetLabelsOfBoardAsync(string boardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
            
List<Label> allLabelsOnBoard = await _trelloClient.GetLabelsOfBoardAsync(boardId);
```