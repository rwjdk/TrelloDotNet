[Back to Action Features](TrelloClient#action-features)

Get the most recent Actions (Changelog Events) of a board

> Tip: Use [this video](https://youtu.be/aWYEg1wPVYY) to find the Id of a Board

## Signature
```cs
/// <summary>
/// Get the most recent Actions (Changelog Events) of a board
/// </summary>
/// <param name="boardId">The Id of the Board</param>
/// <param name="options">Options on how and what is retrieved</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of most Recent Trello Actions</returns>
public async Task<List<TrelloAction>> GetActionsOfBoardAsync(string boardId, GetActionsOptions options, CancellationToken cancellationToken = default)
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";

//Get last 50 actions of the board
List<TrelloAction> last50ActionsOfBoard = await _trelloClient.GetActionsOfBoardAsync(boardId, new GetActionsOptions
{
    Limit = 50
});

//Get last 200 actions of the board
List<TrelloAction> last200ActionsOfBoard = await _trelloClient.GetActionsOfBoardAsync(boardId, new GetActionsOptions
{
    Limit = 200
});

//Get last 100 actions of type 'updateCard' and 'addLabelToCard'
List<TrelloAction> last100ActionsOfBoardOfSpecificTypes = await _trelloClient.GetActionsOfBoardAsync(boardId, new GetActionsOptions
{
    Limit = 100,
    Filter = new List<string>
    {
        TrelloDotNet.Model.Webhook.WebhookActionTypes.UpdateCard,
        TrelloDotNet.Model.Webhook.WebhookActionTypes.AddLabelToCard
    }
});
```