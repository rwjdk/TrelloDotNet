using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class TokenTests : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board;
    private readonly Organization _organization;

    public TokenTests(TestFixtureWithNewBoard fixture)
    {
        _board = fixture.Board!;
        _organization = fixture.Organization!;
    }

    [Fact]
    public async Task GetBoardsCurrentTokenCanAccess()
    {
        var boards = await TrelloClient.GetBoardsCurrentTokenCanAccessAsync();
        Assert.Contains(boards, x => x.Id == _board.Id);
    }
    
    [Fact]
    public async Task GetOrganizationsCurrentTokenCanAccess()
    {
        var organizations = await TrelloClient.GetOrganizationsCurrentTokenCanAccessAsync();
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