[Back to Checklist Features](TrelloClient#checklist-features)

Get a Checklist with a specific Id

## Signature
```cs
/// <summary>
/// Get a Checklist with a specific Id
/// </summary>
/// <param name="checkListId">Id of the Checklist</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Checklist</returns>
public Task<Checklist> GetChecklistAsync(string checkListId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var checklistId = "45345345dedd3e33c9e9dd";

var checklist = await _trelloClient.GetChecklistAsync(checklistId);
```