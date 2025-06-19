Represents the types of membership a user can have within a Trello board.

## Values
| Value | Description |
| --- | --- |
| `Unknown` | Represents an undefined membership type retrieved from the Trello API. |
| `Admin` | Indicates the user has administrative privileges on the board. |
| `Normal` | Indicates the user has standard user privileges on the board. |
| `Observer` | Indicates the user has observer privileges, allowing them to view but not modify the board. |
| `Ghost` | Represents a user who has not yet joined the board. |

## Usage
This enum is utilized in methods such as [AddMemberToBoardAsync](TrelloClient#AddMemberToBoardAsync), [InviteMemberToBoardViaEmailAsync](TrelloClient#InviteMemberToBoardViaEmailAsync), and [UpdateMembershipTypeOfMemberOnBoardAsync](TrelloClient#UpdateMembershipTypeOfMemberOnBoardAsync). It is also referenced in classes like [Member](Member) and [Membership](Membership).
