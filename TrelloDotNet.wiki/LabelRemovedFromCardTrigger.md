[Back to Automation Trigger List](Automation-Engine#triggers)

A Trigger that occurs when a Card has a label removed

### Input options
| Option| Description |
|:---|:---|
| `Constraint` | Constraint of the trigger (`AnyOfTheseLabelsAreRemoved`, `AnyButTheseLabelsAreRemoved`, `AnyLabel`)| 
| `LabelIds` | The Ids of the label or Labels to check. Tip: These can be Label-names instead of Ids if you set 'TreatLabelNameAsId' to True | 
| `TreatLabelNameAsId` | Set this to 'True' if you supplied the names of labels instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to the renaming of things. | 

### Examples

```cs
var sampleTrigger = new("Remove 'Bug Checklist' when label 'Bug' is removed from the card",
    new LabelRemovedFromCardTrigger(LabelRemovedFromCardTriggerConstraint.AnyOfTheseLabelsAreRemoved, "Bug") {TreatLabelNameAsId = true},
    null, //No condition
    new List<IAutomationAction>
    {
        new RemoveChecklistFromCardAction("Bug Checklist")
    });
```