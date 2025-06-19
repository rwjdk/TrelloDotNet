Represents the fields of a card that can be used in conditions for filtering or automation.

## Values
| Value | Description |
| --- | --- |
| `Name` | The name (title) field of a card. |
| `ListId` | The list ID of a card. |
| `ListName` | The name of the list a card belongs to. |
| `LabelId` | The label IDs associated with a card. |
| `LabelName` | The names of labels associated with a card. |
| `MemberId` | The member IDs associated with a card. |
| `MemberName` | The names of members associated with a card. |
| `Description` | The description field of a card. |
| `Due` | The due date of a card, regardless of completion status. |
| `DueWithNoDueComplete` | The due date of a card, excluding those marked as complete. |
| `Start` | The start date of a card. |
| `Created` | The creation date of a card. |
| `CustomField` | A custom field on the card. |
| `DueComplete` | Indicates whether the card's due date is marked as complete. |

## Usage
This enum is utilized in methods such as [AdvancedStringCondition](AdvancedStringCondition), [AdvancedNumberCondition](AdvancedNumberCondition), and [AdvancedDateTimeOffsetCondition](AdvancedDateTimeOffsetCondition) for creating conditions based on card fields.
