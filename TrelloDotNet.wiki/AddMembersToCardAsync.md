[Back to Member Features](TrelloClient#member-features)

Add one or more Members to a Card

## Signature
```cs
/// <summary>
/// Add one or more Members to a Card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="memberIdsToAdd">One or more Ids of Members to add</param>
public async Task<Card> AddMembersToCardAsync(string cardId, CancellationToken cancellationToken = default, params string[] memberIdsToAdd) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";
var memberId1 = "63d1239e857afaa8b003c633";
var memberId2 = "543d123923f544faa8b003c644";

Card cardWithAddedMembers = await _trelloClient.AddMembersToCardAsync(cardId, memberId1, memberId2);
```