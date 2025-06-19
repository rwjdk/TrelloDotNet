[Back to Card Features](TrelloClient#card-features)

ReOpen (Send back to board) a Card

## Signature
```cs
/// <summary>
/// ReOpen (Send back to board) a Card
/// </summary>
/// <param name="cardId">The id of card that should be reopened</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The ReOpened Card</returns>
public async Task<Card> ReOpenCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd"; //Closed/Archived Card Id
await _trelloClient.ReOpenCardAsync(cardId);
```