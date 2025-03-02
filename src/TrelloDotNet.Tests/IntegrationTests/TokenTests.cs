using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetOrganizationOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class TokenTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board = fixture.Board!;
    private readonly Organization _organization = fixture.Organization!;

    [Fact]
    public async Task GetBoardsCurrentTokenCanAccess()
    {
        var boards = await TrelloClient.GetBoardsCurrentTokenCanAccessAsync();
        Assert.Contains(boards, x => x.Id == _board.Id);
    }

    [Fact]
    public async Task GetCurrentTokenMembershipsAsync()
    {
        TokenMembershipOverview memberships = await TrelloClient.GetCurrentTokenMembershipsAsync();
        Assert.NotNull(memberships);
        Assert.NotEmpty(memberships.OrganizationMemberships);
        Assert.Contains(memberships.OrganizationMemberships, pair => pair.Key.Id == _board.OrganizationId);
        Assert.NotEmpty(memberships.BoardMemberships);
        Assert.Contains(memberships.BoardMemberships, pair => pair.Key.Id == _board.Id);
    }

    [Fact]
    public async Task GetOrganizationsCurrentTokenCanAccess()
    {
        var organizations = await TrelloClient.GetOrganizationsCurrentTokenCanAccessAsync();
        Assert.Contains(organizations, x => x.Id == _organization.Id);
    }

    [Fact]
    public async Task GetOrganizationsCurrentTokenCanAccessWithOptions()
    {
        var organizations = await TrelloClient.GetOrganizationsCurrentTokenCanAccessAsync(new GetOrganizationOptions
        {
            OrganizationFields = new OrganizationFields(OrganizationFieldsType.Name, OrganizationFieldsType.Url)
        });
        Assert.Contains(organizations, x => x.Id == _organization.Id);
    }

    [Fact]
    public async Task TokenInformation()
    {
        var tokenInformation = await TrelloClient.GetTokenInformationAsync();
        Assert.NotNull(tokenInformation);
        Assert.NotNull(tokenInformation.Created);
        Assert.Null(tokenInformation.Expires);
        Assert.NotNull(tokenInformation.Id);
        Assert.NotNull(tokenInformation.Identifier);
        Assert.NotNull(tokenInformation.MemberId);
        Assert.NotNull(tokenInformation.Permissions);
        Assert.NotNull(tokenInformation.Permissions[0].ModelId);
        Assert.NotNull(tokenInformation.Permissions[0].ModelType);
        Assert.True(tokenInformation.Permissions[0].Read);
        Assert.True(tokenInformation.Permissions[0].Write);

        var tokenMember = await TrelloClient.GetTokenMemberAsync();
        Assert.NotNull(tokenMember);
    }
}