Defines who can write comments on cards within a Trello board.

## Values
| Value | Description |
| --- | --- |
| `Unknown` | Represents an unknown value retrieved from the Trello REST API. |
| `Disabled` | Commenting is disabled on the board. |
| `Members` | Only admins and board members can comment. |
| `Observers` | Admins, board members, and observers can comment. |
| `Workspace` | Admins, board members, observers, and workspace members can comment (requires the board to be public or workspace board). |
| `Public` | Everyone can comment (requires the board to be public). |

## Usage
Used in [BoardPreferences](BoardPreferences) and [UpdateBoardPreferencesOptions](UpdateBoardPreferencesOptions).
