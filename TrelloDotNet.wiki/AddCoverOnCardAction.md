[Back to Automation Action List](Automation-Engine#actions)

This Automation Action adds/updates a Cover on a card

### Input options
| Option| Description |
|:---|:---|
| `CardCoverToAdd` (Required) | The Cover object to add/update | 

### Examples

```cs
Automation sampleAutomation = new Automation("When Card is moved to the 'On Hold' column, Add a Full red cover to highlight the card",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "On Hold") { TreatListNameAsId = true },
    null, // No conditions necessary
    new List<IAutomationAction>
    {
        new AddCoverOnCardAction(new CardCover(CardCoverColor.Red, CardCoverSize.Full)) //<-- Our Action
    });
```