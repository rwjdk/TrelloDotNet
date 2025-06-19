[Back to Automation Condition List](Automation-Engine#conditions)

Condition that check if a Card have a checklist with a certain name and that none of the items on that list is completed (aka the Checklist have not been started)

### Input options
| Option| Description |
|:---|:---|
| `ChecklistNameToCheck` (Required) | The name of the Checklist to check | 

### Examples

```cs
Automation sampleAutomation = new Automation("Remove the 'Definition of Done' Checklist if Card is moved back to 'Backlog' and the checklist is not started",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Backlog") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new ChecklistNotStartedCondition("Definition of Done") // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new RemoveChecklistFromCardAction("Definition of Done")
    });
```