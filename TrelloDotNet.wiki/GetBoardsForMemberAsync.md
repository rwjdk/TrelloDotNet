[Back to Board Features](TrelloClient#board-features)

Get the Boards that the specified member has access to

## Signature
```cs
/// <summary>
/// Get the Boards that the specified member has access to
/// </summary>
/// <param name="memberId">Id of the Member to find boards for</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Active Boards there is access to</returns>
public async Task<List<Board>> GetBoardsForMemberAsync(string memberId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string memberId = "63c939a5cea0cb006dc9e89d";
List<Board> boardsForMember = await _trelloClient.GetBoardsForMemberAsync(memberId);
```