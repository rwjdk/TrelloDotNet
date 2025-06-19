[Back to Automation Action List](Automation-Engine#actions)

This Automation Action adds a specific Sticker to a card if it is not already present.

> This is often used to warn about something irregular on a Card, for Example when it is moved to 'Done'

### Input options
| Option| Description |
|:---|:---|
| `StickerToAdd` (Required) | The Sticker object to add (if a sticker with same id is not already present) | 

### Examples

```cs
Automation sampleAutomation = new Automation("Add Warning Sticker if non Support cards is moved to review or further Lists but any Checklist is not done (all entries are not marked as complete)",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Review", "Test", "Deploy", "Done") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new ChecklistItemsIncompleteCondition(),
        new LabelCondition(LabelConditionConstraint.NoneOfTheseArePresent, "Support") { TreatLabelNameAsId = true }
    },
    new List<IAutomationAction>
    {
        new AddStickerToCardAction(new Sticker(StickerDefaultImageId.Warning))
    });
```