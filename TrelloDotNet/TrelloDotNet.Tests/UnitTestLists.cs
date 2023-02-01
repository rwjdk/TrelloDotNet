using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public class UnitTestLists
{
    [Fact]
    public async Task GetBoardLists()
    {
        var trelloClient = TestHelper.GetClient();

        var list = await trelloClient.Boards.GetListsAsync(Constants.SampleBoardId);
    }

    [Fact]
    public async Task AddListToBoard()
    {
        var trelloClient = TestHelper.GetClient();

        var result = await trelloClient.Lists.AddAsync(Constants.SampleBoardLongId, "My New List!!!");
    }
        
    [Fact]
    public async Task GetList()
    {
        var trelloClient = TestHelper.GetClient();

        var result = await trelloClient.Lists.GetAsync(Constants.SampleListId);
    }

    
}