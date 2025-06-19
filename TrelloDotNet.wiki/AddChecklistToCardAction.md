[Back to Automation Action List](Automation-Engine#actions)

This Automation Action adds a specific Checklist to a card if it is not already present.

### Input options
| Option| Description |
|:---|:---|
| `ChecklistToAdd` | The Checklist Object to Add (if a checklist with the same name is not already present) | 
| `AddCheckItemsToExistingChecklist` (Optional) | By default a checklist is only added if it does not already exist. This determines if it already exists if the check items should be added to the existing list or not | 
| ``| | 

### Examples

```cs
Automation sampleAutomation = new Automation("Add a 'Definition of done' Checklist to card if it is moved to in progress list and has either the 'Backend' or 'Frontend' Label",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "In Progress") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new LabelCondition(LabelConditionConstraint.AnyOfThesePresent, "FrontEnd", "BackEnd") { TreatLabelNameAsId = true }
    },
    new List<IAutomationAction>
    {
        new AddChecklistToCardAction(new Checklist("Definition of Done", new List<ChecklistItem> //<-- Our Action
        {
            new ChecklistItem("Write Unit-tests"),
            new ChecklistItem("Update Documentation"),
        }))
    });
```