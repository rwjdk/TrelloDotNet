[Back to Automation Trigger List](Automation-Engine#triggers)

A Trigger that occurs when a Card gets a new label Added

### Input options
| Option| Description |
|:---|:---|
| `Constraint` (Required) | The Constraint of the Trigger (`AnyOfTheseLabelsAreAdded`, `AnyButTheseLabelsAreAreAdded` or `AnyLabel`)| 
| `LabelIds` (Optional) | The Ids of the label or Labels to check. Tip: These can be Label-names instead of Ids if you set 'TreatLabelNameAsId' to True | 
| `TreatLabelNameAsId` (Optional) | Set this to 'True' if you supplied the names of labels instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to renaming things. | 

### Examples

```cs
var sampleTrigger = new("Add DoD if a card is already on a 'In Progress' list and has any of the registered labels added.",
    new LabelAddedToCardTrigger(LabelAddedToCardTriggerConstraint.AnyOfTheseLabelsAreAdded, "4534533aa8b003c633", "6323923237a8b003c622"),
    new List<IAutomationCondition>
    {
        new ListCondition(ListConditionConstraint.AnyOfTheseLists, "In Progress", "Review") { TreatListNameAsId = true}
    },
    new List<IAutomationAction>
    {
        new AddChecklistToCardAction(new Checklist("Defintion of Done", new List<ChecklistItem>
            {
                new ChecklistItem("DoD item 1"),
                new ChecklistItem("DoD item 2"),
                new ChecklistItem("DoD item 3")
            }
            )
        )
    });
```