using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public class UnitTestLists
{
    [Fact]
    public async Task GetBoardLists()
    {
        var trelloClient = TestHelper.GetClient();
        var list = await trelloClient.GetListsOnBoardAsync(Constants.SampleBoardId);
    }

    [Fact]
    public async Task AddListToBoard()
    {
        var trelloClient = TestHelper.GetClient();

        var result = await trelloClient.AddListAsync(new List("XList", Constants.SampleBoardLongId));

        result.Name = "Some new name of the list";

        await trelloClient.UpdateListAsync(result);
    }
        
    [Fact]
    public async Task GetList()
    {
        var trelloClient = TestHelper.GetClient();

        var result = await trelloClient.GetListAsync(Constants.SampleListId);
    }

    
}