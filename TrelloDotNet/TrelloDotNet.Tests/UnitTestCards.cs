using Microsoft.VisualBasic;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests
{
    public class UnitTestCards
    {
        [Fact]
        public async Task GetBoardCards()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.Cards.GetCardsOnBoardAsync(Constants.SampleBoardLongId);
            
        }

        [Fact]
        public async Task GetCard()
        {
            var trelloClient = TestHelper.GetClient();

            var result = await trelloClient.Cards.GetCardAsync("63da9fdc0156601dbbe1563c");
            result.Name = "new name";
            await trelloClient.Cards.UpdateCardAsync(result);
        }
        
        [Fact]
        public async Task GetCardChecklists()
        {
            var trelloClient = TestHelper.GetClient();

            var result = await trelloClient.Checklists.GetChecklistsOnCardAsync("63d387ea95b02878fec0b6c0");
            var membersAsync = await trelloClient.Members.GetMembersOfBoardAsync(Constants.SampleBoardId);
        }
        
        [Fact]
        public async Task CreateChecklist()
        {
            var trelloClient = TestHelper.GetClient();
            var checklistItems = new List<ChecklistItem>
            {
                new("Item 1"),
                new("Item 2", DateTimeOffset.Now),
                new("Item 3", DateTimeOffset.Now)
            };
            var addChecklist = await trelloClient.Checklists.AddChecklistAsync("63d387ea95b02878fec0b6c0", new Checklist("My_Cool_list with items 3", checklistItems), true);
        }
        
        [Fact]
        public async Task CreateChecklistFromSource()
        {
            var trelloClient = TestHelper.GetClient();
            var addChecklist = await trelloClient.Checklists.AddChecklistAsync("63da9fdc0156601dbbe1563c", "63dbd5d5841dc8223cb67aeb");
        }
        
        [Fact]
        public async Task UpdateCard()
        {
            var trelloClient = TestHelper.GetClient();

            var card = await trelloClient.Cards.GetCardAsync("63d387ea95b02878fec0b6c0");

            /*
            var result = await trelloClient.Cards.UpdateCardAsync("63d387ea95b02878fec0b6c0", 
                newName: "My new name",
                newDescription: "En ny description",
                due: new DateTimeOffset(2022,12,31,6,0,0,TimeSpan.Zero));
            */

            card.Name = "new name";
            card.Description = "Hello World";
            card.DueComplete = false;
            card.Due = DateTimeOffset.Now.AddDays(3);
            //card.LabelIds = new List<string> { "63da8bfd53f1561554e42bb6" };
            card.LabelIds = new List<string>(); //new List<string> { "63da8bfd53f1561554e42bb6" };
            card.Subscribed = false;
            card.BoardId = "63d128840231b184c3a2c1cd";
            //card.BoardId = Constants.SampleBoardLongId;
            card.Start = DateTimeOffset.Now;
            //var allLists = await trelloClient.Boards.GetListsOnBoardAsync(Constants.SampleBoardId);
            //card.ListId = allLists.First(x=> x.Name == "Done").Id;
            card.ListId = null;
            try
            {
                await trelloClient.Cards.UpdateCardAsync(card);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}