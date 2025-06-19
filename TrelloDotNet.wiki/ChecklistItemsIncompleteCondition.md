[Back to Automation Condition List](Automation-Engine#conditions)

Condition that check if a Card have one or more incomplete check-items on any of its checklists

### Input options
- None

### Examples

```cs
Automation sampleAutomation = new Automation("Add Warning Sticker when card moved to 'Done' and not all checklist are complete",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Done") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new ChecklistItemsIncompleteCondition() // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new AddStickerToCardAction(new Sticker(StickerDefaultImageId.Warning))
    });
```