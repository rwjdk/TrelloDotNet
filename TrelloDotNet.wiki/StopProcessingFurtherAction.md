[Back to Automation Action List](Automation-Engine#actions)

An action that will stop any further processing of automations after this one for the given Webhook Receive Request

### Input options
- None

### Examples

```cs
Automation sampleAutomation = new("Add Start Date to Card when moved to any of the Work in Progress lists (Then stop any further processing)",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "In Progress", "Review", "Test", "Deploy") { TreatListNameAsId = true },
    null,
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardStartFieldValue(DateTimeOffset.UtcNow)),
        new StopProcessingFurtherAction() //<-- STOP
    });
```