[Back to Checklist Features](TrelloClient#checklist-features)

Get a list of Checklists that are used on cards on a specific Board

## Signature
```cs
/// <summary>
/// Get a list of Checklists that are used on cards on a specific Board
/// </summary>
/// <param name="boardId">Id of the Board (in its long or short version)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Checklists</returns>
public async Task<List<Checklist>> GetChecklistsOnBoardAsync(string boardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";

var allChecklistsOnBoardRegardlessOfWhatCard = await _trelloClient.GetChecklistsOnBoardAsync(boardId);
```