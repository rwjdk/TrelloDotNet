[Back to List Features](TrelloClient#list-features)

Get a specific List (Column) based on its Id

## Signature
```cs
/// <summary>
/// Get a specific List (Column) based on its Id
/// </summary>
/// <param name="listId">Id of the List</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns></returns>
public async Task<List> GetListAsync(string listId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var listId = "63d1239e857afaa8b003c633";

List list = await _trelloClient.GetListAsync(listId);
```