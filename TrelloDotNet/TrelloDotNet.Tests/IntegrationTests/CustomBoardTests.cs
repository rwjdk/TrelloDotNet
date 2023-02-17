using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class CustomBoardTests : TestBase
{
    [Fact]
    public async Task AddBoardWithOptions()
    {
        string? boardId = null;
        try
        {
            var addBoardOptions = new AddBoardOptions
            {
                DefaultLabels = false,
                DefaultLists = false,
                WorkspaceId = ""
            };
            var board = await TrelloClient.AddBoardAsync(new Board("CustomTest"), addBoardOptions);
            boardId = board.Id;
            var lists = await TrelloClient.GetListsOnBoardAsync(boardId);
            var labels = await TrelloClient.GetLabelsOfBoardAsync(boardId);
            Assert.Empty(lists);
            Assert.Empty(labels);
        }
        finally
        {
            if (boardId != null)
            {
                TrelloClient.Options.AllowDeleteOfBoards = true;
                await TrelloClient.DeleteBoard(boardId);
            }
        }
    }
}