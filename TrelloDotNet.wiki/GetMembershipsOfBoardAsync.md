[Back to MemberShip Features](TrelloClient#membership-features)

The Membership Information for a board (aka if Users are Admin, Normal, or Observer)

## Signature
```cs
/// <summary>
/// The Membership Information for a board (aka if Users are Admin, Normal, or Observer)
/// </summary>
/// <param name="boardId">Id of the Board that you wish information on</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The List of Memberships</returns>
public async Task<List<Membership>> GetMembershipsOfBoardAsync(string boardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
List<Membership> memberships = await _trelloClient.GetMembershipsOfBoardAsync(boardId);
foreach (Membership membership in memberships)
{
    var memberId = membership.MemberId;
    MembershipType memberType = membership.MemberType; //Admin, Normal or Observer
}
```