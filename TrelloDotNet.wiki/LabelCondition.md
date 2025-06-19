[Back to Automation Condition List](Automation-Engine#conditions)

A condition that checks labels on a Card is present/not present

### Input options
| Option| Description |
|:---|:---|
| `Constraint` (Required) | The constraints of the condition (`AnyOfThesePresent`, `NoneOfTheseArePresent`, `AllOfThesePresent` or `NonePresent` )| 
| `LabelIds` (Required)| The Ids of the label or Labels to check. Tip: These can be Label-names instead of Ids if you set 'TreatLabelNameAsId' to True | 
| `TreatLabelNameAsId` (Optional)| Set this to 'True' if you supplied the names of labels instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to the renaming of things. | 

### Examples

``` cs
Automation sampleAutomation = new Automation("A Definition of Done if Card is moved to 'In Progress' and have the 'Feature' or 'DRQ' Label on it",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "In Progress") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new LabelCondition(LabelConditionConstraint.AnyOfThesePresent, "Feature", "DRQ") {TreatLabelNameAsId = true } // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new AddChecklistToCardAction(new Checklist("Definition of Done", new List<ChecklistItem>
        {
            new ChecklistItem("Review"),
            new ChecklistItem("Unit-test"),
            new ChecklistItem("Write Documentation")
        }))
    });
```