[Back to Action Features](TrelloClient#action-features)

Get the most recent Actions (Changelog Events) for a Member (user)

## Signature
```cs
/// <summary>
/// Get the most recent Actions (Changelog Events) for a Member
/// </summary>
/// <param name="memberId">The Id of the Member</param>
/// <param name="options">Options on how and what is retrieved</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of most Recent Trello Actions</returns>
public async Task<List<TrelloAction>> GetActionsForMemberAsync(string memberId, GetActionsOptions options, CancellationToken cancellationToken = default)
```
### Examples

```cs
var memberId = "63c939a5cea0cb006dc9e88c";

//Get the last 50 actions
List<TrelloAction> last50Actions = await _trelloClient.GetActionsForMemberAsync(memberId, new GetActionsOptions
{
    Limit = 50
});

//Get the last 200 actions
List<TrelloAction> last200Actions = await _trelloClient.GetActionsForMemberAsync(memberId, new GetActionsOptions
{
    Limit = 200
});

//Get the last 100 actions of type 'addMemberToBoard'
List<TrelloAction> last100ActionsOfSpecificTypes = await _trelloClient.GetActionsForMemberAsync(memberId, new GetActionsOptions
{
    Limit = 100,
    Filter = new List<string>
    {
        TrelloDotNet.Model.Webhook.WebhookActionTypes.AddMemberToBoard,
    }
});
```