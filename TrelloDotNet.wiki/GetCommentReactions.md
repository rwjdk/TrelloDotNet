[Back to Comments Features](TrelloClient#comments-features)

The reactions to a comment

## Signature
```cs
/// <summary>
/// The reactions to a comment
/// </summary>
/// <param name="commentActionId">Id of the Comment (ActionId)</param>
/// <param name="cancellationToken">CancellationToken</param>
/// <returns>The Reactions</returns>
public async Task<List<CommentReaction>> GetCommentReactions(string commentActionId, CancellationToken cancellationToken = default)
```
### Examples

```cs
var cardId = "64e9df94130a9a30ed8d625e";
var comments = await _trelloClient.GetAllCommentsOnCardAsync(cardId);
foreach (TrelloAction comment in comments)
{
    //Get Comment Reactions
    List<CommentReaction> reactions = await TrelloClient.GetCommentReactions(comment.Id);
}
```