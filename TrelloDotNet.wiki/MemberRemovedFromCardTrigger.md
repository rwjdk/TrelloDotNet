[Back to Automation Trigger List](Automation-Engine#triggers)

A Trigger that occurs when a Card has a Member removed

### Input options
| Option| Description |
|:---|:---|
| `Constraint` | Constraint of the trigger (`AnyOfTheseMembersAreRemoved`, `AnyButTheseMembersAreRemoved` or `AnyMember` )| 
| `MemberIds`| | 
| `TreatMemberNameAsId`| Set this to 'True' if you supplied the names of Members instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to the renaming of things. | 

### Examples

```cs
var sampleTrigger = new("Add a 'Member Left' comment on the card when a member leaves a card",
    new MemberRemovedFromCardTrigger(MemberRemovedFromCardTriggerConstraint.AnyMember),
    null, //No condition
    new List<IAutomationAction>
    {
        new AddCommentToCardAction("Member Left")
    });
```