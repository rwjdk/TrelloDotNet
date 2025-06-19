[Back to Custom Field Features](TrelloClient#custom-field-features)

Clear a Custom field on a Card

## Signature
```cs
/// <summary>
/// Clear a Custom field on a Card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="customField">The custom Field to clear</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task ClearCustomFieldValueOnCardAsync(string cardId, CustomField customField, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "641ddde2e37dc99ab1ccc988";
List<CustomField> customFieldsOnBoardAsync = await _trelloClient.GetCustomFieldsOnBoardAsync(boardId);
var card = (await _trelloClient.GetCardsOnBoardAsync(boardId)).First(); //Grab random card

//Clear the Card of Custom Values
foreach (var customField in customFieldsOnBoardAsync)
{
    await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField);
}
```