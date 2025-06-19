[Back to Label Features](TrelloClient#label-features)

Add a new label to the Board 

>Not to be confused with [`AddLabelsToCardAsync`](AddLabelsToCardAsync) that assign labels to cards

## Signature
```cs
/// <summary>
/// Add a new label to the Board (Not to be confused with 'AddLabelsToCardAsync' that assign labels to cards)
/// </summary>
/// <param name="label">Definition of the new label</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The newly created label</returns>
public async Task<Label> AddLabelAsync(Label label, CancellationToken cancellationToken = default) {...}
```
### Examples
```cs
var boardId = "63c939a5cea0cb006dc9e88b";

var bugLabel = await _trelloClient.AddLabelAsync(new Label(boardId, "Bug", "red"));
```