[Back to Automation Condition List](Automation-Engine#conditions)

A condition that checks if a card is on a specific list or the event involved a specific list

### Input options
| Option| Description |
|:---|:---|
| `Constraint` (Required) | The constraint of the Condition (`AnyOfTheseLists` or `NoneOfTheseLists`)| 
| `ListIds` (Required) | The Ids of the List or Lists to check. Tip: These can be List-names instead of Ids if you set 'TreatListNameAsId' to True | 
| `TreatListNameAsId` (Optional) | Set this to 'True' if you supplied names of Lists instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to the renaming of things. | 

### Examples

```cs
Automation sampleAutomation = new Automation("When a Card is Created, if it is in the 'In Progress' or 'Review' List, set its Start Date",
    new CardCreatedTrigger(),
    new List<IAutomationCondition>
    {
        new ListCondition(ListConditionConstraint.AnyOfTheseLists, "In Progress", "Review") { TreatListNameAsId = true } // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardStartFieldValue(DateTimeOffset.UtcNow))
    });
```