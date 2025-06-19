Represents the various fields that can be associated with a Trello card.

## Values
| Value | Description |
| --- | --- |
| `IdShort` | Id of the card in short form, unique to the specific board. |
| `BoardId` | Id of the board the card is on. |
| `Name` | Name of the card. |
| `Description` | Description of the card. |
| `Closed` | Indicates if the card is archived (closed). |
| `Position` | Position of the card in the current list. |
| `Subscribed` | Indicates if the card is watched (subscribed) by the owner of the token used against the API. |
| `ListId` | Id of the list the card belongs to. |
| `LastActivity` | Timestamp of the last activity on the card. |
| `Start` | Start date of the work on the card. |
| `Due` | Due date by which the work on the card should be finished. |
| `DueComplete` | Indicates if the card is marked as completed. |
| `Labels` | Details of the labels associated with the card. |
| `LabelIds` | Ids of the labels associated with the card. |
| `ChecklistIds` | Ids of the checklists on the card. |
| `MemberIds` | Ids of members assigned to the card. |
| `MembersVotedIds` | Ids of members who voted on the card. |
| `AttachmentCover` | Id of the image attachment used as the card's cover. |
| `Url` | URL to access the card. |
| `ShortUrl` | Shortened URL to access the card. |
| `Cover` | Details of the card's cover. |
| `IsTemplate` | Indicates if the card is a template. |

## Usage
This enum is used in classes such as [CardFields](CardFields) and [GetCardOptions](GetCardOptions) to specify which fields of a card should be included or manipulated.
