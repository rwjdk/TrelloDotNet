[Back to Generic Features](TrelloClient#generic-features)

Custom Delete Method to be used on unexposed features of the API.

>Tip: Use Trello's API reference to see endpoints: https://developer.atlassian.com/cloud/trello/rest

## Signature
```cs
/// <summary>
/// Custom Delete Method to be used on unexposed features of the API.
/// </summary>
/// <param name="urlSuffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
/// <param name="cancellationToken">Cancellation Token</param>
public async Task DeleteAsync(string urlSuffix, CancellationToken cancellationToken = default)
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
var cardId = "63c939a5cea0cb006dc454f33";

//Delete a Card
await TrelloClient.DeleteAsync($"cards/{cardId}");
```