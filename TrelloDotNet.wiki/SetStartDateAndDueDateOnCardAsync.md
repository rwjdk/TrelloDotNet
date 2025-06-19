[Back to Card Features](TrelloClient#card-features)

Set Start and Due Date on a card

## Signature
```cs
/// <summary>
/// Set Start and Due Date on a card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="startDate">The Start Date (In UTC Time)</param>
/// <param name="dueDate">The Due Date (In UTC Time)</param>
/// <param name="dueComplete">If Due is complete</param>
/// <param name="cancellationToken">Cancellation Token</param> 
public async Task<Card> SetStartDateAndDueDateOnCardAsync(string cardId, DateTimeOffset startDate, DateTimeOffset dueDate, bool dueComplete = false, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";
var startDate = DateTimeOffset.Now;
var dueDate = DateTimeOffset.Now.AddDays(7);
var cardWithStartAndDueDate = await _trelloClient.SetStartDateAndDueDateOnCardAsync(cardId, startDate, dueDate);
```