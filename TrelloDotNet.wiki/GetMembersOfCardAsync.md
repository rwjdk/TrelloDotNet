[Back to Member Features](TrelloClient#member-features)

Get the Members (users) of a Card

## Signature
```cs
/// <summary>
/// Get the Members (users) of a Card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Members</returns>
public async Task<List<Member>> GetMembersOfCardAsync(string cardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var cardId = "63c939a5cea0cb006dc9e9dd";

List<Member> membersOfCard = await _trelloClient.GetMembersOfCardAsync(cardId);
```