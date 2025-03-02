using System.Diagnostics;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.GetLabelOptions;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.Tests.IntegrationTests;

public class BoardTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string _boardId = fixture.BoardId!;
    private readonly string _boardName = fixture.BoardName!;
    private readonly string _boardDescription = fixture.BoardDescription!;
    private readonly Board _board = fixture.Board!;

    [Fact]
    public async Task DefaultBoardData()
    {
        //Check Create date
        AssertTimeIsNow(_board.Created);

        Assert.False(_board.Closed);
        Assert.NotEmpty(_board.ShortUrl);
        Assert.NotEmpty(_board.Url);
        Assert.False(_board.Pinned);
        Assert.Null(_board.EnterpriseId);
        Assert.NotEmpty(_board.OrganizationId);

        var getBoardWithOptions = await TrelloClient.GetBoardAsync(_boardId, new GetBoardOptions
        {
            IncludeLists = GetBoardOptionsIncludeLists.Open,
            IncludeCards = GetBoardOptionsIncludeCards.OpenCards,
            BoardFields = new BoardFields(BoardFieldsType.Name)
        });

        Assert.NotNull(getBoardWithOptions.Lists);
        Assert.Contains(getBoardWithOptions.Lists, x => x.Name == "To Do");
        Assert.NotNull(getBoardWithOptions.Cards);
        Assert.Null(getBoardWithOptions.Url);

        TrelloPlanInformation plan = await TrelloClient.GetTrelloPlanInformationForBoardAsync(_boardId);
        Assert.Equal(_boardName, plan.Name);
        Assert.NotEmpty(plan.Features);
    }

    [Fact]
    public async Task DefaultMembers()
    {
        //Member Tests
        var members = await TrelloClient.GetMembersOfBoardAsync(_boardId);
        Assert.Single(members);
        var member = await TrelloClient.GetMemberAsync(members.First().Id);
        Assert.Equal(member.FullName, members.First().FullName);
        Assert.Equal(member.Username, members.First().Username);
    }

    [Fact]
    public async Task DefaultLists()
    {
        var lists = await TrelloClient.GetListsOnBoardAsync(_boardId);
        var todoList = lists.FirstOrDefault(x => x.Name == "To Do");
        var doingList = lists.FirstOrDefault(x => x.Name == "Doing");
        var doneList = lists.FirstOrDefault(x => x.Name == "Done");
        Assert.NotNull(todoList);
        Assert.NotNull(doingList);
        Assert.NotNull(doneList);
    }

    [Fact]
    public async Task NoClosedLists()
    {
        //No closed lists
        var listsFiltered = await TrelloClient.GetListsOnBoardFilteredAsync(_boardId, ListFilter.Closed);
        Assert.Empty(listsFiltered);
    }

    [Fact]
    public async Task DefaultCards()
    {
        //No Cards on the lists
        var lists = await TrelloClient.GetListsOnBoardAsync(_boardId);
        foreach (List list in lists)
        {
            Assert.Empty(await TrelloClient.GetCardsInListAsync(list.Id));
        }

        Assert.Empty(await TrelloClient.GetCardsOnBoardAsync(_boardId));
        Assert.Empty(await TrelloClient.GetCardsOnBoardAsync(_boardId, new GetCardOptions
            {
                Filter = CardsFilter.All
            }
        ));
    }

    [Fact]
    public async Task BoardUpdate()
    {
        //Board Update
        Debug.Assert(_board != null, nameof(_board) + " != null");
        _board.Name = _boardName + "X";
        _board.Description = _boardDescription + "X";
        var updatedBoard = await TrelloClient.UpdateBoardAsync(_board);
        var getBoard = await TrelloClient.GetBoardAsync(_boardId);
        //Assert.EndsWith("X", updatedBoard.Name); //todo: Add again once bug https://support.atlassian.com/requests/TRELLO-212222/ is resolved
        Assert.EndsWith("X", updatedBoard.Description);
        Assert.EndsWith("X", getBoard.Name);
        Assert.EndsWith("X", getBoard.Description);
        //Assert.Equal(updatedBoard.Name, getBoard.Name); //todo: Add again once bug https://support.atlassian.com/requests/TRELLO-212222/ is resolved
        Assert.Equal(updatedBoard.Description, getBoard.Description);

        var getBoardWithOptions = await TrelloClient.GetBoardAsync(_boardId, new GetBoardOptions
        {
            ActionsTypes = new ActionTypesToInclude([WebhookActionTypes.UpdateBoard])
        });
        Assert.NotEmpty(getBoardWithOptions.Actions);
    }

    [Fact]
    public async Task DefaultChecklists()
    {
        //Checklist Defaults
        var checklists = await TrelloClient.GetChecklistsOnBoardAsync(_boardId);
        Assert.Empty(checklists);
    }

    [Fact]
    public async Task DefaultLabels()
    {
        //Label Defaults
        var labels = await TrelloClient.GetLabelsOfBoardAsync(_boardId);
        Assert.Equal(6, labels.Count);
        foreach (var label in labels)
        {
            Assert.NotEmpty(label.Id);
            Assert.NotEmpty(label.BoardId);
            Assert.Empty(label.Name);
            AssertTimeIsNow(label.Created);
        }

        var labelsWithOptions = await TrelloClient.GetLabelsOfBoardAsync(_boardId, new GetLabelOptions
        {
            Limit = 2,
            LabelFields = new LabelFields(LabelFieldsType.Name)
        });
        Assert.Equal(2, labelsWithOptions.Count);
        Assert.Null(labelsWithOptions[0].Color);
        Assert.Null(labelsWithOptions[1].Color);


        Label addedLabel = await TrelloClient.AddLabelAsync(new Label(_boardId, "MyLabel", "red"));
        Assert.NotNull(addedLabel.Id);
        Assert.Equal("red", addedLabel.Color);

        Label updateLabel = labels[0];
        updateLabel.Name = "Hello world";
        updateLabel.Color = "red";
        await TrelloClient.UpdateLabelAsync(updateLabel);

        await TrelloClient.DeleteLabelAsync(labels[1].Id);
        await TrelloClient.DeleteLabelAsync(labels[2].Id);

        var labelsAfterAddUpdateAndRemove = await TrelloClient.GetLabelsOfBoardAsync(_boardId);
        Assert.Equal(5, labelsAfterAddUpdateAndRemove.Count);
        Assert.Equal(3, labelsAfterAddUpdateAndRemove.Count(x => x.Color == "red"));
    }

    [Fact]
    public async Task MembershipInformation()
    {
        var tokenMember = await TrelloClient.GetTokenMemberAsync();
        List<Membership> memberships = await TrelloClient.GetMembershipsOfBoardAsync(_boardId);
        Assert.Single(memberships);
        Membership membership = memberships.Single(x => x.MemberId == tokenMember.Id);
        Assert.Equal(MembershipType.Admin, membership.MemberType);
        Assert.False(membership.Unconfirmed);
        Assert.False(membership.Deactivated);
    }

    [Fact]
    public async Task PluginInformation()
    {
        var plugins = await TrelloClient.GetPluginsOnBoardAsync(_boardId);
        Assert.Contains(plugins, x => x.Name == "Butler");
    }

    [Fact]
    public async Task BoardsInOrganization()
    {
        var boards = await TrelloClient.GetBoardsInOrganizationAsync(_board.OrganizationId);
        Assert.Single(boards);
        Assert.Equal(_board.Id, boards.First().Id);

        boards = await TrelloClient.GetBoardsInOrganizationAsync(_board.OrganizationId, new GetBoardOptions
        {
            IncludeLists = GetBoardOptionsIncludeLists.All
        });
        Assert.Single(boards);
        Assert.Equal(_board.Id, boards.First().Id);
        foreach (Board board in boards)
        {
            Assert.NotNull(board.Lists);
        }
    }
}