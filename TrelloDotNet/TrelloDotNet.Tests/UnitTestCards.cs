using TrelloDotNet.Model;

namespace TrelloDotNet.Tests
{
    public class UnitTestCards
    {
        [Fact]
        public async Task GetBoardCards()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.Boards.GetCardsAsync(Constants.SampleBoardLongId);
        }

        [Fact]
        public async Task GetCard()
        {
            var trelloClient = TestHelper.GetClient();

            var result = await trelloClient.Cards.GetAsync(Constants.SampleBoardId);
        }
        
        [Fact]
        public async Task UpdateCard()
        {
            var trelloClient = TestHelper.GetClient();

            var result = await trelloClient.Cards.UpdateAsync("63d387ea95b02878fec0b6c0", 
                newName: "My new name",
                newDescription: "En ny description",
                due: new DateTimeOffset(2022,12,31,6,0,0,TimeSpan.Zero));
        }
    }
}