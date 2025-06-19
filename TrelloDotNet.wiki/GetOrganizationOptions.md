The `GetOrganizationOptions` provides options for customizing how and what data is included when retrieving organizations (workspaces) in Trello. This allows for optimizing performance by selecting specific fields or including nested data.

| Property          | Description                                                                 |
|-----------------------|-----------------------------------------------------------------------------|
| AdditionalParameters  | Additional parameters not supported out-of-the-box.                        |
| OrganizationFields    | Specifies which organization (workspace) fields to include.                |

For reference, the `OrganizationFields` property allows specifying fields such as `Name`, `DisplayName`, `Description`, `BoardIds`, `Url`, `Website`, and `Memberships`. These fields can be used to tailor the data retrieved for an organization.