[Back to Card Features](TrelloClient#card-features)

Update a Card (by providing the fields you have changes for via `CardUpdate.Xyz()`)

## Signature
```cs
/// <summary>
/// Update one or more specific fields on a card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="valuesToUpdate">The Specific values to set</param>
/// <param name="cancellationToken">CancellationToken</param>
public async Task<Card> UpdateCardAsync(string cardId, List<CardUpdate> valuesToUpdate, CancellationToken cancellationToken = default)
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

//Update a Card (with new name and description and removal of Due Date)
var updateCard = await TrelloClient.UpdateCardAsync(cardId, [
    CardUpdate.Name("New Name"),
    CardUpdate.Description("New Description"),
    CardUpdate.DueDate(null),
]);
```