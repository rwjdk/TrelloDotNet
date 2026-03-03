using System.Net;
using System.Net.Http;
using System.Text;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.AddCardOptions;

namespace TrelloDotNet.Tests.UnitTests;

public class CardDescriptionRequestBehaviorTests
{
    [Fact]
    public async Task AddCardAsync_Throws_WhenDescriptionIsLongerThanMaximumAllowed()
    {
        var handler = new RecordingHandler();
        using var httpClient = new HttpClient(handler);
        var client = new TrelloClient("key", "token", httpClient: httpClient);
        var cancellationToken = TestContext.Current.CancellationToken;

        string tooLongDescription = new string('x', 16_385);
        var options = new AddCardOptions("listId", "card name", tooLongDescription);

        await Assert.ThrowsAsync<TrelloApiException>(() => client.AddCardAsync(options, cancellationToken));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task UpdateCardAsync_Throws_WhenDescriptionIsLongerThanMaximumAllowed()
    {
        var handler = new RecordingHandler();
        using var httpClient = new HttpClient(handler);
        var client = new TrelloClient("key", "token", httpClient: httpClient);
        var cancellationToken = TestContext.Current.CancellationToken;

        string tooLongDescription = new string('x', 16_385);
        var updates = new List<CardUpdate> { CardUpdate.Description(tooLongDescription) };

        await Assert.ThrowsAsync<TrelloApiException>(() => client.UpdateCardAsync("cardId", updates, cancellationToken));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task AddCardAsync_SendsDescriptionInJsonPayload_WhenQueryStringWouldBeTooLong()
    {
        var handler = new RecordingHandler();
        using var httpClient = new HttpClient(handler);
        var client = new TrelloClient("key", "token", httpClient: httpClient);
        var cancellationToken = TestContext.Current.CancellationToken;

        string description = new string('x', 16_380);
        var options = new AddCardOptions("listId", "card name", description);
        options.Checklists = null;
        options.AttachmentFileUploads = null;
        options.AttachmentUrlLinks = null;
        options.CustomFields = null;

        _ = await client.AddCardAsync(options, cancellationToken);

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.DoesNotContain("desc=", request.Uri.Query, StringComparison.Ordinal);
        Assert.NotNull(request.Body);
        Assert.Contains("\"desc\":", request.Body!, StringComparison.Ordinal);
    }

    [Fact]
    public async Task UpdateCardAsync_SendsDescriptionInJsonPayload_WhenQueryStringWouldBeTooLong()
    {
        var handler = new RecordingHandler();
        using var httpClient = new HttpClient(handler);
        var client = new TrelloClient("key", "token", httpClient: httpClient);
        var cancellationToken = TestContext.Current.CancellationToken;

        string description = new string('x', 16_380);
        var updates = new List<CardUpdate>
        {
            CardUpdate.Name("new card name"),
            CardUpdate.Description(description)
        };

        _ = await client.UpdateCardAsync("cardId", updates, cancellationToken);

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.DoesNotContain("desc=", request.Uri.Query, StringComparison.Ordinal);
        Assert.Contains("name=", request.Uri.Query, StringComparison.Ordinal);
        Assert.NotNull(request.Body);
        Assert.Contains("\"desc\":", request.Body!, StringComparison.Ordinal);
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public List<RecordedRequest> Requests { get; } = new();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string? body = request.Content != null ? await request.Content.ReadAsStringAsync(cancellationToken) : null;
            Requests.Add(new RecordedRequest(request.Method, request.RequestUri!, body));

            string json = "{\"id\":\"cardId\",\"idList\":\"listId\",\"name\":\"card name\",\"desc\":\"description\"}";
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }
    }

    private sealed record RecordedRequest(HttpMethod Method, Uri Uri, string? Body);
}
