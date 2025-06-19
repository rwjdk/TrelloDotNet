[Back to Automation Action List](Automation-Engine#actions)

This Automation Action adds Checklists to cards if it is not already present based on what Labels are present on the card.

### Input options
| Option| Description |
|:---|:---|
| `AddChecklistActionsIfLabelsMatch` (Required) | A set of labels and checklist Actions to apply if one or more of the labels are on the card | 

### Examples

```cs
Automation automation = new Automation("Add DoD to Card when moved to the 'In Progress' column.",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "In Progress") { TreatListNameAsId = true },
    null, // No conditions necessary
    new List<IAutomationAction>
    {
        new AddChecklistToCardIfLabelMatchAction(
            new AddChecklistToCardIfLabelMatch("Frontend", new AddChecklistToCardAction(
                new Checklist("Frontend DoD", new List<ChecklistItem>
                {
                    new ChecklistItem("Frontend item 1"),
                    new ChecklistItem("Frontend item 2"),
                    new ChecklistItem("Frontend item 3"),
                }))) {TreatLabelNameAsId = true},
            new AddChecklistToCardIfLabelMatch("Backend", new AddChecklistToCardAction(
                new Checklist("Backend DoD", new List<ChecklistItem>
                {
                    new ChecklistItem("Backend item 1"),
                    new ChecklistItem("Backend item 2"),
                    new ChecklistItem("Backend item 3"),
                }))) { TreatLabelNameAsId = true}
        )
    });
```