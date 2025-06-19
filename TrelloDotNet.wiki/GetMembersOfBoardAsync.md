[Back to Member Features](TrelloClient#member-features)

Get the Members (users) of a board

## Signature
```cs
/// <summary>
/// Get the Members (users) of a board
/// </summary>
/// <param name="boardId">Id of the Board (in its long or short version)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Members</returns>
public async Task<List<Member>> GetMembersOfBoardAsync(string boardId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";

List<Member> membersOfBoard = await _trelloClient.GetMembersOfBoardAsync(boardId);
```