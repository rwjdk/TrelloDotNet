[Back to Generic Features](TrelloClient#generic-features)

Custom Get Method to be used on unexposed features of the API

>Tip: Use Trello's API reference to see endpoints and query-parameters: https://developer.atlassian.com/cloud/trello/rest

## Signature (raw)
```cs
/// <summary>
/// Custom Get Method to be used on unexposed features of the API delivered back as JSON.
/// </summary>
/// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <param name="parameters">Additional Parameters</param>
/// <returns>JSON Representation of response</returns>
public async Task<string> GetAsync(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters) {...}
```

## Signature (generic)
```cs
/// <summary>
/// Custom Get Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on your class to match JSON Properties
/// </summary>
/// <typeparam name="T">Object to Return</typeparam>
/// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <param name="parameters">Additional Parameters</param>
/// <returns>The Object specified to be returned</returns>
public async Task<T> GetAsync<T>(string urlSuffix, CancellationToken cancellationToken = default, params QueryParameter[] parameters) {...}
```

### Examples

```cs
var boardId = "63c939a5cea0cb006dc9e321";

//Add a Card and get it back as JSON
string rawGetBoardJson = await TrelloClient.GetAsync($"boards/{boardId}");

//Get a Board Object
Board rawGetBoard = await TrelloClient.GetAsync<Board>($"boards/{boardId}");
```