[Back to Checklist Features](TrelloClient#checklist-features)

Get a list of Checklists that are used on a specific card

## Signature
```cs
/// <summary>
/// Get a list of Checklists that are used on a specific card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Checklists</returns>
public async Task<List<Checklist>> GetChecklistsOnCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples
```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

var allChecklistsOnCard = await _trelloClient.GetChecklistsOnCardAsync(cardId);
```