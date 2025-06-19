Defines who can vote on a Trello board when the 'Votes' Power-Up is enabled.

## Values
| Value | Description |
| --- | --- |
| `Unknown` | Represents an unknown value retrieved from the Trello REST API. |
| `Disabled` | Voting is disabled on the board. |
| `Members` | Only admins and board members can vote. |
| `Observers` | Admins, board members, and observers can vote. |
| `Workspace` | Admins, board members, observers, and workspace members can vote (requires the board to be public or a workspace board). |
| `Public` | Everyone can vote (requires the board to be public). |

## Usage
Used in [BoardPreferences](BoardPreferences) and [UpdateBoardPreferencesOptions](UpdateBoardPreferencesOptions).
