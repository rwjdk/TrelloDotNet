[Back to Automation Action List](Automation-Engine#actions)

Action to remove one or more field values on a card

### Input options
| Option| Description |
|:---|:---|
| `DataToRemove` (Required) | A list of data to remove (`StartDate`, `DueDate`, `DueComplete`, `Description`, `AllLabels`, `AllMembers`, `AllChecklists`, `AllAttachments`, `AllComments`, `Cover` and/or `AllStickers`) | 

### Examples

```cs
Automation sampleAutomation = new Automation("Remove all Previous Members, Labels and Start/Due Dates when card is moved to board from another",
    new CardMovedToBoardTrigger(),
    null, //No conditions
    new List<IAutomationAction>
    {
        new RemoveCardDataAction(
            RemoveCardDataType.AllMembers, 
            RemoveCardDataType.AllLabels, 
            RemoveCardDataType.StartDate,
            RemoveCardDataType.DueDate)
    });
```