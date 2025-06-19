Defines the types of fields associated with a Trello member.

## Values
| Value | Description |
| --- | --- |
| `FullName` | The full name related to the account, if it is public. |
| `Username` | The username of the member. |
| `Initials` | The initials related to the account, if it is public. |
| `AvatarUrl` | The URL of the member's avatar. |
| `Confirmed` | Indicates whether the user has confirmed their email address for their account. |
| `Email` | The email address of the member. |
| `MemberType` | The type of membership (admin, normal, observer). |
| `LastActivityDate` | The last time the member performed an activity in Trello. |
| `LastLoginDate` | The last time the member logged into Trello. |

## Usage
This enum is utilized in classes such as [MemberFields](MemberFields) and [GetMemberOptions](GetMemberOptions) to specify the fields to include when retrieving member data.
