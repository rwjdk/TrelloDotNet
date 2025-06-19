[Back to Label Features](TrelloClient#label-features)

Remove one or more Labels from a Card

## Signature
```cs
/// <summary>
/// Remove one or more Labels from a Card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <param name="labelIdsToRemove">One or more Ids of Labels to remove</param>
public async Task<Card> RemoveLabelsFromCardAsync(string cardId, CancellationToken cancellationToken, params string[] labelIdsToRemove) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
var cardId = "63c939a5cea0cb006dc9e9dd";

List<Label> allLabelsOnBoard = await _trelloClient.GetLabelsOfBoardAsync(boardId);

//Find the 'bug' and 'blocked' Labels (or know their Id up front)
Label labelBug = allLabelsOnBoard.First(x => x.Name == "Bug");
Label labelBlocked = allLabelsOnBoard.First(x => x.Name == "Blocked");

//Remove a single label (if the label is not on the card call is ignored)
Card cardWithOneLabelRemoved = await _trelloClient.RemoveLabelsFromCardAsync(cardId, labelBug.Id);

//Remove two (or more) labels (if any of the labels are not on the card it will remove the labels that are there and ignore the rest)
Card cardWithTwoLabelsRemoved = await _trelloClient.RemoveLabelsFromCardAsync(cardId, labelBug.Id, labelBlocked.Id);

//Remove all labels
Card cardWithAllLabelsRemoved = await _trelloClient.RemoveAllLabelsFromCardAsync(cardId);
```