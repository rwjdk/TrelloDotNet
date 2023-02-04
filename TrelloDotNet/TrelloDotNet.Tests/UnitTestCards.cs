using TrelloDotNet.Model;

namespace TrelloDotNet.Tests
{
    public class UnitTestCards
    {
        [Fact]
        public async Task GetBoardCards()
        {
            var trelloClient = TestHelper.GetClient();
            var result = await trelloClient.GetCardsOnBoardAsync(Constants.SampleBoardLongId);
        }

        [Fact]
        public async Task GetCard()
        {
            var trelloClient = TestHelper.GetClient();

            var result = await trelloClient.GetCardAsync(Constants.SampleCardId);
        }

        [Fact]
        public async Task Add10Card()
        {
            var trelloClient = TestHelper.GetClient();
            var boardId = "63d128787441d05619f44dbe";
            var labels = await trelloClient.GetLabelsOfBoardAsync(boardId);
            var listId = "63de3eb58d0d357abc562b02";

            var membersOfBoardAsync = await trelloClient.GetMembersOfBoardAsync(boardId);

            for (int i = 0; i < 10; i++)
            {

                var card = new Card(listId, "My Card " + i, "Some description");
                card.LabelIds.Add(labels.First().Id);
                card.MemberIds.Add(membersOfBoardAsync.First().Id);

                var addCard = await trelloClient.AddCardAsync(card);
            }
        }

        [Fact]
        public async Task Add1Card()
        {
            var trelloClient = TestHelper.GetClient();
            var boardId = "63d128787441d05619f44dbe";
            var labels = await trelloClient.GetLabelsOfBoardAsync(boardId);
            var listId = "63de3eb58d0d357abc562b02";

            var membersOfBoardAsync = await trelloClient.GetMembersOfBoardAsync(boardId);

            var card = new Card(listId, "xMy Card", "Some description");
            card.LabelIds.Add(labels.First().Id);
            var addCard = await trelloClient.AddCardAsync(card);
        }
        
        [Fact]
        public async Task GetCardsOnList()
        {
            var trelloClient = TestHelper.GetClient();
            var listId = "63de3eb58d0d357abc562b02";
            var cardsOnListAsync = await trelloClient.GetCardsOnListAsync(listId);
        }

        [Fact]
        public async Task Update1Card()
        {
            var trelloClient = TestHelper.GetClient();
            var cardAsync = await trelloClient.GetCardAsync("63de84fd6ec529e05977a5ab");
            cardAsync.Name = "cool update";


            var updateCardAsync = await trelloClient.UpdateCardAsync(cardAsync);
        }

        [Fact]
        public async Task GetCardChecklists()
        {
            var trelloClient = TestHelper.GetClient();

            var result = await trelloClient.GetChecklistsOnCardAsync("63d387ea95b02878fec0b6c0");
            var membersAsync = await trelloClient.GetMembersOfBoardAsync(Constants.SampleBoardId);
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
            var addChecklist = await trelloClient.AddChecklistAsync("63d387ea95b02878fec0b6c0", new Checklist("My_Cool_list with items 3", checklistItems), true);
        }

        [Fact]
        public async Task CreateChecklistFromSource()
        {
            var trelloClient = TestHelper.GetClient();
            var addChecklist = await trelloClient.AddChecklistAsync("63da9fdc0156601dbbe1563c", "63dbd5d5841dc8223cb67aeb");
        }

        [Fact]
        public async Task UpdateCard()
        {
            var trelloClient = TestHelper.GetClient();

            var card = await trelloClient.GetCardAsync("63d387ea95b02878fec0b6c0");

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
                await trelloClient.UpdateCardAsync(card);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}