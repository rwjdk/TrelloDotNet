[Back to Automation Trigger List](Automation-Engine#triggers)

Trigger when a card is Updated (This happens in various basic events so it is kind of a catch-all Trigger to things happening on a card)

Tip: You have the option to specify an optional subtype to filter the Event for better performance further

### Input options
- None

### Examples

```cs
var sampleTrigger = new Automation("Whenever a card in 'In Progress' list is updated, make sure it has a Start-Date",
    new CardUpdatedTrigger(), // <-- Our trigger
    new List<IAutomationCondition>
    {
        new ListCondition(ListConditionConstraint.AnyOfTheseLists, "In Progress") {TreatListNameAsId = true }
    },
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardStartFieldValue(DateTimeOffset.UtcNow))
    });
```