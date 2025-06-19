[Back to List Features](TrelloClient#list-features)



## Signature
```cs
/// <summary>
/// Reopen a List (Send back to the board)
/// </summary>
/// <param name="listId">The id of list that should be Reopened</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Archived List</returns>
public async Task<List> ReOpenListAsync(string listId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var listId = "63d1239e857afaa8b003c633";
List archivedList = await _trelloClient.ArchiveListAsync(listId);

List unarchivedList = await _trelloClient.ReOpenListAsync(listId);
```