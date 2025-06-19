[Back to Card Features](TrelloClient#card-features)

Get all Cards a Member is on (across multiple boards)

## Signature
```cs
/// <summary>
/// Get all Cards a Member is on (across multiple boards)
/// </summary>
/// <param name="memberId">Id of Member</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns></returns>
public async Task<List<Card>> GetCardsForMemberAsync(string memberId, CancellationToken cancellationToken = default) {...}
```
### Examples

- [E-Learning video](https://youtu.be/ucFGNB1R9Ic)

```cs
var memberId = "63c939a5cesdfseeec9e89d";
var cardsMemberIsAssignedTo = await _trelloClient.GetCardsForMemberAsync(memberId);
```