[Back to Custom Field Features](TrelloClient#custom-field-features)

Update a Custom field on a Card

>Tip: To remove a value from a custom field use [`ClearCustomFieldValueOnCardAsync`](ClearCustomFieldValueOnCardAsync)

## Signature (6 variants depending on field type)
```cs
/// <summary>
/// Update a Custom field on a Card
/// </summary>
/// <remarks>
/// Tip: To remove a value from a custom field use .ClearCustomFieldValueOnCardAsync()
/// </remarks>
/// <param name="cardId">Id of the Card</param>
/// <param name="customField">The custom Field to update</param>
/// <param name="newValue">The new value (bool, DateTimeOffset, int, decimal, CustomfieldOption or string)</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, bool newValue, CancellationToken cancellationToken = default) 
public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, DateTimeOffset newValue, CancellationToken cancellationToken = default) 
public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, int newValue, CancellationToken cancellationToken = default) 
public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, decimal newValue, CancellationToken cancellationToken = default) 
public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, CustomFieldOption newValue, CancellationToken 
cancellationToken = default) 
public async Task UpdateCustomFieldValueOnCardAsync(string cardId, CustomField customField, string newValue, CancellationToken cancellationToken = default) 
{...}
```
### Examples

```cs
var boardId = "641ddde2e37dc99ab1ccc988";
List<CustomField> customFieldsOnBoardAsync = await _trelloClient.GetCustomFieldsOnBoardAsync(boardId);
_trelloClient.Options.IncludeCustomFieldsInCardGetMethods = true;
var card = (await _trelloClient.GetCardsOnBoardAsync(boardId)).First(); //Grab random card

//Sample set all custom fields on the board (in real code only set the ones you wish)
foreach (var customField in customFieldsOnBoardAsync)
{
    switch (customField.Type)
    {
        case CustomFieldType.Checkbox:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, true); //Update Bool
            bool? boolean = card.CustomFieldItems.GetCustomFieldValueAsBoolean(customField); //Get Bool
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear Bool
            break;
        case CustomFieldType.Date:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, DateTimeOffset.Now); //Update Date
            DateTimeOffset? dateTimeOffset = card.CustomFieldItems.GetCustomFieldValueAsDateTimeOffset(customField); // Get Date
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear Date
            break;
        case CustomFieldType.List:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, customField.Options[0]); //Update ListOption
            CustomFieldOption? listOption = card.CustomFieldItems.GetCustomFieldValueAsOption(customField); //Get ListOption (as Option)
            string listOptionString = card.CustomFieldItems.GetCustomFieldValueAsString(customField); //Get ListOption as String value
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear List Option
            break;
        case CustomFieldType.Number:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, 42); //Update Integer
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, 42M); //Update Decimal
            int? numberAsInteger = card.CustomFieldItems.GetCustomFieldValueAsInteger(customField); //Get Integer
            decimal? numberAsDecimal = card.CustomFieldItems.GetCustomFieldValueAsDecimal(customField); //Get Decimal
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear number
            break;
        case CustomFieldType.Text:
            await _trelloClient.UpdateCustomFieldValueOnCardAsync(card.Id, customField, "Hello World"); //Update String
            var stringValue = card.CustomFieldItems.GetCustomFieldValueAsString(customField); //Get String
            await _trelloClient.ClearCustomFieldValueOnCardAsync(card.Id, customField); //Clear String
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}
```