[Back to Automation Trigger List](Automation-Engine#triggers)

Trigger when a card is added

### Input options
- None

### Examples

```cs
var automation = new Automation("Add Start date to card if it is created in the 'In Progress' List",
    new CardCreatedTrigger(), // <-- Our trigger is when a Card is Created
    new List<IAutomationCondition>
    {
        new ListCondition(ListConditionConstraint.AnyOfTheseLists, "In Progress") { TreatListNameAsId = true }
    },
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardStartFieldValue(DateTimeOffset.UtcNow))
    });
```