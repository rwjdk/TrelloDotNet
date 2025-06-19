Specifies the subtype of the generic CardUpdated event, allowing differentiation between various update types.

## Values
| Value | Description |
| --- | --- |
| `NameChanged` | Indicates that the name of the card was changed. |
| `DescriptionChanged` | Indicates that the description of the card was changed. |
| `DueDateAdded` | Indicates that a due date was added to the card. |
| `DueDateChanged` | Indicates that the due date of the card was changed. |
| `DueDateMarkedAsComplete` | Indicates that the due date was marked as complete. |
| `DueDateMarkedAsIncomplete` | Indicates that the due date was marked as incomplete. |
| `DueDateRemoved` | Indicates that the due date was removed from the card. |
| `StartDateAdded` | Indicates that a start date was added to the card. |
| `StartDateChanged` | Indicates that the start date of the card was changed. |
| `StartDateRemoved` | Indicates that the start date was removed from the card. |
| `MovedToOtherList` | Indicates that the card was moved to another list. |
| `MovedHigherInList` | Indicates that the card was moved to a higher position within its list. |
| `MovedLowerInList` | Indicates that the card was moved to a lower position within its list. |
| `Archived` | Indicates that the card was archived. |
| `Unarchived` | Indicates that the card was unarchived. |

## Usage
This enum is utilized in the [CardUpdatedTrigger](CardUpdatedTrigger) class to filter specific update events.
