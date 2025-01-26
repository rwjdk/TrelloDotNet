using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.AddCardOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class BoardMultiTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string? _boardId = fixture.BoardId;
    private readonly string? _organizationId = fixture.OrganizationId;

    [Fact]
    public async Task ListCanBeMovedToAnotherBoard()
    {
        string? secondBoardId = null;
        try
        {
            var board = new Board("UnitTestBoard - Second Board")
            {
                OrganizationId = _organizationId
            };
            var secondBoard = await TrelloClient.AddBoardAsync(board);
            secondBoardId = secondBoard.Id;

            var addedList = await TrelloClient.AddListAsync(new List("List on first board", _boardId));
            await TrelloClient.AddCardAsync(new AddCardOptions(addedList.Id, "card to move between boards"));
            var listOnPrimaryBoard = await TrelloClient.GetListsOnBoardAsync(_boardId);
            var listOnSecondaryBoard = await TrelloClient.GetListsOnBoardAsync(secondBoardId);
            Assert.Equal(4, listOnPrimaryBoard.Count);
            Assert.Equal(3, listOnSecondaryBoard.Count);
            var cardsOnPrimaryBoard = await TrelloClient.GetCardsOnBoardAsync(_boardId);
            var cardsOnSecondaryBoard = await TrelloClient.GetCardsOnBoardAsync(secondBoardId);
            Assert.Single(cardsOnPrimaryBoard);
            Assert.Empty(cardsOnSecondaryBoard);

            await TrelloClient.MoveListToBoardAsync(addedList.Id, secondBoardId);

            var listOnPrimaryBoardAfterMove = await TrelloClient.GetListsOnBoardAsync(_boardId);
            var listOnSecondaryBoardAfterMove = await TrelloClient.GetListsOnBoardAsync(secondBoardId);
            Assert.Equal(3, listOnPrimaryBoardAfterMove.Count);
            Assert.Equal(4, listOnSecondaryBoardAfterMove.Count);

            var cardsOnPrimaryBoardAfterMove = await TrelloClient.GetCardsOnBoardAsync(_boardId);
            var cardsOnSecondaryBoardAfterMove = await TrelloClient.GetCardsOnBoardAsync(secondBoardId);
            Assert.Empty(cardsOnPrimaryBoardAfterMove);
            Assert.Single(cardsOnSecondaryBoardAfterMove);
        }
        finally
        {
            if (secondBoardId != null)
            {
                TrelloClient.Options.AllowDeleteOfBoards = true;
                await TrelloClient.DeleteBoardAsync(secondBoardId);
                TrelloClient.Options.AllowDeleteOfBoards = false;
            }
        }
    }
}