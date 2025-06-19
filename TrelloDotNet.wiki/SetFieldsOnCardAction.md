[Back to Automation Action List](Automation-Engine#actions)

Action to set one or more fields on a card

### Input options
| Option| Description |
|:---|:---|
| `FieldValues` (Required) | List of field-values to set (You can set 'Name', 'Description', 'start Date', 'Due Date' and/or 'Due Complete') | 

### Examples

```cs
Automation sampleAutomation = new("Add Start Date to Card when moved to any of the Work in Progress lists",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "In Progress", "Review", "Test", "Deploy") { TreatListNameAsId = true },
    null,
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardStartFieldValue(DateTimeOffset.UtcNow))
    });
```