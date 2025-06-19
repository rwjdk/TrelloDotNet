[Back to Comments Features](TrelloClient#comments-features)

Update a comment Action (aka only way to update comments as they are not seen as their own objects)

## Signature
```cs
/// <summary>
/// Update a comment Action (aka only way to update comments as they are not seen as their own objects)
/// </summary>
/// <param name="commentAction">The comment Action with the updated text</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task<TrelloAction> UpdateCommentActionAsync(TrelloAction commentAction, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

//Lets add (or alternative get an existing comment)
TrelloAction trelloActionComment = await _trelloClient.AddCommentAsync(cardId, new Comment("Hello World"));

//Lets change the text of the comment
trelloActionComment.Data.Text = "Changed Comment";

//Lets update the comment
var updatedCommentAction = await _trelloClient.UpdateCommentActionAsync(trelloActionComment);
```