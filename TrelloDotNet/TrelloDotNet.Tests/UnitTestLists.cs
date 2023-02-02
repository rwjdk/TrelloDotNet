using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public class UnitTestLists
{
    [Fact]
    public async Task GetBoardLists()
    {
        var trelloClient = TestHelper.GetClient();
        var list = await trelloClient.Lists.GetListsOnBoardAsync(Constants.SampleBoardId);
    }

    [Fact]
    public async Task AddListToBoard()
    {
        var trelloClient = TestHelper.GetClient();

        var result = await trelloClient.Lists.AddListAsync(new List("XList", Constants.SampleBoardLongId));
    }
        
    [Fact]
    public async Task GetList()
    {
        var trelloClient = TestHelper.GetClient();

        var result = await trelloClient.Lists.GetListAsync(Constants.SampleListId);
    }

    
}