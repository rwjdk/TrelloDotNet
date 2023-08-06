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
            Organization organization = await TrelloClient.AddOrganizationAsync(new Organization("UnitTestOrganization-CustomTestOrg"));
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
            var board = await TrelloClient.AddBoardAsync(custom, addBoardOptions);
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
                await TrelloClient.DeleteBoardAsync(boardId);
                TrelloClient.Options.AllowDeleteOfBoards = false;
            }

            if (organizationId != null)
            {
                TrelloClient.Options.AllowDeleteOfOrganizations = true;
                await TrelloClient.DeleteOrganizationAsync(organizationId);
                TrelloClient.Options.AllowDeleteOfOrganizations = false;
            }
        }
    }
}