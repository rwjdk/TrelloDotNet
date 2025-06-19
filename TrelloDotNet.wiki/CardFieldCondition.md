[Back to Automation Condition List](Automation-Engine#conditions)

Check if a Card Field has a particular value

### Input options
| Option| Description |
|:---|:---|
| `FieldToCheck` (Required) | The Card Field to Check | 
| `Constraint` (Required) | The constraint the field should be checked on (`IsNotSet`, `IsSet` or `Value`) | 
| `Value` (Conditional) | The Value of the Field (stored as an object) | 
| `StringValueMatchCriteria` (Conditional) |  What String match-criteria should be used when Constraint is 'Value' (`Equal`, `StartsWith`, `EndsWith`, `Contains` or `RegEx`)| 
| `DateTimeOffsetValueMatchCriteria` (Conditional) | What DateTimeOffset match-criteria should be used when Constraint is 'Value' (`Equal`, `Before` or `After`) | 
| `MatchDateOnlyOnDateTimeOffsetFields` (Optional) | When checking date fields (Start and Due) indicate that the matching should only happen on Date Level (and not time as well) | 

### Examples

```cs
Automation sampleAutomation1 = new Automation("Set Due Date 3 days from now if there is a Start Date on card when moved to 'In Progress List'",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "In Progress") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new CardFieldCondition(CardField.Start, CardFieldConditionConstraint.IsSet) // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardDueFieldValue(DateTimeOffset.UtcNow.AddDays(3)))
    });

Automation sampleAutomation2 = new Automation("Add the Bug Label if the name of the card contains the word 'Bug' when created",
    new CardCreatedTrigger(),
    new List<IAutomationCondition>
    {
        new CardFieldCondition(CardField.Name, CardFieldConditionConstraint.Value, "Bug", StringMatchCriteria.Contains) // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new AddLabelsToCardAction("Bug") { TreatLabelNameAsId = true }
    });
```