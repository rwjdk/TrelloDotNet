[Back to Action Features](TrelloClient#action-features)

Get the most recent Actions (Changelog Events) for a Organization (Workspace)

> Note: Only organization-specific actions will be returned. For the actions on the boards, see [GetActionsOfBoardAsync](GetActionsOfBoardAsync)

## Signature
```cs
/// <summary>
/// Get the most recent Actions (Changelog Events) for an Organization
/// </summary>
/// <remarks>
/// Only organization-specific actions will be returned. For the actions on the boards, see GetActionsOfBoardAsync
/// </remarks>
/// <param name="organizationId">The Id of the Organization</param>
/// <param name="options">Options on how and what is retrieved</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of most Recent Trello Actions</returns>
public async Task<List<TrelloAction>> GetActionsForOrganizationsAsync(string organizationId, GetActionsOptions options, CancellationToken cancellationToken = default)
```
### Examples

```cs
var organizationId = "63c939a5cea0cb006dc9e88c";

//Get the last 50 actions
List<TrelloAction> last50Actions = await _trelloClient.GetActionsForOrganizationsAsync(organizationId, new GetActionsOptions {
    Limit = 50
});

//Get the last 200 actions
List<TrelloAction> last200Actions = await _trelloClient.GetActionsForOrganizationsAsync(organizationId, new GetActionsOptions
{
    Limit = 200
});

//Get the last 100 actions of type 'updateOrganization'
var filter = new List<string>
{
    TrelloDotNet.Model.Webhook.WebhookActionTypes.UpdateOrganization,
};
List<TrelloAction> last100ActionsOfSpecificTypes = await _trelloClient.GetActionsForOrganizationsAsync(organizationId, new GetActionsOptions
{
    Limit = 100,
    Filter = new List<string>
    {
        TrelloDotNet.Model.Webhook.WebhookActionTypes.UpdateOrganization,
    }
});
```