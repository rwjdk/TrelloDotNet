[Back to Card Features](TrelloClient#card-features)

Set Due Date on a card

## Signature
```cs
/// <summary>
/// Set Due Date on a card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="dueDate">The Due Date (In UTC Time)</param>
/// <param name="dueComplete">If Due is complete</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task<Card> SetDueDateOnCardAsync(string cardId, DateTimeOffset dueDate, bool dueComplete = false, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";
var cardWithDueDate = await _trelloClient.SetDueDateOnCardAsync(cardId, DateTimeOffset.Now.AddDays(2)); //Set due date 2 days from now
```