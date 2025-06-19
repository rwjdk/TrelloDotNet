[Back to Custom Field Features](TrelloClient#custom-field-features)

Get Custom Fields of a Board

## Signature
```cs
/// <summary>
/// Get Custom Fields of a Board
/// </summary>
/// <param name="boardId">Id of the Board (long version)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of CustomFields</returns>
public async Task<List<CustomField>> GetCustomFieldsOnBoardAsync(string boardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "641ddde2e37dc99ab1ccc988";
List<CustomField> customFieldsOnBoardAsync = await _trelloClient.GetCustomFieldsOnBoardAsync(boardId);
```