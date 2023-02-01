using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests
{
    public class UnitTest1
    {
        private const string SampleBoardId = "SCPjg8ON";
        private const string SampleCardId = "63d387e699609c7f4bf0e0ce";
        private const string SampleListId = "63d38da6995aa43f38bd3ff9";
        private const string CustomFieldId = "63da6e4df46172832ddbde14";

        [Fact]
        public async Task GetRaw()
        {
            var trelloClient = GetClient();
            var result = await trelloClient.GetAsync("boards/" + SampleBoardId);
            var result2= await trelloClient.GetAsync<Board>("boards/" + SampleBoardId);
        }
        
        [Fact]
        public async Task GetCustomField()
        {
            var trelloClient = GetClient();
            var result = await trelloClient.CustomFields.GetAsync(CustomFieldId);
        }
        
        [Fact]
        public async Task GetLabes()
        {
            var trelloClient = GetClient();
            var result = await trelloClient.Boards.GetLabelsAsync(SampleBoardId);
        }
        
        [Fact]
        public async Task AddCustomField()
        {
            var trelloClient = GetClient();
            var board = await trelloClient.Boards.GetAsync(SampleBoardId);
            var customFieldOptions = new List<CustomFieldOption>
            {
                new("Option1"),
                new("Option2")
            };
            var result = await trelloClient.CustomFields.AddAsync(board.Id, "MyField7", CustomFieldType.List, CustomFieldPosition.Top, true);
        }

        [Fact]
        public async Task GetBoard()
        {
            var trelloClient = GetClient();
            var result = await trelloClient.Boards.GetAsync("63d128787441d05619f44dbe");
        }
        
        [Fact]
        public async Task GetBoardCards()
        {
            var trelloClient = GetClient();
            var result = await trelloClient.Boards.GetCardsAsync("63d128787441d05619f44dbe");
        }
        
        [Fact]
        public async Task GetBoardLists()
        {
            var trelloClient = GetClient();

            var list = await trelloClient.Boards.GetListsAsync(SampleBoardId);
        }

        [Fact]
        public async Task AddListToBoard()
        {
            var trelloClient = GetClient();

            var board = await trelloClient.Boards.GetAsync(SampleBoardId);
            var result = await trelloClient.Lists.AddAsync(board, "My New List!!!");
        }

        [Fact]
        public async Task GetCard()
        {
            var trelloClient = GetClient();

            var result = await trelloClient.Cards.GetAsync(SampleCardId);
        }
        
        [Fact]
        public async Task GetList()
        {
            var trelloClient = GetClient();

            var result = await trelloClient.Lists.GetAsync(SampleListId);
        }

        private static ITrelloClient GetClient()
        {
            ITrelloClient trelloClient = new TrelloClient("8000d9bde07ef82025e9e070c7ea82d8", "ATTA8a75349a982325c7b874caf6e2f174d78ddb39d38e4aff22064703d509142db45E058BAB");
            return trelloClient;
        }
    }
}