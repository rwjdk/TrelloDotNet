[Back to Automation Trigger List](Automation-Engine#triggers)

Trigger of an event that is a Card is Moved away from a List

### Input options
| Option| Description |
|:---|:---|
| `Constraint` (Required) | The constraints of the Trigger (`AnyOfTheseListsAreMovedAwayFrom` or `AnyButTheseListsAreMovedAwayFrom`) |
| `ListIds` (Required, Params) | The Ids of the Lists the trigger should evaluate. Tip: These can be List-names instead of Ids if you set ` TreatListNameAsId` to True |
| `TreatListNameAsId` (Optional) | Set this to 'True' if you supplied names of Lists instead of the Ids. While this is more convenient, it will sometimes be slightly slower and less resilient to the renaming of things. | 
| `ListNameMatchCriteria` (Optional) | Defines the criteria on how to match Names (only used if TreatListNameAsId = 'True'). Default is Equal Match |

### Examples

```cs
Automation sampleAutomation = new Automation("Removed 'Blocked' label when moved away from the 'Blocked' List",
        new CardMovedAwayFromListTrigger(CardMovedAwayFromListTriggerConstraint.AnyOfTheseListsAreMovedAwayFrom, "Blocked") { TreatListNameAsId = true },
        null, //No condition
        new List<IAutomationAction>
        {
            new RemoveLabelsFromCardAction("Blocked") { TreatLabelNameAsId = true }
        }
    );
```