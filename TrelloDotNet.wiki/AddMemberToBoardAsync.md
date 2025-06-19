[Back to Member Features](TrelloClient#member-features)

Add a Member to a board (aka give them access)

## Signature
```cs
/// <summary>
/// Add a Member to a board (aka give them access)
/// </summary>
/// <param name="boardId">Id of the Board to give access to</param>
/// <param name="memberId">Id of the Member that need access</param>
/// <param name="membershipType">What type of access the member should be given</param>
/// <param name="allowBillableGuest">Optional param that allows organization admins to add multi-board guests onto a board.</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task AddMemberToBoardAsync(string boardId, string memberId, MembershipType membershipType, bool allowBillableGuest = false, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId1 = "63c939a5cea0cb006dc9e88b";
var boardId2 = "3f5443a5ce54322dedc5e443";
var boardId3 = "dasdasdas333d33dedc8e332";

var memberId = "63d1239e857afaa8b003c633";

//Make the member Admin on Board 1
await _trelloClient.AddMemberToBoardAsync(boardId1, memberId, MembershipType.Admin);

//Make the member Normal user on Board 2
await _trelloClient.AddMemberToBoardAsync(boardId2, memberId, MembershipType.Normal);

//Make the member Observer (Premium feature only) user on Board 3
await _trelloClient.AddMemberToBoardAsync(boardId3, memberId, MembershipType.Observer);
```