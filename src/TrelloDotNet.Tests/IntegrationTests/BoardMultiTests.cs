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
            var secondBoard = await TrelloClient.AddBoardAsync(board, cancellationToken: TestCancellationToken);
            secondBoardId = secondBoard.Id;

            var addedList = await TrelloClient.AddListAsync(new List("List on first board", _boardId), cancellationToken: TestCancellationToken);
            await TrelloClient.AddCardAsync(new AddCardOptions(addedList.Id, "card to move between boards"), cancellationToken: TestCancellationToken);
            var listOnPrimaryBoard = await TrelloClient.GetListsOnBoardAsync(_boardId, cancellationToken: TestCancellationToken);
            var listOnSecondaryBoard = await TrelloClient.GetListsOnBoardAsync(secondBoardId, cancellationToken: TestCancellationToken);
            Assert.Equal(4, listOnPrimaryBoard.Count);
            Assert.Equal(3, listOnSecondaryBoard.Count);
            var cardsOnPrimaryBoard = await TrelloClient.GetCardsOnBoardAsync(_boardId, cancellationToken: TestCancellationToken);
            var cardsOnSecondaryBoard = await TrelloClient.GetCardsOnBoardAsync(secondBoardId, cancellationToken: TestCancellationToken);
            Assert.Single(cardsOnPrimaryBoard);
            Assert.Empty(cardsOnSecondaryBoard);

            await TrelloClient.MoveListToBoardAsync(addedList.Id, secondBoardId, cancellationToken: TestCancellationToken);

            var listOnPrimaryBoardAfterMove = await TrelloClient.GetListsOnBoardAsync(_boardId, cancellationToken: TestCancellationToken);
            var listOnSecondaryBoardAfterMove = await TrelloClient.GetListsOnBoardAsync(secondBoardId, cancellationToken: TestCancellationToken);
            Assert.Equal(3, listOnPrimaryBoardAfterMove.Count);
            Assert.Equal(4, listOnSecondaryBoardAfterMove.Count);

            var cardsOnPrimaryBoardAfterMove = await TrelloClient.GetCardsOnBoardAsync(_boardId, cancellationToken: TestCancellationToken);
            var cardsOnSecondaryBoardAfterMove = await TrelloClient.GetCardsOnBoardAsync(secondBoardId, cancellationToken: TestCancellationToken);
            Assert.Empty(cardsOnPrimaryBoardAfterMove);
            Assert.Single(cardsOnSecondaryBoardAfterMove);
        }
        finally
        {
            if (secondBoardId != null)
            {
                TrelloClient.Options.AllowDeleteOfBoards = true;
                await TrelloClient.DeleteBoardAsync(secondBoardId, cancellationToken: TestCancellationToken);
                TrelloClient.Options.AllowDeleteOfBoards = false;
            }
        }
    }
}