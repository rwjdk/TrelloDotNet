using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public abstract class TestBaseWithNewBoard : TestBase, IDisposable
{
    protected Board? Board { get; private set; }
    protected string? BoardId { get; private set; }
    protected string? BoardName { get; private set; }
    protected string? BoardDescription { get; private set; }

    protected TestBaseWithNewBoard()
    {
        CreateNewBoard();
    }

    private void CreateNewBoard()
    {
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