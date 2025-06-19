[Back to Member Features](TrelloClient#member-features)

Get information about the Member that owns the `Token` used by this [`TrelloClient`](TrelloClient)

## Signature
```cs
/// <summary>
/// Get information about the Member that owns the token used by this TrelloClient
/// </summary>
/// <returns>The Member</returns>
public async Task<Member> GetTokenMemberAsync(CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
Member memberForToken = await _trelloClient.GetTokenMemberAsync();
```