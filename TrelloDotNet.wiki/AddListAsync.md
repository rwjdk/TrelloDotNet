[Back to List Features](TrelloClient#list-features)

Add a List to a Board

## Signature
```cs
/// <summary>
/// Add a List to a Board
/// </summary>
/// <remarks>
/// The Provided BoardId, the list should be added to need to be the long version of the BoardId as API does not support the short version
/// </remarks>
/// <param name="list">List to add</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Create list</returns>
public async Task<List> AddListAsync(List list, CancellationToken cancellationToken = default) {...}
```
### Examples

- [E-Learning video](https://youtu.be/VLGL77sUtIo)

```cs
string boardId = "343mfk4k4f49kf4k49k4";
List addedList = await TrelloClient.AddListAsync(new List("My Cool List", boardId));
```