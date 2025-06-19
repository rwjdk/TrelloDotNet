[Back to Label Features](TrelloClient#label-features)

Add a Label to a Card

## Signature
```cs
/// <summary>
/// Add a Label to a Card
/// </summary>
/// <param name="cardId">Id of the Card</param>
/// <param name="cancellation">Cancellation Token</param>
/// <param name="labelIdsToAdd">One or more Ids of Labels to add</param>
public async Task<Card> AddLabelsToCardAsync(string cardId, CancellationToken cancellation = default, params string[] labelIdsToAdd) {...}
```
### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e88b";
var cardId = "63c939a5cea0cb006dc9e9dd";

List<Label> allLabelsOnBoard = await _trelloClient.GetLabelsOfBoardAsync(boardId);

//Find the 'bug' and 'blocked' Labels (or know their Id up front)
Label labelBug = allLabelsOnBoard.First(x => x.Name == "Bug");
Label labelBlocked = allLabelsOnBoard.First(x => x.Name == "Blocked");

//Add a single label
Card cardWithOneLabelAdded = await _trelloClient.AddLabelsToCardAsync(cardId, labelBug.Id);

//Add two labels
Card cardWithTwoLabelsAdded = await _trelloClient.AddLabelsToCardAsync(cardId, labelBug.Id, labelBlocked.Id);

//Add all labels
Card cardWithAllLabelsAdded = await _trelloClient.AddLabelsToCardAsync(cardId, allLabelsOnBoard.Select(x=> x.Id).ToArray());
```