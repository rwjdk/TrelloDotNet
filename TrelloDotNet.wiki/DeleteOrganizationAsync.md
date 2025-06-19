[Back to Organization Features](TrelloClient#organization-features)

Delete an entire Organization including all Boards it contains (WARNING: THERE IS NO WAY GOING BACK!!!).

## Signature
```cs
/// <summary>
/// Delete an entire Organization including all Boards it contains (WARNING: THERE IS NO WAY GOING BACK!!!).
/// </summary>
/// <remarks>
/// As this is a major thing, there is a secondary confirmation needed by setting: Options.AllowDeleteOfOrganizations = true
/// </remarks>
/// <param name="organizationId">The id of the Organization to Delete</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteOrganizationAsync(string organizationId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var organizationId = "dasdasdas333d33dedc8e332";

_trelloClient.Options.AllowDeleteOfOrganizations = true;

await _trelloClient.DeleteOrganizationAsync(organizationId);

_trelloClient.Options.AllowDeleteOfOrganizations = false;
```