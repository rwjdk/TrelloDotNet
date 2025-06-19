[Back to Comments Features](TrelloClient#comments-features)

Get All Comments on a Card (if you only wish the latest comments use [GetPagedCommentsOnCardAsync](GetPagedCommentsOnCardAsync) instead)

## Signature
```cs
/// <summary>
/// Get All Comments on a Card
/// </summary>
/// <param name="cardId">Id of Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Comments</returns>
public async Task<List<TrelloAction>> GetAllCommentsOnCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9db";
List<TrelloAction> trelloActions = await _trelloClient.GetAllCommentsOnCardAsync(cardId);
foreach (TrelloAction trelloAction in trelloActions)
{
    string commentText = trelloAction.Data.Text;
    Member memberThatCreatedTheComment = trelloAction.MemberCreator;
    DateTimeOffset dateAndTimeOfTheComment = trelloAction.Date;
}
```