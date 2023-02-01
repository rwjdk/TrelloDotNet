using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public class UnitTestCustomFields
{
    [Fact]
    public async Task GetCustomField()
    {
        var trelloClient = TestHelper.GetClient();
        var result = await trelloClient.CustomFields.GetAsync(Constants.CustomFieldId);
    }
        
    [Fact]
    public async Task AddCustomField()
    {
        var trelloClient = TestHelper.GetClient();
        var board = await trelloClient.Boards.GetAsync(Constants.SampleBoardId);
        var customFieldOptions = new List<CustomFieldOption>
        {
            new("Option1"),
            new("Option2")
        };
        var result = await trelloClient.CustomFields.AddAsync(Constants.SampleBoardLongId, "MyField7", CustomFieldType.List, CustomFieldPosition.Top, true);
    }
}