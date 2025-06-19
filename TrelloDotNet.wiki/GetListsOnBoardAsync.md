[Back to List Features](TrelloClient#list-features)

Get Lists (Columns) on a Board

## Signature
```cs
/// <summary>
/// Get Lists (Columns) on a Board
/// </summary>
/// <param name="boardId">Id of the Board (in its long or short version)</param>
/// <param name="options">Options of the lists</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>List of Lists (Columns)</returns>
public async Task<List<List>> GetListsOnBoardAsync(string boardId, GetListOptions options, CancellationToken cancellationToken = default) {...}
```
### Examples
```cs
var boardId = "63c939a5cea0cb006dc9e88b";
            
//Get all open (visible lists on a board)
List<List> listsOnBoard = await _trelloClient.GetListsOnBoardAsync(boardId);

//Get all archived Lists
List<List> listsOnBoard = await client.GetListsOnBoardAsync(boardId, new GetListOptions
{
    Filter = ListFilter.Closed,
    IncludeBoard = true,
    IncludeCards = GetListOptionsIncludeCards.OpenCards
});

//Get all open Lists and include their board and Cards
List<List> listsOnBoard = await client.GetListsOnBoardAsync(boardId, new GetListOptions
{
    IncludeBoard = true,
    IncludeCards = GetListOptionsIncludeCards.OpenCards
});
```