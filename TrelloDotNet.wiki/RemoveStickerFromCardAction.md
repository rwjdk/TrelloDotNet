[Back to Automation Action List](Automation-Engine#actions)

This Automation Action removes a specific Sticker from a card based on imageId

### Input options
| Option| Description |
|:---|:---|
| `StickerImageIdToRemove` | ImageId of Sticker to Remove | 

### Examples

```cs
Automation sampleAutomation = new Automation("Remove warning Sticker when present and you complete last Checklist Item",
    new CheckItemStateUpdatedOnCardTrigger(ChecklistItemState.Complete),
    new List<IAutomationCondition>
    {
        new ChecklistItemsCompleteCondition()
    },
    new List<IAutomationAction>
    {
        new RemoveStickerFromCardAction(StickerDefaultImageId.Warning)
    });
```