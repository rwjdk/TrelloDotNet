[Back to Automation Action List](Automation-Engine#actions)

This Automation Action Removes a Checklist from a Card if it is present

### Input options
| Option| Description |
|:---|:---|
| `ChecklistNameToRemove` (Required) | Name of the Checklist to remove | 

### Examples

```cs
Automation sampleAutomation = new Automation("Remove Bug DoD Checklist when Label 'Bug' is removed from the card",
    new LabelRemovedFromCardTrigger(LabelRemovedFromCardTriggerConstraint.AnyOfTheseLabelsAreRemoved, "Bug") { TreatLabelNameAsId = true},
    null, //No conditions
    new List<IAutomationAction>
    {
        new RemoveChecklistFromCardAction("Bug Definition of Done")
    });
```