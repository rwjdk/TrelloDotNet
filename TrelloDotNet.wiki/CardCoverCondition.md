[Back to Automation Condition List](Automation-Engine#conditions)

Check if a Card has a specific Cover or not

### Input options
| Option| Description |
|:---|:---|
| `Constraint` (Required) | The constraint for the Cover (One of these: `DoesNotHaveACover`, `DoesNotHaveACoverOfTypeColor`, `DoesNotHaveACoverOfTypeImage`, `HaveACover`, `HaveACoverOfTypeColor`, `HaveACoverOfTypeImage`) | 

### Examples

```cs
Automation sampleAutomation1 = new Automation("Remove Cover if present when card is moved to 'review List'",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Review") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new CardCoverCondition(CardCoverConditionConstraint.HaveACover) // <--Our condition
    },
    new List<IAutomationAction>
    {
        new RemoveCoverFromCardAction()
    });

Automation sampleAutomation2 = new Automation("Add a start-date if cover does not have an image cover when card is moved to 'review List'",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Review") { TreatListNameAsId = true},
    new List<IAutomationCondition>
    {
        new CardCoverCondition(CardCoverConditionConstraint.DoesNotHaveACoverOfTypeImage) // <--Our condition
    },
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardStartFieldValue(DateTimeOffset.UtcNow))
    }
);
```