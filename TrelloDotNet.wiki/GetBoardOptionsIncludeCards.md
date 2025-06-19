Specifies the collection of cards to be included when retrieving board data.

## Values
| Value | Description |
| --- | --- |
| `None` | No cards should be included in the response. |
| `OpenAndArchivedCards` | Includes all cards, both open and archived. |
| `ArchivedCards` | Includes only archived cards. |
| `OpenCardsOnOpenAndArchivedLists` | Includes open cards from both open and archived lists. |
| `OpenCards` | Includes only open cards from open lists. |

## Usage
This enum is used in the [GetBoardOptions](GetBoardOptions) class to define the `IncludeCards` property.
