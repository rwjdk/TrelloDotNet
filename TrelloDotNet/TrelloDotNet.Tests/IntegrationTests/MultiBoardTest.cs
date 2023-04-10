using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

[Collection("Integration Tests")]
public class MultiBoardTest : TestBaseWithNewBoard
{
    [Fact]
    public async Task ListCanBeMovedToAnotherBoard()
    {
        try
        {
            await CreateNewBoard();
            string? secondBoardId = null;
            try
            {
                var secondBoard = await TrelloClient.AddBoardAsync(new Board("Second Board"));
                secondBoardId = secondBoard.Id;

                var addedList = await TrelloClient.AddListAsync(new List("List on first board", BoardId));
                await TrelloClient.AddCardAsync(new Card(addedList.Id, "card to move between boards"));
                var listOnPrimaryBoard = await TrelloClient.GetListsOnBoardAsync(BoardId);
                var listOnSecondaryBoard = await TrelloClient.GetListsOnBoardAsync(secondBoardId);
                Assert.Equal(4, listOnPrimaryBoard.Count);
                Assert.Equal(3, listOnSecondaryBoard.Count);
                var cardsOnPrimaryBoard = await TrelloClient.GetCardsOnBoardAsync(BoardId);
                var cardsOnSecondaryBoard = await TrelloClient.GetCardsOnBoardAsync(secondBoardId);
                Assert.Single(cardsOnPrimaryBoard);
                Assert.Empty(cardsOnSecondaryBoard);

                await TrelloClient.MoveListToBoardAsync(addedList.Id, secondBoardId);

                var listOnPrimaryBoardAfterMove = await TrelloClient.GetListsOnBoardAsync(BoardId);
                var listOnSecondaryBoardAfterMove = await TrelloClient.GetListsOnBoardAsync(secondBoardId);
                Assert.Equal(3, listOnPrimaryBoardAfterMove.Count);
                Assert.Equal(4, listOnSecondaryBoardAfterMove.Count);

                var cardsOnPrimaryBoardAfterMove = await TrelloClient.GetCardsOnBoardAsync(BoardId);
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
        finally
        {
            await DeleteBoard();
        }
    }
}