[Back to Card Features](TrelloClient#card-features)

This method retrieves all cards present in the user's Trello Inbox. The Inbox is a special collection of cards associated with the user that is not associated with a specific board. The method allows customization of the data retrieved using the [GetInboxCardOptions](GetInboxCardOptions) parameter, enabling the inclusion of specific fields, attachments, checklists, and other nested data. This customization helps optimize performance and tailor the response to the user's needs.

## Method Signature
```csharp
async Task<List<Card>> GetCardsInInboxAsync(GetInboxCardOptions options)
```

### Parameters
- `options` An instance of [GetInboxCardOptions](GetInboxCardOptions) that specifies the fields, attachments, checklists, and other data to include in the response. It also allows filtering and ordering of the cards.
### Return value
`List<Card>` A collection of `Card` objects representing the cards in the user's Inbox. Each card contains details as specified by the [GetInboxCardOptions](GetInboxCardOptions) parameter.
## Examples
```csharp
// Example 1: Retrieve all cards from the user's Inbox with default settings.
GetInboxCardOptions options = new GetInboxCardOptions();
List<Card> inboxCards = await trelloClient.GetCardsInInboxAsync(options);
```
```csharp
// Example 2: Retrieve cards from the Inbox including their attachments and checklists.
GetInboxCardOptions options = new GetInboxCardOptions
{
    IncludeAttachments = GetCardOptionsIncludeAttachments.True,
    IncludeChecklists = true
};
List<Card> detailedInboxCards = await trelloClient.GetCardsInInboxAsync(options);
```
```csharp
// Example 3: Retrieve cards from the Inbox filtered to only include active cards.
GetInboxCardOptions options = new GetInboxCardOptions
{
    Filter = CardsFilter.Open
};
List<Card> activeInboxCards = await trelloClient.GetCardsInInboxAsync(options);
```
