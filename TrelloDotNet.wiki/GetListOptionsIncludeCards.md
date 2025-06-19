Specifies the type of cards to include when retrieving lists from Trello.

## Values
| Value | Description |
| --- | --- |
| `None` | No cards are included in the response. |
| `All` | Includes all cards, including archived ones. |
| `ArchivedCards` | Includes only archived cards. |
| `OpenCards` | Includes only open cards, even if they are on archived lists. |

## Usage
Used in [GetListOptions](GetListOptions) to define the inclusion of cards in list retrieval operations.
