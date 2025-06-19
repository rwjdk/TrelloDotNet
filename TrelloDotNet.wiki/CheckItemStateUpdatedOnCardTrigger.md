[Back to Automation Trigger List](Automation-Engine#triggers)

Trigger that occurs when a Check-item on a Card Change State

### Input options
| Option| Description |
|:---|:---|
| `state` (optional) | What state should the check-items state be (or leave 'None' to catch both states) | 

### Examples

```cs
var sampleTrigger = new("Remove Warning Sticker when present and you complete last Checklist Item",
    new CheckItemStateUpdatedOnCardTrigger(ChecklistItemState.Complete), //<-- Our Trigger
    new List<IAutomationCondition>
    {
        new ChecklistItemsCompleteCondition()
    },
    new List<IAutomationAction>
    {
        new RemoveStickerFromCardAction(StickerDefaultImageId.Warning)
    }),
});
```