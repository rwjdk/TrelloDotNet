[Back to Comments Features](TrelloClient#comments-features)

Delete a Comment (WARNING: THERE IS NO WAY GOING BACK!!!).

## Signature
```cs
/// <summary>
/// Delete a Comment (WARNING: THERE IS NO WAY GOING BACK!!!).
/// </summary>
/// <param name="commentActionId">Id of Comment Action Id</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteCommentActionAsync(string commentActionId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

//Get all comments on a card
var allCommentsOnCard = await _trelloClient.GetActionsOnCardAsync(cardId, new List<string> { TrelloDotNet.Model.Webhook.WebhookActionTypes.CommentCard },1000);

//Delete all comments on the card
foreach (var trelloActionComment in allCommentsOnCard)
{
    await _trelloClient.DeleteCommentActionAsync(trelloActionComment.Id);
}
```