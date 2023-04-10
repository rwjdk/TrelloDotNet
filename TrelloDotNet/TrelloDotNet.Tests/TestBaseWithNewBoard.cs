using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public abstract class TestBaseWithNewBoard : TestBase
{
    protected Board? Board { get; private set; }
    protected string? BoardId { get; private set; }
    protected string? BoardName { get; private set; }
    protected string? BoardDescription { get; private set; }

    public async Task CreateNewBoard()
    {
        BoardName = $"UnitTestBoard-{DateTime.Now:yyyyMMddHHmmss}";
        BoardDescription = $"BoardDescription-{DateTime.Now:yyyyMMddHHmmss}";
        Board = await TrelloClient.AddBoardAsync(new Board(BoardName, BoardDescription));
        BoardId = Board.Id;
        Assert.Equal(BoardName, Board.Name);
        Assert.Equal(BoardDescription, Board.Description);
    }
    
    public async Task DeleteBoard()
    {
        WaitToAvoidRateLimits(10);
        try
        {
            await TrelloClient.DeleteBoardAsync(BoardId);
        }
        catch (Exception e)
        {
            Assert.Contains("Deletion of Boards are disabled via Options.AllowDeleteOfBoards (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back: https://support.atlassian.com/trello/docs/deleting-a-board/)", e.Message);
        }
        finally
        {
            TrelloClient.Options.AllowDeleteOfBoards = true;
            await TrelloClient.DeleteBoardAsync(BoardId);
        }
    }
}