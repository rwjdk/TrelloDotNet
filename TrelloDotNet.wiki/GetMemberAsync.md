[Back to Member Features](TrelloClient#member-features)

Get a Member with a specific Id

## Signature
```cs
/// <summary>
/// Get a Member with a specific Id
/// </summary>
/// <param name="memberId">Id of the Member</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Member</returns>
public async Task<Member> GetMemberAsync(string memberId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var memberId = "63d1239e857afaa8b003c633";

Member member = await _trelloClient.GetMemberAsync(memberId);
string fullName = member.FullName;
string username = member.Username;
```