[Back to PluginData features](TrelloClient#plugindata-features)

This method retrieves the plugin data associated with a specific card in Trello. Plugin data represents information stored by Power-Ups linked to the card. The method returns a list of plugin data objects, each containing details such as the plugin ID, scope, visibility, and raw value (typically in JSON format). This data can be used to understand or interact with the custom functionalities provided by the Power-Ups. Note that the API does not support adding, updating, or removing plugin data; it is strictly for retrieval purposes.

## Method Signature
```csharp
async Task<List<PluginData>> GetPluginDataOnCardAsync(string cardId)
```

### Parameters
- `cardId` The unique identifier of the card from which plugin data is to be retrieved.
### Return value
`List<PluginData>` A collection of plugin data objects, each representing data stored by a plugin on the specified card. This data includes plugin-specific information such as scope, visibility, and raw values.
## Examples
```csharp
// Example 1: Retrieve plugin data for a specific card.
string cardId = "<your_card_id>";
List<PluginData> pluginDataList = await trelloClient.GetPluginDataOnCardAsync(cardId);
foreach (PluginData pluginData in pluginDataList)
{
    Console.WriteLine($"Plugin ID: {pluginData.PluginId}, Value: {pluginData.Value}");
}
```

