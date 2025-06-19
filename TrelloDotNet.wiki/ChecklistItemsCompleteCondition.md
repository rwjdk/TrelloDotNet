[Back to Automation Condition List](Automation-Engine#conditions)

Condition that check if a Card have all its check-items complete

### Input options
- None

### Examples

```cs
Automation sampleAutomation = new Automation("Remove Warning Sticker when All Checklist items are complete and move to the 'Done' list",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Done") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new ChecklistItemsCompleteCondition() // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new RemoveStickerFromCardAction(StickerDefaultImageId.Warning)
    });
```