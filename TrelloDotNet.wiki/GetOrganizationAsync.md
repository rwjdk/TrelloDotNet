[Back to Organization Features](TrelloClient#organization-features)

Get an Organization (also known as Workspace)

## Signature
```cs
/// <summary>
/// Get an Organization (also known as Workspace)
/// </summary>
/// <param name="organizationId">ID of an Organization</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Organization</returns>
public async Task<Organization> GetOrganizationAsync(string organizationId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var organizationId = "dasdasdas333d33dedc8e332";

Organization organization = await _trelloClient.GetOrganizationAsync(organizationId);
```