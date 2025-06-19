This method retrieves all plugin data associated with a specific Trello board. Plugin data represents information stored by Power-Ups (plugins) that are enabled on the board. The returned data includes details for each plugin, with the value field containing JSON strings that represent the plugin-specific data. Users are responsible for deserializing these JSON strings to access the plugin-specific information. This method is useful for accessing and analyzing plugin data for boards, but note that the API does not support adding, updating, or removing plugin data.

## Method Signature
```csharp
async Task<List<PluginData>> GetPluginDataOfBoardAsync(string boardId)
```

### Parameters
- `boardId` The unique identifier of the board whose plugin data is to be retrieved.
### Return value
`List<PluginData>` A collection of plugin data objects, each containing information about a specific plugin enabled on the board. The `value` field in each object is a JSON string that requires deserialization to access plugin-specific details.
## Examples
```csharp
// Example 1: Retrieve plugin data for a specific board.
string boardId = "<your_board_id>";
List<PluginData> pluginData = await trelloClient.GetPluginDataOfBoardAsync(boardId);
foreach (PluginData data in pluginData)
{
    Console.WriteLine($"Plugin ID: {data.PluginId}, Data: {data.Value}");
}
```