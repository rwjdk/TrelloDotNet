[Back to Search Features](TrelloClient#search-features)

Search Trello for Cards, Boards, and/or Organizations

> Search-Tips: https://support.atlassian.com/trello/docs/searching-for-cards-all-boards/ and https://blog.trello.com/the-secrets-of-superior-trello-searches

## Signature
```cs
/// <summary>
/// Search Trello for Cards, Boards, and/or Organizations
/// </summary>
/// <remarks>
/// Search-tips: https://blog.trello.com/the-secrets-of-superior-trello-searches
/// </remarks>
/// <param name="searchRequest">The Search Request</param>
/// <param name="cancellationToken">CancellationToken</param>
public async Task<SearchResult> SearchAsync(SearchRequest searchRequest, CancellationToken cancellationToken = default)
```
### Examples

```cs
var _trelloClient = TrelloClient;

//Search Cards only for the word 'issue'
var searchRequest = new SearchRequest("issue", partialSearch: true)
{
    SearchCards = true,
    SearchBoards = false,
    SearchOrganizations = false,
    CardLimit = 1000,
    CardFields = new SearchRequestCardFields("id", "name")
};

SearchResult result = await _trelloClient.SearchAsync(searchRequest);
List<Card> resultCards = result.Cards;
```