[Back to Automation Trigger List](Automation-Engine#triggers)

Action to remove one or more field values on a card

### Input options
- None

### Examples

```cs
var cardMovedToBoardTrigger = new Automation("Remove and Start and Due Dates (if any) when a card is move to this board from another board",
    new CardMovedToBoardTrigger(), // <-- Our trigger is when a Card is moved from another board
    null, //No condition
    new List<IAutomationAction>
    {
        new RemoveCardDataAction(
            RemoveCardDataType.StartDate,
            RemoveCardDataType.DueDate
        )
    });

```