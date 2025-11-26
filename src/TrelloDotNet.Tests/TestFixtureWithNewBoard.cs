using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public class TestFixtureWithNewBoard : TestBase, IAsyncLifetime
{
    public Board? Board { get; set; }
    public string? BoardId { get; set; }
    public string? BoardName { get; set; }
    public string? BoardDescription { get; set; }
    public Organization? Organization { get; set; }
    public string? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }

    public async ValueTask InitializeAsync()
    {
        Assert.True(TrelloClient.Options.MaxRetryCountForTokenLimitExceeded > 0);
        Assert.True(TrelloClient.Options.DelayInSecondsToWaitInTokenLimitExceededRetry > 0);

        var organizationName = Guid.NewGuid().ToString();
        OrganizationName = $"UnitTestOrganization-{organizationName}";
        Organization = await TrelloClient.AddOrganizationAsync(new Organization(OrganizationName), cancellationToken: TestCancellationToken);
        OrganizationId = Organization.Id;
        Assert.Equal(OrganizationName, Organization.DisplayName);

        var boardName = Guid.NewGuid().ToString();
        BoardName = $"UnitTestBoard-{boardName}";
        BoardDescription = $"BoardDescription-{boardName}";
        var board = new Board(BoardName, BoardDescription)
        {
            OrganizationId = Organization.Id
        };
        Board = await TrelloClient.AddBoardAsync(board, cancellationToken: TestCancellationToken);
        BoardId = Board.Id;
        Assert.Equal(BoardName, Board.Name);
        Assert.Equal(BoardDescription, Board.Description);
        Assert.Equal(OrganizationId, Board.OrganizationId);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await TrelloClient.DeleteBoardAsync(BoardId, cancellationToken: TestCancellationToken);
        }
        catch (Exception e)
        {
            Assert.Contains("Deletion of Boards are disabled via Options.AllowDeleteOfBoards (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back: https://support.atlassian.com/trello/docs/deleting-a-board/)", e.Message);
        }
        finally
        {
            TrelloClient.Options.AllowDeleteOfBoards = true;
            await TrelloClient.DeleteBoardAsync(BoardId, cancellationToken: TestCancellationToken);
        }

        try
        {
            await TrelloClient.DeleteOrganizationAsync(OrganizationId, cancellationToken: TestCancellationToken);
        }
        catch (Exception e)
        {
            Assert.Contains("Deletion of Organizations are disabled via Options.AllowDeleteOfOrganizations (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back)", e.Message);
        }
        finally
        {
            TrelloClient.Options.AllowDeleteOfOrganizations = true;
            await TrelloClient.DeleteOrganizationAsync(OrganizationId, cancellationToken: TestCancellationToken);
        }

        TrelloClient.Options.AllowDeleteOfBoards = false;
        TrelloClient.Options.AllowDeleteOfOrganizations = false;
    }
}