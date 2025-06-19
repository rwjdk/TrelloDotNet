[Back to Comments Features](TrelloClient#comments-features)

Get Paged Comments on a Card (Note: this method can max return up to 50 latest comments. For more use the page parameter [note: the API can't give you back how many there are in total so you need to try until non is returned])

If you wish all comments use [GetAllCommentsOnCardAsync](GetAllCommentsOnCardAsync) instead

## Signature
```cs
/// <summary>
/// Get Paged Comments on a Card (Note: this method can max return up to 50 comments. For more use the page parameter [note: the API can't give you back how many there are in total so you need to try until non is returned])
/// </summary>
/// <param name="cardId">Id of Card</param>
/// <param name="page">The page of results for actions. Each page of results has 50 actions. (Default: 0, Maximum: 19)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Comments</returns>
public async Task<List<TrelloAction>> GetPagedCommentsOnCardAsync(string cardId, int page = 0, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

List<TrelloAction> latest50CommentsOnCard = await TrelloClient.GetPagedCommentsOnCardAsync(cardId, page: 0);

List<TrelloAction> next50CommentsOnCard = await TrelloClient.GetPagedCommentsOnCardAsync(cardId, page: 1);

```