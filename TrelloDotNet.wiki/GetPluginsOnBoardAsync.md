This method retrieves a list of plugins that are registered on a specified Trello board. Plugins in Trello are associated with Power-Ups, which extend the functionality of boards.

## Method Signature
```csharp
async Task<List<Plugin>> GetPluginsOnBoardAsync(string boardId)
```

### Parameters
- `boardId` The unique identifier of the Trello board from which to retrieve the plugins.
### Return value
`List<Plugin>` A collection of plugins registered on the specified board.
## Examples
```csharp
// Example 1: Retrieve all plugins on a specific board.
string boardId = "<your_board_id>";
List<Plugin> plugins = await trelloClient.GetPluginsOnBoardAsync(boardId);
foreach (Plugin plugin in plugins)
{
    Console.WriteLine($"Plugin Name: {plugin.Name}, Plugin ID: {plugin.Id}");
}
```
