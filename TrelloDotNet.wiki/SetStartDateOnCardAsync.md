[Back to Card Features](TrelloClient#card-features)

Set Due Date on a card

## Signature
```cs
/// <summary>
/// Set Due Date on a card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="startDate">The Start Date (In UTC Time)</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task<Card> SetStartDateOnCardAsync(string cardId, DateTimeOffset startDate, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";
var cardWithStartDate = await _trelloClient.SetStartDateOnCardAsync(cardId, DateTimeOffset.Now); //Set start date to now
```