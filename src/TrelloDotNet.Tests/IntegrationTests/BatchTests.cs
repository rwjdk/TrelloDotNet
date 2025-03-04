using TrelloDotNet.Model;
using TrelloDotNet.Model.Batch;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.AddCardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.GetListOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class BatchTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string _boardId = fixture.BoardId!;

    [Fact]
    public async Task ExecuteBatchedRequestAsync()
    {
        Board? b = null;

        await TrelloClient.ExecuteBatchedRequestAsync(
        [
            BatchRequest.GetBoard(_boardId, result => b = result.GetData<Board>())
        ]);
        Assert.NotNull(b);
    }
}