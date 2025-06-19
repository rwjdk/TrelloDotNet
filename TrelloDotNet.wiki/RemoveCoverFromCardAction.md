[Back to Automation Action List](Automation-Engine#actions)

This Automation Action Removes a Cover from a card

### Input options
- None

### Examples

```cs
Automation sampleAutomation = new Automation("Remove Cover when the card is moved to board from another",
    new CardMovedToBoardTrigger(),
    null, //No conditions
    new List<IAutomationAction>
    {
        new RemoveCoverFromCardAction()
    });
```