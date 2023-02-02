using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public class UnitTestCustomFields
{
    [Fact]
    public async Task GetCustomField()
    {
        var trelloClient = TestHelper.GetClient();
        var result = await trelloClient.CustomFields.GetCustomFieldAsync(Constants.CustomFieldId);
    }
        
    [Fact]
    public async Task AddCustomField()
    {
        var trelloClient = TestHelper.GetClient();
        var board = await trelloClient.Boards.GetBoardAsync(Constants.SampleBoardId);
        var customFieldOptions = new List<CustomFieldOption>
        {
            new("Option1"),
            new("Option2")
        };
        var result = await trelloClient.CustomFields.AddCustomFieldAsync(Constants.SampleBoardLongId, "MyField7", CustomFieldType.List, true);
    }
}