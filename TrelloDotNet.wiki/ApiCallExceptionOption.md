Defines the level of detail included in exceptions related to API calls.

## Values
| Value | Description |
| --- | --- |
| `IncludeUrlAndCredentials` | Exceptions include the full URL and credentials. |
| `IncludeUrlButMaskCredentials` | Exceptions include the full URL but mask credentials. |
| `DoNotIncludeTheUrl` | Exceptions do not include the URL. |

## Usage
Used in [TrelloClientOptions](TrelloClientOptions) to configure exception handling behavior.
