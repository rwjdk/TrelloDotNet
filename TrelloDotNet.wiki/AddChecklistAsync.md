[Back to Checklist Features](TrelloClient#checklist-features)

Add a Checklist to the card

## Signature (New Checklist)
```cs
/// <summary>
/// Add a Checklist to the card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="checklist">The Checklist to add</param>
/// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true the card will be checked if a checklist with the same name (case sensitive) exists and if so return that instead of creating a new</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>New or Existing Checklist with same name</returns>
public async Task<Checklist> AddChecklistAsync(string cardId, Checklist checklist, bool ignoreIfAChecklistWithThisNameAlreadyExist = false, CancellationToken cancellationToken = default) {...}
```

## Signature (New Checklist based on Existing Checklist)
```cs
/// <summary>
/// Add a Checklist to the card based on an existing checklist (as a copy)
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="existingChecklistIdToCopyFrom">Id of an existing Checklist that should be added to the card as a new copy</param>
/// <param name="ignoreIfAChecklistWithThisNameAlreadyExist">If true the card will be checked if a checklist with same name (case sensitive) exist and if so return that instead of creating a new</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>New Checklist</returns> {...}
```

### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

var checklistItems = new List<ChecklistItem>
{
    new ChecklistItem("Item A"),
    new ChecklistItem("Item B"),
    new ChecklistItem("Item C"),
};
var checklist = new Checklist("My Cool Checklist", checklistItems);

//Create new Checklist
var checklistAdded = await _trelloClient.AddChecklistAsync(cardId, checklist);

var newChecklistWithSameName = new Checklist("My Cool Checklist", checklistItems);

//This call will NOT create a second checklist as the name is the same
var sameAsChecklistAdded = await _trelloClient.AddChecklistAsync(cardId, newChecklistWithSameName, ignoreIfAChecklistWithThisNameAlreadyExist: true);

//This call WILL create a second checklist with the same name as we do not ignore the same name
var secondAddedChecklist = await _trelloClient.AddChecklistAsync(cardId, newChecklistWithSameName, ignoreIfAChecklistWithThisNameAlreadyExist: false);

//*******************************************************************************

var otherCardId = "45345345345345dc9e9dd";

//Add a new Checklist to another card BASED ON the previously added checklist on the first card (good if you work with template cards)
await _trelloClient.AddChecklistAsync(otherCardId, checklistAdded.Id, ignoreIfAChecklistWithThisNameAlreadyExist: false);

```