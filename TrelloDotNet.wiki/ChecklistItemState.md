Represents the state of a checklist item, indicating whether it is complete or incomplete.

## Values
| Value | Description |
| --- | --- |
| `Unknown` | Represents an unknown state retrieved from the Trello REST API. |
| `None` | Indicates no state is assigned to the checklist item. |
| `Incomplete` | The checklist item is not completed. |
| `Complete` | The checklist item is completed. |

## Usage
This enum is utilized in various classes and methods such as [ChecklistItem](ChecklistItem), [TrelloActionDataCheckItem](TrelloActionDataCheckItem), [WebhookActionDataCheckItem](WebhookActionDataCheckItem), and [CheckItemStateUpdatedOnCardTrigger](CheckItemStateUpdatedOnCardTrigger).
