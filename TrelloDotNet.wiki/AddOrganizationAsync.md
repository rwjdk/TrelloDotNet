[Back to Organization Features](TrelloClient#organization-features)

Create a new Organization (Workspace)

## Signature
```cs
/// <summary>
/// Create a new Organization (Workspace)
/// </summary>
/// <param name="newOrganization">the new Organization</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The New Organization</returns>
public async Task<Organization> AddOrganizationAsync(Organization newOrganization, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var newOrganization = new Organization(displayName: "My Cool Workspace");
newOrganization.Description = "This is my workspace where I do cool things"; //Optional
newOrganization.Name = "myuniquelowercaseonlyname"; //Optional - If not provided Trello will auto-generate one based on displayName and a random number
newOrganization.Website = "https://www.mywebsite.com"; //Optional
Organization addedOrganization = await _trelloClient.AddOrganizationAsync(newOrganization);
```