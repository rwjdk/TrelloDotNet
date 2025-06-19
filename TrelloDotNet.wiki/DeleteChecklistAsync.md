[Back to Checklist Features](TrelloClient#checklist-features)

Delete a Checklist (WARNING: THERE IS NO WAY GOING BACK!!!)

## Signature
```cs
/// <summary>
/// Delete a Checklist (WARNING: THERE IS NO WAY GOING BACK!!!)
/// </summary>
/// <param name="checklistId">The id of the Checklist to Delete</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteChecklistAsync(string checklistId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var checklistId = "34435345ea0cb006dc9e23ds";

await _trelloClient.GetChecklistsOnCardAsync(checklistId);
```