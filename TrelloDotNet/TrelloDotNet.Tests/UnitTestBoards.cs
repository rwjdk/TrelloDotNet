using TrelloDotNet.Model;

namespace TrelloDotNet.Tests
{
    public class UnitTestBoards
    {
        [Fact]
        public async Task GetRaw()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.GetAsync("boards/" + Constants.SampleBoardId);
            var result2 = await trelloClient.GetAsync<Board>("boards/" + Constants.SampleBoardId);
        }

        [Fact]
        public async Task GetBoard()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.GetBoardAsync("63d128787441d05619f44dbe");
        }
        
        [Fact]
        public async Task GetBoardChecklists()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.GetChecklistsOnCardAsync("63d128787441d05619f44dbe");
        }
    }
}