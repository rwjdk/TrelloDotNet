[Back to Checklist Features](TrelloClient#checklist-features)

Update a Check-item on a Card

## Signature
```cs
/// <summary>
/// Update a Check-item on a Card
/// </summary>
/// <param name="cardId">The Id of the Card the ChecklistItem is on</param>
/// <param name="item">The updated Check-item</param>
/// <param name="cancellationToken"></param>
/// <returns>The Updated Checklist Item</returns>
public async Task<ChecklistItem> UpdateChecklistItemAsync(string cardId, ChecklistItem item, CancellationToken cancellationToken = default) {...}
```
### Examples
```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

var checklists = await _trelloClient.GetChecklistsOnCardAsync(cardId);

var specificChecklist = checklists.FirstOrDefault(x=> x.Name == "My Cool Checklist");
if (specificChecklist != null)
{
    //Update all checklist-items on the checklist to be completed
    foreach (var checklistItem in specificChecklist.Items)
    {
        checklistItem.State = ChecklistItemState.Complete;
        await TrelloClient.UpdateChecklistItemAsync(cardId, checklistItem);
    }
}
```