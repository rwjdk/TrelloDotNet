using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.AddCardOptions;
using TrelloDotNet.Model.Options.GetActionsOptions;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.IntegrationTests;

public class ActionTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string? _boardId = fixture.BoardId;
    private readonly Board _board = fixture.Board!;
    private readonly Organization _organization = fixture.Organization!;

    [Fact]
    public async Task GetActionsOfBoard()
    {
        var nameBefore = _board.Name;
        string newName = _board.Name + "GetActionsOfBoard";
        await TrelloClient.UpdateBoardAsync(_board.Id, [BoardUpdate.Name(newName)]);
        var actions = await TrelloClient.GetActionsOfBoardAsync(_boardId, new GetActionsOptions
        {
            Filter = [WebhookActionTypes.UpdateBoard]
        });
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateBoard && x.Data.Board.Name == newName && x.Data.Old.Name == nameBefore);
    }

    [Fact]
    public async Task GetActionsForOrganizations()
    {
        var nameBefore = _organization.DisplayName;
        string newName = _organization.DisplayName + "GetActionsForOrganizations";
        _organization.DisplayName = newName;
        await TrelloClient.UpdateOrganizationAsync(_organization);
        var actions = await TrelloClient.GetActionsForOrganizationsAsync(_organization.Id, new GetActionsOptions
        {
            Filter = [WebhookActionTypes.UpdateOrganization]
        });
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateOrganization && x.Data.Organization.Name == newName && x.Data.Old.DisplayName == nameBefore);
    }

    [Fact]
    public async Task GetActionsOfCard()
    {
        const string testName = "GetActionsOfCard";
        List list = await TrelloClient.AddListAsync(new List(testName, _boardId));
        Card card = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, testName));
        const string newName = testName + "X";
        await TrelloClient.UpdateCardAsync(card.Id, [
            CardUpdate.Name(newName),
        ]);
        var actions = await TrelloClient.GetActionsOnCardAsync(card.Id, new GetActionsOptions
        {
            Filter = [WebhookActionTypes.UpdateCard]
        });
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateCard && x.Data.Card.Id == card.Id && x.Data.Card.Name == newName && x.Data.Old.Name == testName);
    }

    [Fact]
    public async Task GetActionsForList()
    {
        const string testName = "GetActionsOfList";
        List list = await TrelloClient.AddListAsync(new List(testName, _boardId));
        Card card = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, testName));
        const string newName = testName + "X";
        await TrelloClient.UpdateCardAsync(card.Id, [CardUpdate.Name(newName)]);
        var actions = await TrelloClient.GetActionsForListAsync(list.Id, new GetActionsOptions
        {
            Filter = [WebhookActionTypes.UpdateCard]
        });
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateCard && x.Data.Card.Id == card.Id && x.Data.Card.Name == newName && x.Data.Old.Name == testName);
    }

    [Fact]
    public async Task GetActionsForMember()
    {
        const string testName = "GetActionsForMember";
        List list = await TrelloClient.AddListAsync(new List(testName, _boardId));
        Card card = await TrelloClient.AddCardAsync(new AddCardOptions(list.Id, testName));
        const string newName = testName + "X";
        await TrelloClient.UpdateCardAsync(card.Id, [CardUpdate.Name(newName)]);
        Member member = await TrelloClient.GetTokenMemberAsync();
        var actions = await TrelloClient.GetActionsForMemberAsync(member.Id, new GetActionsOptions
        {
            Filter = [WebhookActionTypes.UpdateCard]
        });
        Assert.Contains(actions, x => x.Type == WebhookActionTypes.UpdateCard && x.Data.Card.Id == card.Id && x.Data.Card.Name == newName && x.Data.Old.Name == testName);
    }
}