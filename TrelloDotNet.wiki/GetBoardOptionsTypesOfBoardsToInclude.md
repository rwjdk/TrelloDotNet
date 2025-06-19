Specifies the types of boards to include in a multi-board result.

## Values
| Value | Description |
| --- | --- |
| `All` | Includes all boards, both open and closed. |
| `Closed` | Includes only closed boards. |
| `Members` | Includes boards associated with members (specific functionality undocumented). |
| `Open` | Includes only open boards. |
| `Organization` | Includes boards associated with organizations (specific functionality undocumented). |
| `Public` | Includes public boards. |
| `Starred` | Includes boards starred by the member owning the Trello token. |

## Usage
This enum is utilized in the [GetBoardOptions](GetBoardOptions) class to filter board types in methods such as [GetBoardsAsync](GetBoardsAsync) and [GetBoardsForMemberAsync](GetBoardsForMemberAsync).
