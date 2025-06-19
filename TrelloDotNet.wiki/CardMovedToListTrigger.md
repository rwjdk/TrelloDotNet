[Back to Automation Trigger List](Automation-Engine#triggers)

Trigger of an event that is a Card is Moved to a List

### Input options
| Option| Description |
|:---|:---|
| `Constraint` (Required) | The constraints of the Trigger (`AnyOfTheseListsAreMovedTo` or `AnyOfTheseListsAreMovedTo`) |
| `ListIds` (Required, Params) | The Ids of the Lists the trigger should evaluate. Tip: These can be List-names instead of Ids if you set ` TreatListNameAsId` to True |
| `TreatListNameAsId` (Optional) | Set this to 'True' if you supplied names of Lists instead of the Ids. While this is more convenient, it will sometimes be slightly slower and less resilient to the renaming of things. | 
| `ListNameMatchCriteria` (Optional) | Defines the criteria on how to match Names (only used if TreatListNameAsId = 'True'). Default is Equal Match |

### Examples

```cs
//Version where lists and labels are defined using Names
var cardMoveToListTriggerAutomation1 = new Automation("Add a 'Definition of done' Checklist to card if it is moved to in progress list and has either the 'Backend' or 'Frontend' Label",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "In Progress"){ TreatListNameAsId = true }, // <-- Our trigger is when a Card is moved to another list
    new List<IAutomationCondition>
    {
        new LabelCondition(LabelConditionConstraint.AnyOfThesePresent, "FrontEnd", "BackEnd") { TreatLabelNameAsId = true }
    },
    new List<IAutomationAction>
    {
        new AddChecklistToCardAction(new Checklist("Definition of Done", new List<ChecklistItem>
        {
            new ChecklistItem("Write Unit-tests"),
            new ChecklistItem("Update Documentation"),
        }))
    });

//----------------------------------------------------------------------------------------------------------------

//Version where lists and labels are defined using Ids
var cardMoveToListTriggerAutomation2 = new Automation("Add a 'Definition of done' Checklist to card if it is moved to in progress list and has either the 'Backend' or 'Frontend' Label",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "63c939a5cea0cb006dc9e9dd"), // <-- Our trigger is when a Card is moved to another list
    new List<IAutomationCondition>
    {
        new LabelCondition(LabelConditionConstraint.AnyOfThesePresent, "4534533aa8b003c633", "6323923237a8b003c622")
    },
    new List<IAutomationAction>
    {
        new AddChecklistToCardAction(new Checklist("Definition of Done", new List<ChecklistItem>
        {
            new ChecklistItem("Write Unit-tests"),
            new ChecklistItem("Update Documentation"),
        }))
    });

//----------------------------------------------------------------------------------------------------------------

//Sample showing that you can react to a partial list name (here when List 'StartsWith' the word 'Done')
var cardMoveToListTriggerAutomation3 = new Automation("Set Due Completed if the card is moved to any list that has the Prefix 'Done' (example Done - June 2023)",
    new CardMovedToListTrigger(CardMovedToListTriggerConstraint.AnyOfTheseListsAreMovedTo, "Done")
    {
        TreatListNameAsId = true,
        ListNameMatchCriteria = StringMatchCriteria.StartsWith
    }, 
    new List<IAutomationCondition>
    {
        new LabelCondition(LabelConditionConstraint.AnyOfThesePresent, "4534533aa8b003c633", "6323923237a8b003c622")
    },
    new List<IAutomationAction>
    {
        new SetFieldsOnCardAction(new SetCardDueCompleteFieldValue(true))
    });
```