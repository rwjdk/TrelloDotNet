using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class PluginDataTests(TestFixtureWithNewBoard fixture) : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board = fixture.Board!;

    [Fact]
    public async Task GetPluginDataOnCard()
    {
        var list = await AddDummyList(_board.Id);
        var card = await AddDummyCardToList(list);

        var pluginData = await TrelloClient.GetPluginDataOnCardAsync(card.Id);

        Assert.NotNull(pluginData);
        Assert.Empty(pluginData);
    }

    [Fact]
    public async Task GetPluginDataOnCardWithPluginId()
    {
        var list = await AddDummyList(_board.Id);
        var card = await AddDummyCardToList(list);
        const string pluginId = "com.example.plugin";

        PluginData pluginData = await TrelloClient.GetPluginDataOnCardAsync(card.Id, pluginId);

        Assert.Null(pluginData);
    }

    [Fact]
    public async Task GetPluginDataOnCardWithGenericType()
    {
        var list = await AddDummyList(_board.Id);
        var card = await AddDummyCardToList(list);
        const string pluginId = "com.example.plugin";

        var pluginData = await TrelloClient.GetPluginDataOnCardAsync<DummyPlugin>(card.Id, pluginId);

        Assert.Null(pluginData);
    }

    [Fact]
    public async Task GetPluginDataOnBoard()
    {
        var pluginData = await TrelloClient.GetPluginDataOfBoardAsync(_board.Id);

        Assert.NotNull(pluginData);
        Assert.Empty(pluginData);
    }

    [Fact]
    public async Task GetPluginDataOnBoardWithPluginId()
    {
        const string pluginId = "com.example.plugin";

        var pluginData = await TrelloClient.GetPluginDataOfBoardAsync(_board.Id, pluginId);

        Assert.Null(pluginData);
    }

    [Fact]
    public async Task GetPluginDataOnBoardWithGenericType()
    {
        const string pluginId = "com.example.plugin";

        DummyPlugin pluginData = await TrelloClient.GetPluginDataOfBoardAsync<DummyPlugin>(_board.Id, pluginId);

        Assert.Null(pluginData);
    }
}

public class DummyPlugin
{
    //Empty
}