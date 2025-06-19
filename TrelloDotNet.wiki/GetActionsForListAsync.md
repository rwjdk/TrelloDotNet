[Back to Action Features](TrelloClient#action-features)

Get the most recent Actions (Changelog Events) of a List

> Tip: Use [this video](https://youtu.be/aWYEg1wPVYY) to find the Id of a List

## Signature
```cs
/// <summary>
/// Get the most recent Actions (Changelog Events) for a List
/// </summary>
/// <param name="listId">The Id of the List</param>
/// <param name="options">Options on how and what is retrieved</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of most Recent Trello Actions</returns>
public async Task<List<TrelloAction>> GetActionsForListAsync(string listId, GetActionsOptions options, CancellationToken cancellationToken = default)
```
### Examples

```cs
var listId = "63c939a5cea0cb006dc9e88c";

//Get the last 50 actions on a list
List<TrelloAction> last50Actions = await _trelloClient.GetActionsForListAsync(listId, new GetActionsOptions
{
    Limit = 50
});

//Get the last 200 actions on a list
List<TrelloAction> last200Actions = await _trelloClient.GetActionsForListAsync(listId, new GetActionsOptions
{
    Limit = 200
});

//Get the last 100 actions of type 'updateCard:idList' (Move card to List)
List<TrelloAction> last100ActionsOfSpecificTypes = await _trelloClient.GetActionsForListAsync(listId, new GetActionsOptions
{
    Limit = 100,
    Filter = new List<string>
    {
        TrelloDotNet.Model.Webhook.WebhookActionTypes.MoveCardToList,

    }
});
```