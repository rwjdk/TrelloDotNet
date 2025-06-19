[Back to List Features](TrelloClient#list-features)

Archive a List

> Up can get an archived list back with [`ReOpenListAsync`](ReOpenListAsync)

## Signature
```cs
/// <summary>
/// Archive a List
/// </summary>
/// <param name="listId">The id of list that should be Archived</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Archived List</returns>
public async Task<List> ArchiveListAsync(string listId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var listId = "63d1239e857afaa8b003c633";
List list = await _trelloClient.ArchiveListAsync(listId);
```