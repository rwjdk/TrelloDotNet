[Back to MemberShip Features](TrelloClient#membership-features)

Change the membership-type of a member Member on a board (Example make them Admin)

## Signature
```cs
/// <summary>
/// Change the membership-type of a member Member on a board (Example make them Admin)
/// </summary>
/// <param name="boardId">Id of the Board</param>
/// <param name="membershipId">Id of the Member's Membership (NB: This is NOT the memberId..., you get this via method 'GetMembershipsOfBoardAsync')</param>
/// <param name="membershipType">What type of access the member should be given instead</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task UpdateMembershipTypeOfMemberOnBoardAsync(string boardId, string membershipId, MembershipType membershipType, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";

//Get all memberships
List<Membership> memberships = await _trelloClient.GetMembershipsOfBoardAsync(boardId);
foreach (Membership membership in memberships)
{
    //Make all users Admin (properly not something you should do in a real scenario :-) )
    await _trelloClient.UpdateMembershipTypeOfMemberOnBoardAsync(boardId, membership.Id, MembershipType.Admin);
}
```