[Back to Automation Action List](Automation-Engine#actions)

Add a Comment to the Card

### Input options
| Option| Description |
|:---|:---|
| `Comment` (Required) | Comment to add | 

### Examples

```cs
Automation sampleAutomation = new Automation("When Card is moved to the 'Done' column, Add a @Reply comment to the sales team member.",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Done") { TreatListNameAsId = true },
    null, // No conditions necessary
    new List<IAutomationAction>
    {
        new AddCommentToCardAction("@SalesTeamUser The work is now done :-)") //<-- Our Action
    });

```