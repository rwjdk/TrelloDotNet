using Microsoft.Extensions.Configuration;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public abstract class IntegrationTestBase : IDisposable
{
    protected TrelloClient TrelloClient;
    protected Board Board;
    protected readonly string BoardId;
    protected readonly string BoardName;
    protected readonly string BoardDescription;


    protected IntegrationTestBase()
    {
        TrelloClient = new TestHelper().GetClient();
        BoardName = $"UnitTestBoard-{DateTime.Now:yyyyMMddHHmmss}";
        BoardDescription = $"BoardDescription-{DateTime.Now:yyyyMMddHHmmss}";
        Board = TrelloClient.AddBoardAsync(new Board(BoardName, BoardDescription)).Result;
        BoardId = Board.Id;
        Assert.Equal(BoardName, Board.Name);
        Assert.Equal(BoardDescription, Board.Description);
    }

    public void Dispose()
    {
        try
        {
            TrelloClient.DeleteBoard(BoardId).Wait();
        }
        catch (Exception e)
        {
            Assert.Contains("Deletion of Boards are disabled via Options.AllowDeleteOfBoards (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back: https://support.atlassian.com/trello/docs/deleting-a-board/)", e.Message);
        }
        finally
        {
            TrelloClient.Options.AllowDeleteOfBoards = true;
            TrelloClient.DeleteBoard(BoardId).Wait();
        }
    }
}