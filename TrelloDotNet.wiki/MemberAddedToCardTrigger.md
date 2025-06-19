[Back to Automation Trigger List](Automation-Engine#triggers)

A Trigger that occurs when a Card gets a new Member Added

### Input options
| Option| Description |
|:---|:---|
| `Constraint` | The Constraint of the Trigger (`AnyOfTheseMembersAreAdded`, `AnyButTheseMembersAreAreAdded` or `AnyMember`)| 
| `MemberIds`| The Ids of the Member or Members to check. Tip: These can be Member-usernames instead of Ids if you set 'TreatMemberNameAsId' to True | 
| `TreatMemberNameAsId`| Set this to 'True' if you supplied the usernames of Members instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to renaming things. | 

### Examples

```cs
var sampleTrigger = new("When a member is added to a card that is in 'Backlog' list, set the card's Due Date 7 days from now",
    new MemberAddedToCardTrigger(MemberAddedToCardTriggerConstraint.AnyMember),
    new List<IAutomationCondition>
    {
        new ListCondition(ListConditionConstraint.AnyOfTheseLists, "Backlog") { TreatListNameAsId = true }
    },
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardDueFieldValue(DateTimeOffset.UtcNow.AddDays(7)))
    });
```