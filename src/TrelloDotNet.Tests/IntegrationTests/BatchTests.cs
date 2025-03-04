using TrelloDotNet.Model;
using TrelloDotNet.Model.Batch;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.GetListOptions;

namespace TrelloDotNet.Tests.IntegrationTests;

public class BatchTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly string _boardId = fixture.BoardId!;

    [Fact]
    public async Task ExecuteBatchedRequestAsync()
    {
        Board? b = null;

        await TrelloClient.ExecuteBatchedRequestAsync(
        [
            BatchRequest.GetBoard(_boardId, result => b = result.GetData<Board>())
        ]);

        Assert.NotNull(b);
    }

    [Fact]
    public async Task GetOrganizations()
    {
        var data = await TrelloClient.GetOrganizationsAsync([fixture.OrganizationId!]);
        Assert.NotEmpty(data);
    }

    [Fact]
    public async Task GetNoneExistOrganizations()
    {
        await Assert.ThrowsAsync<TrelloApiException>(async () =>
        {
            var data = await TrelloClient.GetOrganizationsAsync(["non-Exist"]);
            Assert.NotEmpty(data);
        });
    }

    [Fact]
    public async Task GetListsAndCards()
    {
        (List? list, Card? card) = await AddDummyCardAndList(_boardId);
        // ReSharper disable once RedundantAssignment
        var listData = await TrelloClient.GetListsAsync([list.Id]);
        listData = await TrelloClient.GetListsAsync([list.Id], new GetListOptions
        {
            IncludeBoard = true,
            IncludeCards = GetListOptionsIncludeCards.All
        });
        Assert.NotEmpty(listData);
        Assert.Equal(list.Id, listData[0].Id);
        Assert.Equal(list.Name, listData[0].Name);
        Assert.Equal(list.BoardId, listData[0].BoardId);
        Assert.Equal(list.Closed, listData[0].Closed);
        Assert.Equal(list.Position, listData[0].Position);

        var data = await TrelloClient.GetCardsAsync([card.Id]);
        Assert.NotEmpty(data);
        Assert.Equal(card.Id, data[0].Id);
        Assert.Equal(card.Name, data[0].Name);
        Assert.Equal(card.ListId, data[0].ListId);
        Assert.Equal(card.BoardId, data[0].BoardId);
        Assert.Equal(card.Description, data[0].Description);
        Assert.Equal(card.Closed, data[0].Closed);
        Assert.Equal(card.Position, data[0].Position);
        Assert.Equal(card.Due, data[0].Due);
        Assert.Equal(card.DueComplete, data[0].DueComplete);
        Assert.Equal(card.Start, data[0].Start);
        Assert.Equal(card.MemberIds, data[0].MemberIds);
        Assert.Equal(card.LabelIds, data[0].LabelIds);

        data = await TrelloClient.GetCardsAsync([card.Id], new GetCardOptions
        {
            CardFields = new CardFields(CardFieldsType.Name)
        });
        Assert.NotEmpty(data);
        Assert.Equal(card.Id, data[0].Id);
        Assert.Equal(card.Name, data[0].Name);
        Assert.Null(data[0].Description);
        Assert.Null(data[0].Due);
        Assert.Null(data[0].Start);
    }


    [Fact]
    public async Task GetMembers()
    {
        Member member = await TrelloClient.GetTokenMemberAsync();
        var data = await TrelloClient.GetMembersAsync([member.Id]);
        Assert.NotEmpty(data);
    }

    [Fact]
    public async Task GetBoards()
    {
        var data = await TrelloClient.GetBoardsAsync([fixture.BoardId!]);
        Assert.NotEmpty(data);

        data = await TrelloClient.GetBoardsAsync([fixture.BoardId!], new GetBoardOptions
        {
            IncludeLabels = true
        });
        Assert.NotEmpty(data);
    }
}