[Back to Organization Features](TrelloClient#organization-features)

Get the Organizations that the specified member has access to

## Signature
```cs
/// <summary>
/// Get the Organizations that the specified member has access to
/// </summary>
/// <param name="memberId">Id of the Member to find organizations for</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Organizations there is access to</returns>
public async Task<List<Organization>> GetOrganizationsForMemberAsync(string memberId, CancellationToken cancellationToken = default)
```
### Examples

```cs
string memberId = "34343l4343jjk3343jk43kj";

List<Organization> organizations = await _trelloClient.GetOrganizationsForMemberAsync(memberId);
```