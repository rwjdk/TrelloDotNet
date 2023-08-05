using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.IntegrationTests;

public class ActionTests : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string? _boardId;
    private readonly Board _board;
    private Organization _organization;

    public ActionTests(TestFixtureWithNewBoard fixture)
    {
        _boardId = fixture.BoardId;
        _board = fixture.Board!;
        _organization = fixture.Organization!;
    }

    [Fact]
    public async Task GetActionsOfBoard()
    {
        var nameBefore = _board.Name;
        string newName = _board.Name+ "GetActionsOfBoard";
        _board.Name = newName;
        await TrelloClient.UpdateBoardAsync(_board);
        var actions = await TrelloClient.GetActionsOfBoardAsync(_boardId, new List<string> { WebhookActionTypes.UpdateBoard});
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateBoard && x.Data.Board.Name == newName && x.Data.Old.Name == nameBefore);
    }
    
    [Fact]
    public async Task GetActionsForOrganizations()
    {
        var nameBefore = _organization.DisplayName;
        string newName = _organization.DisplayName + "GetActionsForOrganizations";
        _organization.DisplayName = newName;
        await TrelloClient.UpdateOrganizationAsync(_organization);
        var actions = await TrelloClient.GetActionsForOrganizationsAsync(_organization.Id, new List<string> { WebhookActionTypes.UpdateOrganization});
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateOrganization && x.Data.Organization.Name == newName && x.Data.Old.DisplayName == nameBefore);
    }
    
    [Fact]
    public async Task GetActionsOfCard()
    {
        const string testName = "GetActionsOfCard";
        List list = await TrelloClient.AddListAsync(new List(testName, _boardId));
        Card card = await TrelloClient.AddCardAsync(new Card(list.Id, testName));
        const string newName = testName+"X";
        card.Name = newName;
        await TrelloClient.UpdateCardAsync(card);
        var actions = await TrelloClient.GetActionsOnCardAsync(card.Id, new List<string> { WebhookActionTypes.UpdateCard});
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateCard && x.Data.Card.Id == card.Id && x.Data.Card.Name == newName && x.Data.Old.Name == testName);
    }
    
    [Fact]
    public async Task GetActionsForList()
    {
        const string testName = "GetActionsOfList";
        List list = await TrelloClient.AddListAsync(new List(testName, _boardId));
        Card card = await TrelloClient.AddCardAsync(new Card(list.Id, testName));
        const string newName = testName+"X";
        card.Name = newName;
        await TrelloClient.UpdateCardAsync(card);
        var actions = await TrelloClient.GetActionsForListAsync(list.Id, new List<string> { WebhookActionTypes.UpdateCard});
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateCard && x.Data.Card.Id == card.Id && x.Data.Card.Name == newName && x.Data.Old.Name == testName);
    }
    
    [Fact]
    public async Task GetActionsForMember()
    {
        const string testName = "GetActionsForMember";
        List list = await TrelloClient.AddListAsync(new List(testName, _boardId));
        Card card = await TrelloClient.AddCardAsync(new Card(list.Id, testName));
        const string newName = testName+"X";
        card.Name = newName;
        await TrelloClient.UpdateCardAsync(card);
        Member member = await TrelloClient.GetTokenMemberAsync();
        var actions = await TrelloClient.GetActionsForMemberAsync(member.Id, new List<string> { WebhookActionTypes.UpdateCard});
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateCard && x.Data.Card.Id == card.Id && x.Data.Card.Name == newName && x.Data.Old.Name == testName);
    }
}