[Back to Automation Action List](Automation-Engine#actions)

This Automation Action adds a set of Members to a card if they are not already present.

### Input options
| Option| Description |
|:---|:---|
| `MemberIds` (Required) | The Member-ids to add | 
| `TreatMemberNameAsId` (Optional) | TreatMemberNameAsId | 

### Examples

```cs
Automation sampleAutomation = new Automation("When Card is moved to the 'Testing' column, Add a member 'Mike Tester' to the card",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Testing") { TreatListNameAsId = true },
    null, // No conditions necessary
    new List<IAutomationAction>
    {
        new AddMembersToCardAction("Mike Tester") { TreatMemberNameAsId = true}
    });
```