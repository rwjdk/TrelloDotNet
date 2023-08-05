using System.Text.Json;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class RawCallTests : TestBase, IClassFixture<TestFixtureWithNewBoard>
{
    private readonly Board _board;

    public RawCallTests(TestFixtureWithNewBoard fixture)
    {
        _board = fixture.Board!;
    }

    [Fact]
    public async Task RawExceptionThrowsIncludeUrlButMaskCredentials()
    {
        try
        {
            TrelloClient.Options.ApiCallExceptionOption = ApiCallExceptionOption.IncludeUrlButMaskCredentials;
            await TrelloClient.GetAsync("xyz");
        }
        catch (TrelloApiException e)
        {
            //Empty
            Assert.Contains("XXXXXXXXXX", e.DataSentToTrello);
        }
    }

    [Fact]
    public async Task RawExceptionThrowsErrorWithoutUrl()
    {
        try
        {
            TrelloClient.Options.ApiCallExceptionOption = ApiCallExceptionOption.DoNotIncludeTheUrl;
            await TrelloClient.GetAsync("xyz");
        }
        catch (TrelloApiException e)
        {
            //Empty
            Assert.Equal("XXXXX", e.DataSentToTrello);
        }
    }

    [Fact]
    public async Task RawExceptionThrowsErrorWithUrlAndCredentials()
    {
        try
        {
            TrelloClient.Options.ApiCallExceptionOption = ApiCallExceptionOption.IncludeUrlAndCredentials;
            await TrelloClient.GetAsync("xyz");
        }
        catch (TrelloApiException e)
        {
            //Empty
            Assert.DoesNotContain("XXXXXXXXXX", e.DataSentToTrello);
        }
    }

    [Fact]
    public async Task RawExceptionsThrowCorrectException()
    {
        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.GetAsync("xyz"));
        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.PostAsync("xyz"));
        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.PutAsync("xyz"));
        await Assert.ThrowsAsync<TrelloApiException>(async () => await TrelloClient.DeleteAsync("xyz"));
    }

    [Fact]
    public async Task RawGet()
    {
        //Raw JSON
        var rawGet = await TrelloClient.GetAsync($"boards/{_board.Id}");
        Assert.NotNull(rawGet);

        //Raw
        var rawGetBoard = await TrelloClient.GetAsync<Board>($"boards/{_board.Id}");
        Assert.Equal(_board.Id, rawGetBoard.Id);
    }

    [Fact]
    public async Task RawPost()
    {
        var list = await TrelloClient.AddListAsync(new List("List for Card Tests", _board.Id));
        var rawPost = await TrelloClient.PostAsync("cards", new QueryParameter("name", "Card"), new QueryParameter("idList", list.Id));
        Assert.NotNull(rawPost);

        var rawPostCard = await TrelloClient.PostAsync<Card>("cards", new QueryParameter("name", "Card"), new QueryParameter("idList", list.Id));
        Assert.NotNull(rawPostCard.Id);
    }

    [Fact]
    public async Task RawPut()
    {
        Card card = await AddDummyCard(_board.Id, "RawPut");

        var rawUpdate = await TrelloClient.PutAsync($"cards/{card.Id}", new QueryParameter("desc", "New Description"));
        Assert.NotNull(rawUpdate);

        var rawUpdateCard = await TrelloClient.PutAsync<Card>($"cards/{card.Id}", new QueryParameter("desc", "New Description2"));
        Assert.Equal("New Description2", rawUpdateCard.Description);
    }

    [Fact]
    public async Task RawPutWithPayload()
    {
        Card card = await AddDummyCard(_board.Id, "RawPut");

        CardCover coverToAdd = new CardCover(CardCoverColor.Black, CardCoverSize.Full);
        coverToAdd.PrepareForAddUpdate();
        string payload = $"{{\"cover\":{JsonSerializer.Serialize(coverToAdd)}}}";

        var rawUpdate = await TrelloClient.PutAsync($"{UrlPaths.Cards}/{card.Id}", payload);
        Assert.NotNull(rawUpdate);
    }
}