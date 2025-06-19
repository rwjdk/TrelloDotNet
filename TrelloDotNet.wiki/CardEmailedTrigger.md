[Back to Automation Trigger List](Automation-Engine#triggers)

Trigger when a card is emailed to the board

### Input options
- None

### Examples

```cs
var cardEmailedTriggerAutomation = new Automation("Add a 'From Email' Checklist to card if it is emailed to the board",
    new CardEmailedTrigger(), // <-- Our trigger is when a Card is Emailed
    null, //No Condition
    new List<IAutomationAction>
    {
        new AddChecklistToCardAction(new Checklist("Email Follow-up", new List<ChecklistItem>
        {
            new ChecklistItem("Ensure no confidential data is on this card"),
            new ChecklistItem("Follow-up on email-sender"),
        }))
    });
```