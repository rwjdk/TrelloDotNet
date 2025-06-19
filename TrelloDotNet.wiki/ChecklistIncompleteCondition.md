[Back to Automation Condition List](Automation-Engine#conditions)

Condition that checks if a Card has a checklist with a specific name and that checklist has one or more incomplete items

### Input options
| Option| Description |
|:---|:---|
| `ChecklistNameToCheck` (Required) | The name of the Checklist to check | 
| `ChecklistNameMatchCriteria` (Optional) | Defines the criteria on how to match the checklist name. Default is Equal Match | 

### Examples

```cs
Automation sampleAutomation = new Automation("Add Warning Sticker when card moved to 'Done' and the 'Definition of Done' checklist are not complete",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Done") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new ChecklistIncompleteCondition("Definition of Done") // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new AddStickerToCardAction(new Sticker(StickerDefaultImageId.Warning))
    });

```