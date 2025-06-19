[Back to Checklist Features](TrelloClient#checklist-features)

Delete a Checklist Item from a checklist (WARNING: THERE IS NO WAY GOING BACK!!!)

## Signature
```cs
/// <summary>
/// Delete a Checklist Item from a checklist (WARNING: THERE IS NO WAY GOING BACK!!!)
/// </summary>
/// <param name="checklistId">The id of the Checklist</param>
/// <param name="checklistItemId">The id of the Checklist Item to delete</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteChecklistItemAsync(string checklistId, string checklistItemId, CancellationToken cancellationToken = default)
```

### Examples

```cs
string checklistId = "64a86a1616a79a0f15320d85";
string checklistItemId = "64a86a1b854d3490bc6c8aa3";
await _trelloClient.DeleteChecklistItemAsync(checklistId, checklistItemId);
```