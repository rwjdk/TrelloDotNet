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

            result.Description = "New description";
            result.Name = "New Name";

            await trelloClient.UpdateBoardAsync(result);
        }
        
        [Fact]
        public async Task GetBoardMembers()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.GetMembersOfBoardAsync("63d128787441d05619f44dbe");

            var member = await trelloClient.GetMember(result.First().Id);
        }

        [Fact]
        public async Task GetBoardActions()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.GetBoardAsync("63d128787441d05619f44dbe");

            //var actionsOnBoard = await trelloClient.GetActionsOnBoard("SCPjg8ON");
        }

        [Fact]
        public async Task AddBoard()
        {
            await Task.CompletedTask;
            var trelloClient = TestHelper.GetClient();
            //var result = await trelloClient.AddBoardAsync(new Board("My Api board", "with some cool description"));

            //var actionsOnBoard = await trelloClient.GetActionsOnBoard("SCPjg8ON");
        }

        [Fact]
        public async Task GetBoardChecklists()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.GetChecklistsOnCardAsync("63d128787441d05619f44dbe");
        }
    }
}