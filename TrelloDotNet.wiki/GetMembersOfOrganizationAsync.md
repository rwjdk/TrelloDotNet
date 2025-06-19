[Back to Member Features](TrelloClient#member-features)

Get the Members (users) of an Organization (aka Workspace)

## Signature
```cs
/// <summary>
/// Get the Members (users) of an Organization
/// </summary>
/// <param name="organizationId">Id of the Organization</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Members</returns>
public async Task<List<Member>> GetMembersOfOrganizationAsync(string organizationId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var organizationId = "dasdasdas333d33dedc8e332";

List<Member> membersOfOrganization = await _trelloClient.GetMembersOfOrganizationAsync(organizationId);
```