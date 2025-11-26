using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class BoardCustomTests : TestBase
{
    [Fact]
    public async Task AddBoardWithOptions()
    {
        string? boardId = null;
        string? organizationId = null;
        try
        {
            Organization organization = await TrelloClient.AddOrganizationAsync(new Organization("UnitTestOrganization-CustomTestOrg"), cancellationToken: TestCancellationToken);
            organizationId = organization.Id;

            var addBoardOptions = new AddBoardOptions
            {
                DefaultLabels = false,
                DefaultLists = false,
                WorkspaceId = null
            };
            var custom = new Board("UnitTestBoard-CustomTest")
            {
                OrganizationId = organizationId
            };
            var board = await TrelloClient.AddBoardAsync(custom, addBoardOptions, cancellationToken: TestCancellationToken);
            boardId = board.Id;
            var lists = await TrelloClient.GetListsOnBoardAsync(boardId, cancellationToken: TestCancellationToken);
            var labels = await TrelloClient.GetLabelsOfBoardAsync(boardId, cancellationToken: TestCancellationToken);
            Assert.Empty(lists);
            Assert.Empty(labels);
        }
        finally
        {
            if (boardId != null)
            {
                TrelloClient.Options.AllowDeleteOfBoards = true;
                await TrelloClient.DeleteBoardAsync(boardId, cancellationToken: TestCancellationToken);
                TrelloClient.Options.AllowDeleteOfBoards = false;
            }

            if (organizationId != null)
            {
                TrelloClient.Options.AllowDeleteOfOrganizations = true;
                await TrelloClient.DeleteOrganizationAsync(organizationId, cancellationToken: TestCancellationToken);
                TrelloClient.Options.AllowDeleteOfOrganizations = false;
            }
        }
    }
}