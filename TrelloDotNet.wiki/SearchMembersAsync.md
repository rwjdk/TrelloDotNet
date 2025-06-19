[Back to Search Features](TrelloClient#search-features)


## Signature
```cs
/// <summary>
/// Search Members across Trello
/// </summary>
/// <param name="searchRequest">The Search-request</param>
/// <param name="cancellationToken">CancellationToken</param>
public async Task<List<Member>> SearchMembersAsync(SearchMemberRequest searchRequest, CancellationToken cancellationToken = default)
```
### Examples

```cs
var searchMemberRequest = new SearchMemberRequest("john")
{
    OnlyOrgMembersFilter = true
};
List<Member> membersForSearch = await _trelloClient.SearchMembersAsync(searchMemberRequest);
```