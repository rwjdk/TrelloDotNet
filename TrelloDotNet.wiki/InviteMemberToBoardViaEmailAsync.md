[Back to Member Features](TrelloClient#member-features)

Invite a Member to a board via email (aka give them access)

## Signature
```cs
/// <summary>
/// Invite a Member to a board via email (aka give them access)
/// </summary>
/// <param name="boardId">Id of the Board to give access to</param>
/// <param name="email">Email to invite</param>
/// <param name="membershipType">What type of access the member should be given</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task InviteMemberToBoardViaEmailAsync(string boardId, string email, MembershipType membershipType, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";

//Invite 'user@domain.com' to be admin on the board (if email is not on an existing Trello user a 'Join Trello' invite is sent)
await _trelloClient.InviteMemberToBoardViaEmailAsync(boardId, "user@domain.com", MembershipType.Admin);
```