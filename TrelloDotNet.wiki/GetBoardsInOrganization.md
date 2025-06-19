[Back to Board Features](TrelloClient#board-features)

Get the Boards in an Organization

## Signature
```cs
/// <summary>
/// Get the Boards in an Organization
/// </summary>
/// <param name="organizationId">Id of the Organization</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Active Boards in the Organization</returns>
public async Task<List<Board>> GetBoardsInOrganization(string organizationId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
string organizationId = "63c939a5cea0cb006dc9e34d";
List<Board> boardsInOrganization = await _trelloClient.GetBoardsInOrganization(organizationId);
```