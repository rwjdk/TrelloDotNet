Defines options for handling labels when moving a card to another board.

## Values
| Value | Description |
| --- | --- |
| `MigrateToLabelsOfSameNameAndColorAndCreateMissing` | Migrate labels with the same color and name, creating missing labels on the new board. |
| `MigrateToLabelsOfSameNameAndColorAndRemoveMissing` | Migrate labels with the same color and name, removing labels that do not exist on the new board. |
| `MigrateToLabelsOfSameNameAndCreateMissing` | Migrate labels with the same name (allowing color changes), creating missing labels on the new board. |
| `MigrateToLabelsOfSameNameAndRemoveMissing` | Migrate labels with the same name (allowing color changes), removing labels that do not exist on the new board. |
| `RemoveAllLabelsOnCard` | Remove all labels from the card before moving it to the new board. |

## Usage
Used in [MoveCardToBoardOptions](MoveCardToBoardOptions) and related methods such as [MoveCardToBoardAsync](MoveCardToBoardAsync).
