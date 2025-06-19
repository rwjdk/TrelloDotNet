[Back to Card Features](TrelloClient#card-features)

Get a Card by its Id 

> Tip: Use [this video](https://youtu.be/aWYEg1wPVYY) to find the Id of a Card

### Signature
```cs
/// <summary>
/// Get Card by its Id
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Card</returns>
public async Task<Card> GetCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```

### Examples

- [E-Learning video](https://youtu.be/ucFGNB1R9Ic)

```cs
//Get a card with a specific Id
string cardId = "342423423423423";
TrelloDotNet.Model.Card card = await _trelloClient.GetCardAsync(cardId);
```