[Back to Action Features](TrelloClient#action-features)

Get the most recent Actions (Changelog Events) of a Card

> Note: By default if no filter is given the default filter of Trello API is 'commentCard, updateCard:idList' (aka 'Move Card To List' and 'Add Comment')

> Tip: Use [this video](https://youtu.be/aWYEg1wPVYY) to find the Id of a Card

## Signature
```cs
/// <summary>
/// Get the most recent Actions (Changelog Events) on a Card
/// </summary>
/// <param name="cardId">The Id of the Card</param>
/// <param name="options">Options on how and what is retrieved</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of most Recent Trello Actions</returns>
public async Task<List<TrelloAction>> GetActionsOnCardAsync(string cardId, GetActionsOptions options, CancellationToken cancellationToken = default)
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e88c";

//Get last 50 actions on the card (Default filter 'commentCard, updateCard:idList')
List<TrelloAction> last50ActionsOnCard = await _trelloClient.GetActionsOnCardAsync(cardId, new GetActionsOptions
{
    Limit = 50
});

//Get last 200 actions on the card  (Default filter 'commentCard, updateCard:idList')
List<TrelloAction> last200ActionsOnCard = await _trelloClient.GetActionsOnCardAsync(cardId, new GetActionsOptions
{
    Limit = 200
});

//Get last 100 actions of type 'removeLabelFromCard' and 'addLabelToCard'
List<TrelloAction> last100ActionsOnCArdOfSpecificTypes = await _trelloClient.GetActionsOnCardAsync(cardId, new GetActionsOptions
{
    Limit = 100,
    Filter = new List<string>
    {
        TrelloDotNet.Model.Webhook.WebhookActionTypes.RemoveLabelFromCard,
        TrelloDotNet.Model.Webhook.WebhookActionTypes.AddLabelToCard,

    }
});
```