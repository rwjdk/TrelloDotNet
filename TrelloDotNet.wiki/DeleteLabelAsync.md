[Back to Label Features](TrelloClient#label-features)

Delete a Label from the board and remove it from all cards it was added to (WARNING: THERE IS NO WAY GOING BACK!!!). 

>If you are looking to remove a label from a Card then see [`RemoveLabelsFromCardAsync`](RemoveLabelsFromCardAsync) and [`RemoveAllLabelsFromCardAsync`](RemoveAllLabelsFromCardAsync)

## Signature
```cs
/// <summary>
/// Delete a Label from the board and remove it from all cards it was added to (WARNING: THERE IS NO WAY GOING BACK!!!). If you are looking to remove a label from a Card then see 'RemoveLabelsFromCardAsync' and 'RemoveAllLabelsFromCardAsync'
/// </summary>
/// <param name="labelId">The id of the Label to Delete</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteLabelAsync(string labelId, CancellationToken cancellationToken = default) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
            
List<Label> labelsOnBoard = await _trelloClient.GetLabelsOfBoardAsync(boardId);

//Get the 'bug' label and delete it
Label labelBug = labelsOnBoard.First(x => x.Name == "Bug");
await _trelloClient.DeleteLabelAsync(labelBug.Id);
```