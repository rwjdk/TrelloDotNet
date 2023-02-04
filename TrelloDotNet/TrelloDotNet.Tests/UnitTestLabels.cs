using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public class UnitTestLabels
{
    [Fact]
    public async Task GetLabels()
    {
        var trelloClient = TestHelper.GetClient();
        var result = await trelloClient.GetLabelsOfBoardAsync(Constants.SampleBoardId);
    }
}