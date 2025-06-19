[Back to Card Features](TrelloClient#card-features)

This method retrieves all cards associated with a specified Trello board. By default, it fetches cards from unarchived lists, but it can be configured to include archived cards or filter cards based on specific criteria using the [`GetCardOptions`](GetCardOptions) parameter. The method allows customization of the returned card data, such as including only specific fields or nested data, to optimize performance and reduce the need for additional API calls.

## Method Signature
```csharp
async Task<List<Card>> GetCardsOnBoardAsync(string boardId, GetCardOptions options)
```

### Parameters
- `boardId` The unique identifier of the board from which cards are to be retrieved. It can be provided in its long or short format.
- `options` An instance of [`GetCardOptions`](GetCardOptions) that allows customization of the data returned for each card. This includes filtering cards (e.g., archived or active), specifying which fields to include, and whether to include nested data such as lists or members.
### Return value
`List<Card>` A collection of `Card` objects representing the cards on the specified board. Each card contains details as specified by the [`GetCardOptions`](GetCardOptions) parameter.
## Examples
```csharp
// Example 1: Retrieve all cards from a board with default settings.
string boardId = "<your_board_id>";
List<Card> cards = await trelloClient.GetCardsOnBoardAsync(boardId);
```
```csharp
// Example 2: Retrieve cards with specific fields and include their associated lists.
string boardId = "<your_board_id>";
GetCardOptions options = new GetCardOptions
{
    CardFields = new CardFields(CardFieldsType.Name, CardFieldsType.Start),
    IncludeList = true
};
List<Card> cards = await trelloClient.GetCardsOnBoardAsync(boardId, options);
```
```csharp
// Example 3: Retrieve only archived cards from a board.
string boardId = "<your_board_id>";
GetCardOptions options = new GetCardOptions
{
    Filter = CardsFilter.Closed
};
List<Card> archivedCards = await trelloClient.GetCardsOnBoardAsync(boardId, options);
```
