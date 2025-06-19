[Back to Member Features](TrelloClient#member-features)

Remove one or more Members from a Card

## Signature
```cs
/// <summary>
/// Remove one or more Members from a Card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellation">Cancellation Token</param>
/// <param name="memberIdsToRemove">One or more Ids of Members to remove</param>
public async Task<Card> RemoveMembersFromCardAsync(string cardId, CancellationToken cancellation = default, params string[] memberIdsToRemove) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";
var memberId1 = "dasdasdas333d33dedc8e332";
var memberId2 = "63d1239e857afaa8b003c633";

Card cardWithRemovedMembers = await _trelloClient.RemoveMembersFromCardAsync(cardId, memberId1, memberId2);
```