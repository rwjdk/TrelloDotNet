[Back to Organization Features](TrelloClient#organization-features)

Update an Organization (Workspace)

## Signature
```cs
/// <summary>
/// Update an Organization (Workspace)
/// </summary>
/// <param name="organizationWithChanges">Organization with changes</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The updated Organization</returns>
public async Task<Organization> UpdateOrganizationAsync(Organization organizationWithChanges, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var organizationId = "dasdasdas333d33dedc8e332";

Organization organization = await _trelloClient.GetOrganizationAsync(organizationId);
organization.DisplayName = "My new workspace name";

Organization updatedOrganization = await _trelloClient.UpdateOrganizationAsync(organization);
```