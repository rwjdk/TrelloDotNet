[Back to Automation Action List](Automation-Engine#actions)

Remove one or more Members from a card

### Input options
| Option| Description |
|:---|:---|
| `MemberIds` (Required) | Member Id's to remove | 
| `TreatMemberNameAsId` (Optional)| Set this to 'True' if you supplied names of Member Full-names instead of the Ids. While this is more convenient, it will in certain cases be slightly slower and less resilient to the renaming of things. | 

### Examples

```cs
Automation sampleAutomation = new Automation("Remove Member 'Donald Duck' when the card is moved to board from another",
    new CardMovedToBoardTrigger(),
    null, //No conditions
    new List<IAutomationAction>
    {
        new RemoveMembersFromCardAction("Donald Duck") { TreatMemberNameAsId = true }
    });
```