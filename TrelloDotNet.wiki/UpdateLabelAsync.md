[Back to Label Features](TrelloClient#label-features)

Update the definition of a label (Name and Color)

## Signature
```cs
/// <summary>
/// Update the definition of a label (Name and Color)
/// </summary>
/// <param name="labelWithUpdates">The label with updates</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task<Label> UpdateLabelAsync(Label labelWithUpdates, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
            
List<Label> labelsOnBoard = await _trelloClient.GetLabelsOfBoardAsync(boardId);

//Get the label up update and set its properties
Label labelBug = labelsOnBoard.First(x => x.Name == "Bug");
labelBug.Color = "red";
var updatedLabel = await _trelloClient.UpdateLabelAsync(labelBug);
```