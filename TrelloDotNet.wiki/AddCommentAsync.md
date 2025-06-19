[Back to Comments Features](TrelloClient#comments-features)

Add a new Comment on a Card

## Signature
```cs
/// <summary>
/// Add a new Comment on a Card
/// </summary>
/// <paramref name="cardId">Id of the Card</paramref>
/// <paramref name="comment">The Comment</paramref>
/// <returns>The Comment Action</returns>
public async Task<TrelloAction> AddCommentAsync(string cardId, Comment comment, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

TrelloAction trelloActionComment = await _trelloClient.AddCommentAsync(cardId, new Comment("Hello World"));
```