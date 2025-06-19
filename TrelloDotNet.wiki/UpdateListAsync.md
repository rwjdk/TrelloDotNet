[Back to List Features](TrelloClient#list-features)

Update a List

## Signature
```cs
/// <summary>
/// Update a List
/// </summary>
/// <param name="listWithChanges">The List with the changes</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Updated List</returns>
public async Task<List> UpdateListAsync(List listWithChanges, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var listId = "63d1239e857afaa8b003c633";
            
List list = await _trelloClient.GetListAsync(listId);
list.Name = "New Name";

List updatedListWithNewName = await _trelloClient.UpdateListAsync(list);
```