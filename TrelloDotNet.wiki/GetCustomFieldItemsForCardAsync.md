[Back to Custom Field Features](TrelloClient#custom-field-features)

Get Custom Field Item Values for a Card

## Signature
```cs
/// <summary>
/// Get Custom Fields for a Card
/// </summary>
/// <remarks>Tip: Use Extension methods GetCustomFieldValueAsXYZ for a handy way to get values</remarks>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Custom Fields</returns>
public async Task<List<CustomFieldItem>> GetCustomFieldItemsForCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";
var customFieldItemsForCardAsync = await _trelloClient.GetCustomFieldItemsForCardAsync(cardId);
foreach (var customFieldItem in customFieldItemsForCardAsync)
{
    //Use 'customFieldItem.CustomFieldId' to determine the type of field and then use the below to get the values
    string checkboxValue = customFieldItem.Value.CheckedAsString;
    string dateValue = customFieldItem.Value.DateAsString;
    string numberValue = customFieldItem.Value.NumberAsString;
    string textvalue = customFieldItem.Value.TextAsString;
    string? listValueId = customFieldItem.ValueId;
}

//Alternative access via the Card itself
var boardId = "63c939a5cea0cb006dc9e88b";
List<CustomField>? customFieldsOnBoard = await _trelloClient.GetCustomFieldsOnBoardAsync(boardId);
CustomField customField = customFieldsOnBoard.First(x => x.Name == "Priority");
_trelloClient.Options.IncludeCustomFieldsInCardGetMethods = true;
Card? card = await _trelloClient.GetCardAsync(cardId);
CustomFieldOption option = card.CustomFieldItems.GetCustomFieldValueAsOption(customField);
var priorityDescription = option.Value.Text;
```