[Back to Automation Action List](Automation-Engine#actions)

This Automation Action adds a set of Labels to a card if they are not already present.

### Input options
| Option| Description |
|:---|:---|
| `LabelIds` | The Label-ids to add | 
| `TreatLabelNameAsId`| Set this to 'True' if you supplied the names of labels instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to the renaming of things. | 

### Examples

```cs
Automation sampleAutomation = new Automation("When Card is moved to the 'On Hold' column, Add a the Blocked Label to the card",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "On Hold") { TreatListNameAsId = true },
    null, // No conditions necessary
    new List<IAutomationAction>
    {
        new AddLabelsToCardAction("Blocked") { TreatLabelNameAsId = true}
    });
```