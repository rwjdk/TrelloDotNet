[Back to Automation Condition List](Automation-Engine#conditions)

A condition that checks Members on a Card is present/not present

### Input options
| Option| Description |
|:---|:---|
| `Constraint` (Required) | The constraints of the condition (`AnyOfThesePresent`, `NoneOfTheseArePresent`, `AllOfThesePresent` or `NonePresent`) | 
| `MemberIds` (Required) | The Ids of the Member or Members to check. Tip: These can be Member-names instead of Ids if you set 'TreatMemberNameAsId' to True | 
| `TreatMemberNameAsId` (Optional) | Set this to 'True' if you supplied the names of members instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to the renaming of things. | 

### Examples

```cs
Automation sampleAutomation = new Automation("Add a Warning sticker if a Card is move to 'In Progress' but no Member is present on the card",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "In Progress") { TreatListNameAsId = true },
    new List<IAutomationCondition>
    {
        new MemberCondition(MemberConditionConstraint.NonePresent) // <-- Our condition
    },
    new List<IAutomationAction>
    {
        new AddStickerToCardAction(new Sticker(StickerDefaultImageId.Warning))
    });
```