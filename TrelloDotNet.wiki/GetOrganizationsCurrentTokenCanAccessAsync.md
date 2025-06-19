[Back to Organization Features](TrelloClient#organization-features)

## Signature
```cs
/// <summary>
/// Get the Organizations that the token provided to the TrelloClient can Access
/// </summary>
/// <returns>The Organizations there is access to</returns>
public async Task<List<Organization>> GetOrganizationsCurrentTokenCanAccessAsync(CancellationToken cancellationToken = default)
```
### Examples

```cs
List<Organization> organizations = await _trelloClient.GetOrganizationsCurrentTokenCanAccessAsync();
```