This method retrieves an overview of the memberships associated with the current token user. It provides details about the user's roles and permissions within various workspaces and boards. The method assumes that if the user is an admin in a workspace, they are also an admin in the boards under that workspace. This functionality is useful for understanding the scope of access and administrative rights of the token user across Trello's organizational structure.

## Method Signature
```csharp
async Task<TokenMembershipOverview> GetCurrentTokenMembershipsAsync(GetBoardOptions boardOptions, GetOrganizationOptions organizationOptions)
```

### Parameters
- `boardOptions` Specifies options for retrieving boards, such as filters or additional data to include.
- `organizationOptions` Specifies options for retrieving organizations for what additional data to include.
### Return value
`TokenMembershipOverview` Provides a detailed overview of the user's memberships, including their roles in workspaces and boards. This can be used to analyze or display the user's access levels.
## Examples
```csharp
// Example 1: Retrieve the current token user's membership overview.
GetBoardOptions boardOptions = new GetBoardOptions
{
    Filter = GetBoardOptionsFilter.Open
};

GetOrganizationOptions organizationOptions = new GetOrganizationOptions
{
    OrganizationFields = new OrganizationFields(OrganizationFieldsType.DisplayName)
};

TokenMembershipOverview memberships = await trelloClient.GetCurrentTokenMembershipsAsync(boardOptions, organizationOptions);
foreach (var workspaceMembership in memberships.OrganizationMemberships)
{
    Console.WriteLine($"Workspace: {workspaceMembership.Key.Name}, Role: {workspaceMembership.Value}");
}

foreach (var boardMembership in memberships.BoardMemberships)
{
    Console.WriteLine($"Board: {boardMembership.Key.Name}, Role: {boardMembership.Value}");
}
```