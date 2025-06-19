[Back to Automation Action List](Automation-Engine#actions)

Remove one or more Labels from a card

### Input options
| Option| Description |
|:---|:---|
| `LabelsIds` (Required) | Label Id's to remove | 
| `TreatLabelNameAsId` (Optional) | Set this to 'True' if you supplied the names of labels instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to the renaming of things. | 

### Examples

```cs
Automation sampleAutomation = new Automation("Remove Labels 'Blocked' and 'Backlog' when the card is moved to board from another",
    new CardMovedToBoardTrigger(),
    null, //No conditions
    new List<IAutomationAction>
    {
        new RemoveLabelsFromCardAction("Blocked", "Backlog") { TreatLabelNameAsId = true }
    });
```